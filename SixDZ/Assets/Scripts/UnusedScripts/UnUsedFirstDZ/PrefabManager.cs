using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class PrefabManager : MonoBehaviour
{
    //[MenuItem("AssetDatabase/LoadAllAssetsAtPath")]
    static PrefabManager() // this is the static constructor of the class
    {
        EditorApplication.update += LoadPrefabs;
    }
    private static void LoadPrefabs()
    {
        Object[] data = AssetDatabase.LoadAllAssetsAtPath("Assets/MyPrefab.prefab");
        foreach (Object o in data)
        {
            Debug.Log(o);
        }
    }
}
