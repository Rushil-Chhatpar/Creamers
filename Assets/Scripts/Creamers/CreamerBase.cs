using UnityEngine;

public class CreamerBase : MonoBehaviour
{

    [SerializeField] private int _scoreValue = 1;
    [SerializeField] private float _startingDownwardsVelocity = 2.0f;
    private Rigidbody _rigidbody;

    private bool _isLanded = false;

    public static int creamerCount = 0;

    public bool IsLanded => _isLanded;

    void Start()
    {
        _isLanded = false;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    public void Drop()
    {
        creamerCount++;
        _rigidbody.useGravity = true;
        _rigidbody.linearVelocity = new Vector3(0, -_startingDownwardsVelocity, 0);
        BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider boxCollider in colliders)
        {
            boxCollider.enabled = true;
        }
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
        _isLanded = true;
        Debug.Log("SCORE!");
        ScoreManager.Instance.ScoreEvent.Invoke(_scoreValue);
    }
}
