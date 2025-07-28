using UnityEngine;

[System.Serializable]
public class GameData
{
    public int HighScore;
    public int CreamerSetID;
    public int CurrencyPoints;

    public GameData()
    {
        this.HighScore = 0;
        this.CreamerSetID = CreamerSet.DEFAULT_ID;
        this.CurrencyPoints = 0;
    }
}
