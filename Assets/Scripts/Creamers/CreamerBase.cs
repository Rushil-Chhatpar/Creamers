using UnityEngine;

public class CreamerBase : MonoBehaviour
{

    [SerializeField] private int _scoreValue = 1;
    [SerializeField] private float _startingDownwardsVelocity = 2.0f;
    private Rigidbody _rigidbody;

    private bool _isLanded = false;

    public static int creamerCount = 0;

    public bool IsLanded => _isLanded;
    private bool _isFalling = false;

    void Start()
    {
        _isLanded = false;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        //_rigidbody.isKinematic = true;
        //_isFalling = false;
    }

    public void Drop()
    {
        creamerCount++;
        _rigidbody.useGravity = true;
        _rigidbody.linearVelocity = new Vector3(0, -_startingDownwardsVelocity, 0);
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
        _isFalling = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CreamerBase>() || collision.gameObject.GetComponent<BasePlane>())
        {
            if (!_isLanded)
            {
                Land();
            }
        }
    }

    private void Land()
    {
        _rigidbody.useGravity = true;
        //_rigidbody.isKinematic = false;
        _isLanded = true;
        Debug.Log("SCORE!");
        //ScoreManager.Instance.ScoreEvent.Invoke(_scoreValue);
        Game.Instance.CurrentLevel.CreamerLandEventCallback();
        _isFalling = false;
        this.enabled = false;
    }

    //private void Update()
    //{
    //    if (_isFalling)
    //    {
    //        transform.position += new Vector3(0, -_startingDownwardsVelocity * Time.deltaTime, 0);
    //    }
    //}
}
