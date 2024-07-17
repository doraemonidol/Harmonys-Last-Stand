using MHLab.Patch.Core.Client;
using MHLab.Patch.Core.Client.IO;
using MHLab.Patch.Launcher.Scripts;
using UnityEditor;
using UnityEngine;

namespace MHLab.Patch.Admin.Editor.Inspectors
{
    [CustomEditor(typeof(LauncherData))]
    public class LauncherDataCustomInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Test Remote URL"))
            {
                var data = (LauncherData)target;

                var settings = CreateSettings(data.RemoteUrl);
                
                CheckRemoteUrl(settings);
            }
        }

        private LauncherSettings CreateSettings(string remoteUrl)
        {
            var settings = new LauncherSettings();
            settings.RemoteUrl = remoteUrl;
            return settings;
        }

        private void CheckRemoteUrl(ILauncherSettings settings)
        {
            var checker = new NetworkChecker();
            if (checker.IsRemoteServiceAvailable(settings.GetRemoteBuildsIndexUrl(), out var exception))
            {
                Debug.Log($"Perfect! Your Remote URL [{settings.RemoteUrl}] is reachable by the Launcher!");
                return;
            }
            
            Debug.LogError($"Wait! Double check your Remote URL [{settings.RemoteUrl}] before proceeding. It's not reachable by the Launcher! The specific file the Launcher is trying to access is: [{settings.GetRemoteBuildsIndexUrl()}]");
        }
    }
}