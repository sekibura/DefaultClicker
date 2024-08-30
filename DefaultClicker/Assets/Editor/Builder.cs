using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
//using System.Media;

public class Builder : MonoBehaviour
{
    public enum BuildPlatformType
    {
        WebYG,
        AndroidRustore
    }
     
    private const string BuildsFolder = "Builds2";
    private const string RustoreDir = "Rustore";
    private const string YGDir = "YG";

    private const string DefaultProductName = "Clicker";
    private const string RustoreProductName = "ClickerRustore";
    private const string YGProductName = "ClickerYG";

    private const string YGBuildName = "Build";
    private const string YGBuildExtension = ".zip";
    private const string AndroidBuildName = "ApkBuild";
    private const string AndroidBuildExtension = ".apk";

    private static string[] RustoreScenes = new string[] {
        "Assets/Scenes/SampleScene.unity",
        //"Assets/Scenes/MainMenu.unity"
    };
    private static string[] YGScenes = new string[] {
        "Assets/Scenes/SampleScene.unity",
    };

    private const string RustoreBuildDefine = "RUSTORE_BUILD";
    private const string YGBuildDefine = "YG_BUILD";

    [MenuItem("Tools/BuildTools/Android/Rustore")]
    public static void BuildAndroidRustore()
    {
        Build(BuildPlatformType.AndroidRustore);
    }
    
    [MenuItem("Tools/BuildTools/WEBGL/YandexGames")]
    public static void BuildWebGLYG()
    {
        Build(BuildPlatformType.WebYG);
    }

    private static void Build(BuildPlatformType platformType)
    {
        DateTime buildStartTime = DateTime.Now;
        string platformDir = GetPlatformDir(platformType);
        string[] scenes = GetScenes(platformType);
        string definedSymbols = GetDefinedSymbols(platformType);
        //EditorSceneManager.OpenScene(scenes[0]);


        var appBuildsFolder = string.Concat(BuildsFolder, "\\", platformDir, "\\");
        //директория с "базовой" сборкой (чтобы использовать il2cpp incremental build, нужно собирать в директории с предыдущей сборкой)
        var baseBuildFile = Path.Combine(BuildsFolder, GetBuildName(platformType));
        Directory.CreateDirectory(appBuildsFolder);

        var nowDate = DateTime.Now.ToString("dd.MM.yy");
        var basePath = string.Format("{0}v{2}.", appBuildsFolder, GetBuildName(platformType), nowDate);
        var buildNum = GetBuildNum(appBuildsFolder, basePath);
        //Debug.Log($"{appBuildsFolder}\n{basePath}");

        var buildPathFinal = basePath + buildNum;
        Directory.CreateDirectory(buildPathFinal);


        PlayerSettings.productName = GetProductName(platformType);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(GetBuildTargetGroup(platformType), definedSymbols);
        

        //Debug.Log(Path.Combine(buildPathFinal, GetBuildName(platformType)));
        //Debug.Log(baseBuildFile);
        var result = BuildPipeline.BuildPlayer(scenes, baseBuildFile, GetBuildTarget(platformType), GetOptions(platformType));
        Debug.Log($"Build completed with result: {result.summary.result.ToString()}. (type: {platformType.ToString()}, target: {GetBuildTarget(platformType)}, time: {(DateTime.Now - buildStartTime).ToString()}");
        //Debug.Log(baseBuildFile);
        //Debug.Log(Path.Combine(buildPathFinal, GetBuildName(platformType)));
        File.Move(baseBuildFile + GetExtension(platformType), Path.Combine(buildPathFinal, GetBuildName(platformType)) + GetExtension(platformType));

        PlayFinishSound();
    }


    private static string GetPlatformDir(BuildPlatformType platformType)
    {
        switch (platformType)
        {
            case BuildPlatformType.AndroidRustore:
                return RustoreDir;
            case BuildPlatformType.WebYG:
                return YGDir;
            default:
                    throw new ArgumentOutOfRangeException("platformType", platformType, null);
        }
    }

    private static string[] GetScenes(BuildPlatformType platformType)
    {
        switch (platformType)
        {
            case BuildPlatformType.AndroidRustore:
                return RustoreScenes;
            case BuildPlatformType.WebYG:
                return YGScenes;
            default:
                throw new ArgumentOutOfRangeException("platformType", platformType, null);
        }
            
    }

    private static string GetDefinedSymbols(BuildPlatformType platformType)
    {
            
        switch (platformType) 
        {
            case BuildPlatformType.AndroidRustore:
                return RustoreBuildDefine;
            case BuildPlatformType.WebYG:
                return GetDefinesString(YGBuildDefine, "YG_PLUGIN_YANDEX_GAME");
            default:
                throw new ArgumentOutOfRangeException("platformType", platformType, null);
        }
    }

    private static string GetDefinesString(params string[] singleDefines)
    {
        return string.Join("; ", singleDefines);
    }

    private static string GetProductName(BuildPlatformType platformType)
    {
        switch (platformType)
        {
            case BuildPlatformType.AndroidRustore:
                return RustoreProductName;
            case BuildPlatformType.WebYG:
                return YGProductName;
            default:
                return DefaultProductName;
        }
    }

    private static BuildTargetGroup GetBuildTargetGroup(BuildPlatformType platformType)
    {
        switch (platformType)
        {
            case BuildPlatformType.AndroidRustore:
                return BuildTargetGroup.Android;
            case BuildPlatformType.WebYG:
                return BuildTargetGroup.WebGL;
            default:
                throw new ArgumentOutOfRangeException("platformType", platformType, null);
        }
    }
    private static BuildTarget GetBuildTarget(BuildPlatformType platformType)
    {
        switch (platformType)
        {
            case BuildPlatformType.AndroidRustore:
                return BuildTarget.Android;
            case BuildPlatformType.WebYG:
                return BuildTarget.WebGL;
            default:
                throw new ArgumentOutOfRangeException("platformType", platformType, null);
        }
    }

    private static string GetBuildName(BuildPlatformType platformType)
    {
        switch (platformType)
        {
            case BuildPlatformType.AndroidRustore:
                return AndroidBuildName;
            case BuildPlatformType.WebYG:
                return YGBuildName;
            default:
                throw new ArgumentOutOfRangeException("platformType", platformType, null);
        }
    }

    private static int GetBuildNum(string appBuildsFolder, string basePath)
    {
        int buildNum = 1;

        var dirs = Directory.GetDirectories(appBuildsFolder);

        while (dirs.Contains(basePath + buildNum))
        {
            buildNum++;
        }

        return buildNum;
    }

    private static string GetExtension(BuildPlatformType platformType)
    {
        switch (platformType)
        {
            case BuildPlatformType.AndroidRustore:
                return AndroidBuildExtension;
            case BuildPlatformType.WebYG:
                return YGBuildExtension;
            default:
                throw new ArgumentOutOfRangeException("platformType", platformType, null);
        }
    }
    private static BuildOptions GetOptions(BuildPlatformType platformType)
    {
        BuildOptions options = BuildOptions.None;
        switch (platformType)
        {
            case BuildPlatformType.AndroidRustore:
                options = BuildOptions.CompressWithLz4HC;
                break;
            case BuildPlatformType.WebYG:
                break;
        }
        return options;
    }


        public static void PlayFinishSound()
    {
        //System.Media.SoundPlayer player = new System.Media.SoundPlayer("Assets/Editor/BuildFinished.wav");
        //player.PlaySync();
    }
}


