using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace JENKINS
{
    public class JenkinsBuildProcessor
    {
        public static string[] SCENES = FindEnabledEditorScenes();

        public static string APP_NAME = "DiceOfWaktaverse";
        public static string TARGET_WIN_DIR = "proj.pcwin/DiceOfWaktaverse.exe";

        [MenuItem("Custom/CI/BuildGradlePCRelease")]
        public static void BuildGradlePCRelease()
        {
            bool isFull = true;
            if (isFull)
            {
                // Fullscreen mode로 변경
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
            }
            else
            {
                Vector2Int screenSize = new Vector2Int(1920, 1080);
                PlayerSettings.resizableWindow = true;
                // 윈도우 모드로 변경
                Screen.SetResolution(screenSize.x, screenSize.y, FullScreenMode.Windowed);

            }

            GenericBuildPC(SCENES,
                         TARGET_WIN_DIR,
                         BuildTarget.StandaloneWindows,
                         BuildOptions.None);
        }
        private static string[] InitBuildSetting(bool isRelease)
        {
            List<string> Scenes = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
				if (scene.enabled)
                {
                    Scenes.Add(scene.path);
                }
            }
            return Scenes.ToArray();
        }

        public static void GenericBuildPC(string[] scenes,
                                        string dest_path,
                                        BuildTarget build_target,
                                        BuildOptions build_options)
        {
            scenes = InitBuildSetting(true);

            // do build
            BuildReport report = BuildPipeline.BuildPlayer(scenes,
                                                           dest_path,
                                                           build_target,
                                                           build_options);

            if (BuildResult.Succeeded != report.summary.result)
            {
                throw new Exception("Build failed: " + report.summary.result);
            }
        }

        private static string[] FindEnabledEditorScenes()
        {
            List<string> EditorScenes = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    EditorScenes.Add(scene.path);
                }
            }

            return EditorScenes.ToArray();
        }
    }

}