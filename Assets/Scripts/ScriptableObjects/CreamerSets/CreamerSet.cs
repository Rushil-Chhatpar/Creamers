using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "CreamerSet")]
public class CreamerSet : ScriptableObject, IPurchasable
{
    public List<GameObject> _creamerPrefabs;
    public int _cost;
    public Texture2D _setIcon;

    public static readonly int DEFAULT_ID = 2;
    public int UniqueId
    {
        get { return _uniqueID; }
        private set { _uniqueID = value; }
    }
    public int Cost => _cost;

    // TODO: Disable this later after development
    [ReadOnly] public int _uniqueID = -1;

    public void AssignUniqueID(int id)
    {
        if (UniqueId == -1)
            UniqueId = id;
    }

    public void Purchase()
    {
        Game.Instance.CurrentLevel.SetCreamerSet(UniqueId);
    }
}


#if UNITY_EDITOR
[InitializeOnLoad]
public class CreamerSetIDAssigner
{
    private const string KEY_ID_COUNTER = "CreamerSetIDCounter";

    static CreamerSetIDAssigner()
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
        string[] guids = AssetDatabase.FindAssets("t:CreamerSet");
        int idCounter = EditorPrefs.GetInt(KEY_ID_COUNTER, CreamerSet.DEFAULT_ID);

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            CreamerSet set = AssetDatabase.LoadAssetAtPath<CreamerSet>(path);
            if (set && set.UniqueId == -1)
            {
                set.AssignUniqueID(idCounter);
                idCounter++;
                EditorUtility.SetDirty(set);
            }
        }

        EditorPrefs.SetInt(KEY_ID_COUNTER, idCounter);
        AssetDatabase.SaveAssets();
    }
}

#endif