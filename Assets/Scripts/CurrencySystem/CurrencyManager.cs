using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour, IDataPersistence
{
    private static CurrencyManager _instance;

    public int CurrencyPoints { get; private set; }
    private HashSet<int> _purchasedItems = new HashSet<int>();


    // Class specific items
    private List<CreamerSet> _purchasedCreamerSets = new List<CreamerSet>();
    //

    public static CurrencyManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindFirstObjectByType<CurrencyManager>();
                if (!_instance)
                {
                    GameObject currencyManagerObject = new GameObject("CurrencyManager");
                    _instance = currencyManagerObject.AddComponent<CurrencyManager>();
                    DontDestroyOnLoad(currencyManagerObject);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadData(GameData data)
    {
        this.CurrencyPoints = data.CurrencyPoints;
        this._purchasedItems = new HashSet<int>(data.PurchasedItems);

        _purchasedCreamerSets.Clear();
        foreach (CreamerSet set in Game.Instance.CreamerSets)
        {
            if (_purchasedItems.Contains(set.UniqueId))
            {
                _purchasedCreamerSets.Add(set);
            }
            // do the same for every type of items to hold a record
        }
    }

    public void SaveData(ref GameData data)
    {
        data.CurrencyPoints = this.CurrencyPoints;

        // convert HashSet to List
        data.PurchasedItems = new List<int>(_purchasedItems);
    }

    public void PurchaseItem(int UniqueID)
    {
        _purchasedItems.Add(UniqueID);
    }

    public ref readonly List<CreamerSet> GetPurchasedCreamerSets()
    {
        return ref _purchasedCreamerSets;
    }
}
