using UnityEngine;

[System.Serializable]
public class GameData
{
    public int HighScore;
    public int CreamerSetID;

    public GameData()
    {
        this.HighScore = 0;
        this.CreamerSetID = CreamerSet.DEFAULT_ID;
    }
}
