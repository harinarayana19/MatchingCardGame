using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PrefabImageCleaner : EditorWindow
{
    [MenuItem("Tools/Prefab Image Cleaner")]
    public static void ShowWindow()
    {
        GetWindow<PrefabImageCleaner>("Prefab Image Cleaner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Scan and Fix Extra Images in Card Prefabs", EditorStyles.boldLabel);

        if (GUILayout.Button("Scan Prefabs"))
        {
            ScanPrefabs();
        }
    }

    private void ScanPrefabs()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        int fixedCount = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null && prefab.name.ToLower().Contains("card")) // Only check prefabs with 'card' in the name
            {
                Image[] images = prefab.GetComponentsInChildren<Image>(true);
                if (images.Length > 2)
                {
                    Debug.LogWarning($"Prefab '{prefab.name}' has {images.Length} Image components. Fixing...");

                    // Keep only the first two (assume Front and Back)
                    for (int i = 2; i < images.Length; i++)
                    {
                        DestroyImmediate(images[i], true);
                    }

                    PrefabUtility.SavePrefabAsset(prefab);
                    fixedCount++;
                }
            }
        }

        Debug.Log($"Scan Complete. Fixed {fixedCount} prefabs.");
    }
}