using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] protected GameModeBase _gameMode;
    [SerializeField] protected Transform _dropperTransform;

    protected GameObject _dropper;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _dropper = _gameMode.DropperInitialize(_dropperTransform);
    }

    public void CreamerLandEventCallback()
    {
        _gameMode.CreamerLandCallback();
    }

    public void SetCreamerSet(int creamerSetID)
    {
        _dropper.GetComponent<Dropper>().SetCreamerSet(creamerSetID);
    }
}
