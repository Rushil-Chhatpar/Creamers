using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public interface IPurchasable
{
    public abstract void Purchase();
    public int UniqueId { get; }
    public int Cost { get; }

    public abstract void AssignUniqueID(int id);

    public static readonly int DEFAULT_ID = 2;
}

#if UNITY_EDITOR
[InitializeOnLoad]
public class PurchasableUniqueIDAssigner
{
    private const string KEY_ID_COUNTER = "PurchasableIDCounter";

    static PurchasableUniqueIDAssigner()
    {
        AssetModificationProcessor processor = new CustomAssetModificationProcessor();
    }

    private class CustomAssetModificationProcessor : AssetModificationProcessor
    {
        static void OnWillCreateAsset(string assetPath)
        {
            AssignIDsToSets();
        }
        // static string OnWillSaveAssets(string[] paths)
        // {
        //     AssignIDsToSets();
        //     return null;
        // }
    }

    private static void AssignIDsToSets()
    {
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");
        int idCounter = EditorPrefs.GetInt(KEY_ID_COUNTER, IPurchasable.DEFAULT_ID);

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            if (so && so is IPurchasable purchasable)
            {
                purchasable.AssignUniqueID(idCounter);
                idCounter++;
                EditorUtility.SetDirty(so);
            }
        }

        EditorPrefs.SetInt(KEY_ID_COUNTER, idCounter);
        AssetDatabase.SaveAssets();
    }
}

#endif