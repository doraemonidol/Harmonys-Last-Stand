﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GAP_ParticleSystemController
{
    public static class SaveParticleSystemScript
    {
        public static void SaveVFX(GameObject prefabVFX, List<ParticleSystemOriginalSettings> psOriginalSettingsList)
        {
#if UNITY_2018_3_OR_NEWER
            var prefabFolderPath = GetPrefabFolder2018_3(prefabVFX);
#else
             var prefabFolderPath = GetPrefabFolder (prefabVFX);
#endif

#if UNITY_EDITOR
            if (!Directory.Exists(prefabFolderPath + "/OriginalSettings"))
            {
                AssetDatabase.CreateFolder(prefabFolderPath, "OriginalSettings");
                Debug.Log("Created folder:  " + prefabFolderPath + "/OriginalSettings");
            }
#endif
            var bf = new BinaryFormatter();
            var stream = new FileStream(prefabFolderPath + "/OriginalSettings/" + prefabVFX.name + ".dat",
                FileMode.Create);

            bf.Serialize(stream, psOriginalSettingsList);
            stream.Close();

#if UNITY_2018_3_OR_NEWER
            SaveNestedPrefab(prefabVFX);
#endif

            Debug.Log("Original Settings of '" + prefabVFX.name + "' saved to: " + prefabFolderPath +
                      "/OriginalSettings");
        }

        public static List<ParticleSystemOriginalSettings> LoadVFX(GameObject prefabVFX)
        {
#if UNITY_2018_3_OR_NEWER
            var prefabFolderPath = GetPrefabFolder2018_3(prefabVFX);
#else
            var prefabFolderPath = GetPrefabFolder(prefabVFX);
#endif

            if (File.Exists(prefabFolderPath + "/OriginalSettings/" + prefabVFX.name + ".dat"))
            {
                var bf = new BinaryFormatter();
                var stream = new FileStream(prefabFolderPath + "/OriginalSettings/" + prefabVFX.name + ".dat",
                    FileMode.Open);

                var originalSettingsList = new List<ParticleSystemOriginalSettings>();
                originalSettingsList = bf.Deserialize(stream) as List<ParticleSystemOriginalSettings>;

                stream.Close();
                return originalSettingsList;
            }

            Debug.Log("No saved VFX data found");
            return null;
        }

        public static bool CheckExistingFile(GameObject prefabVFX)
        {
#if UNITY_2018_3_OR_NEWER
            var prefabFolderPath = GetPrefabFolder2018_3(prefabVFX);
#else
            var prefabFolderPath = GetPrefabFolder(prefabVFX);
#endif
            if (prefabFolderPath != null)
            {
                if (File.Exists(prefabFolderPath + "/OriginalSettings/" + prefabVFX.name + ".dat"))
                    return true;
                return false;
            }

            return false;
        }

        private static string GetPrefabFolder(GameObject prefabVFX)
        {
#if UNITY_EDITOR
            var prefabPath = AssetDatabase.GetAssetPath(prefabVFX);
            var prefabFolderPath = Path.GetDirectoryName(prefabPath);
            return prefabFolderPath;
#else
            return null;
#endif
        }

#if UNITY_2018_3_OR_NEWER
        private static string GetPrefabFolder2018_3(GameObject prefabVFX)
        {
#if UNITY_EDITOR
            var prefabPath = PrefabStageUtility.GetPrefabStage(prefabVFX).prefabAssetPath;
            var prefabFolderPath = Path.GetDirectoryName(prefabPath);
            return prefabFolderPath;
#else
            return null;
#endif
        }
#endif

#if UNITY_2018_3_OR_NEWER
        public static void SaveNestedPrefab(GameObject prefab)
        {
#if UNITY_EDITOR
            var prefabStage = PrefabStageUtility.GetPrefabStage(prefab);
            PrefabUtility.SaveAsPrefabAsset(prefabStage.prefabContentsRoot, prefabStage.prefabAssetPath);
#endif
        }
#endif
    }
}