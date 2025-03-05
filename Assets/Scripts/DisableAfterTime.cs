using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    [SerializeField]
    private float _time;
    [SerializeField]
    bool _enableOnStart = false;

    private float _elapsed = 0f;
    private bool _ticking = false;

    private void Start()
    {
        _ticking = _enableOnStart;
    }

    public void BeginTimer()
    {
        _ticking = true;
    }
    void Update()
    {
        if (!_ticking)
            return;
        _elapsed += Time.deltaTime;
        if( _elapsed >= _time )
        {
            this.gameObject.SetActive( false );
        }
    }
}
