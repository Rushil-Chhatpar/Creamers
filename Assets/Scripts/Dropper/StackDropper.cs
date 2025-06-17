using UnityEngine;
using UnityEngine.Events;

public class StackDropper : Dropper
{
    private readonly UnityEvent _tapButtonPressed = new UnityEvent();

    //[SerializeField] private GameObject _creamerPrefabs;

    private float _heightToClimb = 0;

    private Vector3 _movePos;
    private Vector3 _startPos;
    [SerializeField] private float _moveDist = 1;
    [SerializeField, Range(0.0f, 0.1f)] private float _speedIncreaaseRate = 0.02f;

    [SerializeField] private bool _useSin = true;
    [ShowIf("_useSin", true)][SerializeField, Range(0.001f, 1.5f)] private float _sinMoveSpeed = 1;
    [ShowIf("_useSin", false)][SerializeField, Range(0.001f, 3f)] private float _nonSinMoveSpeed = 0.7f;

    private float _leftX = 0, _rightX = 0;
    private int _horizontalDir = 1;

    [SerializeField] private float _verticalMoveSpeed = 1;

    private DropperHeightTrigger _trigger;

    private GameObject _currentCreamer = null;

    private int _upwardsMoveMultiplier = 0;
    public int UpwardsMoveSpeedMultiplier
    {
        get => _upwardsMoveMultiplier;

        set
        {
            if (value < 0)
            {
                _upwardsMoveMultiplier = 0;
            }
            else
            {
                _upwardsMoveMultiplier = value;
            }
        }
    }

    private void Start()
    {
        base.Start();
        _startPos = transform.position;
        _tapButtonPressed.AddListener(TapButtonPressedAction);
        _trigger = GetComponentInChildren<DropperHeightTrigger>();

        // Debug.Assert(_creamerPrefabs.Count > 0, "Cannot find gameobject: Creamer!!!");
        Debug.Assert(_creamerSet._creamerPrefabs.Count > 0, "Cannot find gameobject: Creamer!!!", this);
        SpawnAtBase();
        //ScoreManager.Instance.ScoreEvent.AddListener(ScoreEvent);
        //Game.GameOverEvent.AddListener(GameOver);

        BoxCollider collider = _creamerSet._creamerPrefabs[0].GetComponent<BoxCollider>();

        if (collider != null)
        {
            //_heightToClimb = collider.size.y;
            Vector3 worldSize = Vector2.Scale(collider.size, collider.transform.lossyScale);
            _heightToClimb = worldSize.y;
        }

        float X = transform.position.x;
        _leftX = X - _moveDist;
        _rightX = X + _moveDist;
    }

    protected override void ScoreEvent(int score)
    {
        _trigger.CheckForTrigger();
        //float newY = transform.position.y + (_heightToClimb * UpwardsMoveSpeedMultiplier);
        //transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        SpawnAtBase();
        AdjustDropperMovement();
    }

    protected override void GameOver()
    {
        ScoreManager.Instance.ScoreEvent.RemoveListener(ScoreEvent);
    }

    private void SpawnAtBase()
    {
        GameObject creamer = Instantiate(_creamerSet._creamerPrefabs[_creamerIndex], transform.position, Quaternion.identity, transform);

        // TODO: Fix the model default angle
        creamer.transform.rotation = Quaternion.Euler(-90,0,0);
        // __________________________________________________________________
        _currentCreamer = creamer;
        _creamerIndex = (_creamerIndex + 1) % _creamerSet._creamerPrefabs.Count;
    }

    void FixedUpdate()
    {
        // TODO: Need a better fix for this 
        if (UpwardsMoveSpeedMultiplier > 0)
        {
            _trigger.CheckForTrigger();
        }
        // left and right
        if (_useSin)
        {
            _movePos.x = _startPos.x + Mathf.Sin(Time.time * _sinMoveSpeed) * _moveDist;
        }
        else
        {
            _movePos.x = transform.position.x + (_horizontalDir * Time.fixedDeltaTime * _nonSinMoveSpeed);
            if (_movePos.x > _rightX || _movePos.x < _leftX)
            {
                _horizontalDir *= -1;
                //_movePos.x = _horizontalDir * _moveDist;
            }
        }

        // up
        //if (UpwardsMoveSpeedMultiplier > 0)
        //{
        //    _movePos.y = transform.position.y + _heightToClimb;
        //    UpwardsMoveSpeedMultiplier = 0;
        //}
        _movePos.y = transform.position.y + (_verticalMoveSpeed * Time.fixedDeltaTime * UpwardsMoveSpeedMultiplier);

        transform.position = new Vector3(_movePos.x, _movePos.y, _startPos.z);
    }

    public void TapButtonPressed()
    {
        _tapButtonPressed.Invoke();
    }

    private void TapButtonPressedAction()
    {
        _currentCreamer.transform.SetParent(null);
        _currentCreamer.GetComponent<CreamerBase>().Drop();
        //GameObject creamer = Instantiate(_creamerPrefabs, transform.position, Quaternion.identity);
    }

    private void AdjustDropperMovement()
    {
        if (_useSin)
        {
            _sinMoveSpeed += _sinMoveSpeed * _speedIncreaaseRate;
        }
        else
        {
            _nonSinMoveSpeed += _nonSinMoveSpeed * _speedIncreaaseRate;
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                // left
                transform.position = new Vector3(_leftX, transform.position.y, transform.position.z);
                _horizontalDir = 1;
            }
            else
            {
                transform.position = new Vector3(_rightX, transform.position.y, transform.position.z);
                _horizontalDir = -1;
            }
        }
    }
}
