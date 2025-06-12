using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Stateless;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public abstract class Dropper : MonoBehaviour, IDataPersistence
{
    [SerializeField] protected CreamerSet _creamerSet;

    protected int _creamerIndex = 0;

    protected void Start()
    {
        ScoreManager.Instance.ScoreEvent.AddListener(ScoreEvent);
        Game.GameOverEvent.AddListener(GameOver);
    }

    protected abstract void ScoreEvent(int score);

    protected abstract void GameOver();

    public void LoadData(GameData data)
    {
        int id = data.CreamerSetID;
        string[] guids = AssetDatabase.FindAssets("t:CreamerSet");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            CreamerSet set = AssetDatabase.LoadAssetAtPath<CreamerSet>(path);
            if (set.UniqueId == id)
            {
                _creamerSet = set;
                return;
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data.CreamerSetID = _creamerSet.UniqueId;
    }

    public void SetCreamerSet(int cremaerSetID)
    {
        string[] guids = AssetDatabase.FindAssets("t:CreamerSet");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            CreamerSet set = AssetDatabase.LoadAssetAtPath<CreamerSet>(path);
            if (set.UniqueId == cremaerSetID)
            {
                _creamerSet = set;
                return;
            }
        }
    }
}
