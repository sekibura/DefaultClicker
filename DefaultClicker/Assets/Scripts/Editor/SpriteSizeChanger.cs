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
    private Texture2D texture;
    private VerticalSide verticalSide;
    private HorizontalSide horizontalSide;

    [MenuItem("Tools/SpriteSizeChanger")]
    static void Init()
    {
        SpriteSizeChanger window = (SpriteSizeChanger)EditorWindow.GetWindow(typeof(SpriteSizeChanger));
        window.Show();
    }

    void OnGUI()
    {
        //GUILayout.BeginVertical();

        GUILayout.Label("Sprite for size changing:", EditorStyles.boldLabel);
        //GUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        //GUILayout.Label("Sprite: ", EditorStyles.wordWrappedLabel);
        //doDownload = EditorGUILayout.Toggle(doDownload);
        texture = (Texture2D)EditorGUILayout.ObjectField("Sprite:", texture, typeof(Texture2D), true);
        //verticalSide = (VerticalSide)EditorGUILayout.ObjectField("Sprite:", verticalSide, typeof(VerticalSide), true);
        
        verticalSide = (VerticalSide)EditorGUILayout.EnumPopup("Vertical side for cahnge", verticalSide);
        horizontalSide = (HorizontalSide)EditorGUILayout.EnumPopup("Horizontal side for cahnge", horizontalSide);


        //GUILayout.FlexibleSpace();
        //GUILayout.EndHorizontal();
        //GUILayout.Label("Если не загружать список с сервера, будет использован файл, находящийся в папке \\CombApp\\BuildUtils", EditorStyles.helpBox);
        //device = (BuildDeviceType)EditorGUILayout.EnumPopup("Устройство:", device);

        //GUILayout.EndVertical();
        if (GUILayout.Button("Generate"))
        {
            var newSize = CalcNewSize();
            var newTexture = ResizeTexture(newSize);
            SaveNewTexture(newTexture);

        }
    }

    private Vector2Int CalcNewSize()
    {
        float width = texture.width;
        float height = texture.height;

        // Вычисляем новые размеры, кратные 4
        int newWidth = Mathf.CeilToInt(width / 4f) * 4;
        int newHeight = Mathf.CeilToInt(height / 4f) * 4;

        Debug.Log($"{width}|{height} - {newWidth}|{newHeight}");
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
        //newTexture.SetPixels(newX, newY, texture.width, texture.height, texture.GetPixels()); 
        //newTexture.SetPixels(texture.GetPixels());
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


        Debug.Log(sourceFileName);
        Debug.Log(newFileName);
        File.WriteAllBytes(newFileName, bytes);
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
