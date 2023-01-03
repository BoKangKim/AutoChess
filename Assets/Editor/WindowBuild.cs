using UnityEditor;
using System;
using System.IO;

public class WindowBuild
{
    private static readonly string defaultPath = "C:/MetaTrendTeam/Windows";
    private static string folder_date = "";
    private static string folder_time = "";

    [MenuItem("Tool/Build_Android")]
    public static void Build()
    {
        string[] scenes = { "Assets/Scenes/SampleScene.unity" };
        folder_date = defaultPath + "/" + DateTime.Now.ToString("yyyy_MM_dd");
        folder_time = folder_date + "/" + DateTime.Now.ToString("HH_mm_ss") + "/";

        FileInfo date = new FileInfo(folder_date);
        FileInfo time = new FileInfo(folder_time);

        if (date.Exists == false)
        {
            Directory.CreateDirectory(date.FullName);
        }

        if (time.Exists == false)
        {
            Directory.CreateDirectory(time.FullName);
        }

        BuildPipeline.BuildPlayer(scenes, folder_time + "build.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}
