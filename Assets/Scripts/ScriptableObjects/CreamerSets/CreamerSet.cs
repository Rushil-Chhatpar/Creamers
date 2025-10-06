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
    [ReadOnly] private int _uniqueID = -1;

    public void AssignUniqueID(int id)
    {
        if (UniqueId == -1)
            UniqueId = id;
    }

    public void Purchase()
    {
        Game.Instance.CurrentLevel.SetCreamerSet(UniqueId);
        CurrencyManager.Instance.PurchaseItem(UniqueId);
    }
}