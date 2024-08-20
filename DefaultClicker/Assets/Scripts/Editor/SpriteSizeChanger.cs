using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using UnityEditor;
using UnityEngine;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;

[UnityEngine.ExecuteInEditMode]
public class SpriteSizeChanger : EditorWindow
{
    [HideInInspector]
    private Texture2D texture;
    private VerticalSide verticalSide;
    private HorizontalSide horizontalSide;
    private bool toExpand = false;
    private bool sameName = false;
    private SpriteSizeChangerParameterSO sizeChangerParameter;

    [MenuItem("Tools/SpriteSizeChanger")]
    static void Init()
    {
        SpriteSizeChanger window = (SpriteSizeChanger)EditorWindow.GetWindow(typeof(SpriteSizeChanger));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Sprite for size changing:", EditorStyles.boldLabel);
        //texture = (Texture2D)EditorGUILayout.ObjectField("Sprite:", texture, typeof(Texture2D), true);
        sizeChangerParameter = (SpriteSizeChangerParameterSO)EditorGUILayout.ObjectField("sizeChangerParameterSO:", sizeChangerParameter, typeof(SpriteSizeChangerParameterSO), true);
        verticalSide = (VerticalSide)EditorGUILayout.EnumPopup("Vertical side for cahnge", verticalSide);
        horizontalSide = (HorizontalSide)EditorGUILayout.EnumPopup("Horizontal side for cahnge", horizontalSide);
        toExpand = EditorGUILayout.Toggle("To expand?", toExpand);
        sameName = EditorGUILayout.Toggle("Same name", sameName);
        if (GUILayout.Button("Generate"))
        {
            foreach (var txtr in sizeChangerParameter.textures)
            {
                texture = txtr;
                if ((texture.width % 4f == 0) && (texture.height % 4f == 0))
                {
                    Debug.Log("This sprite already x4!");
                    continue;
                }
                var newSize = CalcNewSize();
                var newTexture = ResizeTexture(newSize);
                SaveNewTexture(newTexture);
            }
           
        }
    }

    private Vector2Int CalcNewSize()
    {
        float width = texture.width;
        float height = texture.height;
        int newWidth;
        int newHeight;

        // Вычисляем новые размеры, кратные 4
        if (toExpand)
        {
            newWidth = Mathf.CeilToInt(width / 4f) * 4;
            newHeight = Mathf.CeilToInt(height / 4f) * 4;
        }
        else
        {
            newWidth = Mathf.FloorToInt(width / 4f) * 4;
            newHeight = Mathf.FloorToInt(height / 4f) * 4;
        }

        //Debug.Log($"{width}|{height} - {newWidth}|{newHeight}");
        return new Vector2Int(newWidth, newHeight);
    }

    private Texture2D ResizeTexture(Vector2Int newSize)
    {
        var newTexture = new Texture2D(newSize.x, newSize.y);

        //Где должны быть пусте пиксели
        int newX = 0; // координаты ориг пикчи на новой текстуре
        switch (horizontalSide)
        {
            case HorizontalSide.Left: //Где должны быть пусте пиксели
                newX = newSize.x - texture.width;
                break;
            case HorizontalSide.Right:
                newX = 0;
                break;
            case HorizontalSide.Both:
                newX = (newSize.x - texture.width) / 2;
                break;
            default:
                break;
        }

        int newY = 0;
        switch (verticalSide)
        {
            case VerticalSide.Bottom:
                newY = newSize.y - texture.height;
                break;
            case VerticalSide.Top:
                newY = 0;
                break;
            case VerticalSide.Both:
                newY = (newSize.y - texture.height) / 2;
                break;
            default:
                break;
        }

        SetTextureImporterFormat(texture, true);
        Color emptyPixel = new Color(0, 0, 0, 0);
        for (int x = 0; x < newSize.x; x++)
        {
            for (int y = 0; y < newSize.y; y++)
            {
                if((x < newX || x > newX + texture.width) || (y < newY || y > newY + texture.height))
                    newTexture.SetPixel(x, y, emptyPixel);
                else
                    newTexture.SetPixel(x, y, texture.GetPixel(x - newX, y - newY));
            }
        }
        SetTextureImporterFormat(texture, false);
        newTexture.Apply();
        return newTexture;
    }

    private void SaveNewTexture(Texture2D newTexture)
    {
        byte[] bytes = newTexture.EncodeToPNG();
        var sourceFileName = AssetDatabase.GetAssetPath(texture);
        if (string.IsNullOrEmpty(sourceFileName))
        {
            Debug.LogError($"Path is null or empty!");
            return;
        }
        string newFileName = Path.Combine(Path.GetDirectoryName(sourceFileName), $"{Path.GetFileNameWithoutExtension(sourceFileName)}_{newTexture.width}_{newTexture.height}{Path.GetExtension(sourceFileName)}");


        string resultPath = sameName ? sourceFileName : newFileName;
        Debug.Log(resultPath);
        File.WriteAllBytes(resultPath, bytes);
   
    }

    public static void SetTextureImporterFormat(Texture2D texture, bool isReadable)
    {
        if (null == texture) return;

        string assetPath = AssetDatabase.GetAssetPath(texture);
        var tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (tImporter != null)
        {
            tImporter.textureType = TextureImporterType.Sprite;
            tImporter.isReadable = isReadable;

            AssetDatabase.ImportAsset(assetPath);
            AssetDatabase.Refresh();
        }
    }

    public enum VerticalSide
    {
        Top,
        Bottom,
        Both
    }

    public enum HorizontalSide
    {
        Left,
        Right,
        Both
    }

}
