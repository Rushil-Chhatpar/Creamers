using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int HighScore;
    public int CreamerSetID;
    public int CurrencyPoints;
    // TODO: Convert hashset to List for saving. need serializable data type for it
    public List<int> PurchasedItems;

    public GameData()
    {
        this.HighScore = 0;
        this.CreamerSetID = CreamerSet.DEFAULT_ID;
        this.CurrencyPoints = 0;
    }
}
