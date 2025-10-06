using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour, IDataPersistence
{
    private static CurrencyManager _instance;

    public int CurrencyPoints { get; private set; }
    private HashSet<int> _purchasedItems = new HashSet<int>();

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
    }

    public void SaveData(ref GameData data)
    {
        data.CurrencyPoints = this.CurrencyPoints;

        // convert HashSet to List
        data.PurchasedItems = new List<int>(_purchasedItems);
    }

    public void PurchaseItem(int UniqueID)
    {

    }

    public List<CreamerSet> GetPurchasedCreamerSets()
    {
        List<CreamerSet> set = new List<CreamerSet>();

        // TODO: https://tcmstudios.atlassian.net/browse/SCRUM-171?focusedCommentId=10115

        return set;
    }
}
