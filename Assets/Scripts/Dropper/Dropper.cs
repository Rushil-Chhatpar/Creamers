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
    //[SerializeField] protected List<GameObject> _creamerPrefabs;

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
    }

    public void SaveData(ref GameData data)
    {
    }
}
