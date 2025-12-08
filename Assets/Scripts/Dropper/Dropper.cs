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

        // TODO: get from the purchased creamer sets not all sets
        foreach (CreamerSet set in Game.Instance.CreamerSets)
        {
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

    public virtual void SetCreamerSet(int cremaerSetID)
    {
        foreach (CreamerSet set in Game.Instance.CreamerSets)
        {
            if (set.UniqueId == cremaerSetID)
            {
                _creamerSet = set;
                return;
            }
        }
    }
}
