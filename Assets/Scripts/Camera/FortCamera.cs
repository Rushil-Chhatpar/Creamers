using Unity.Cinemachine;
using UnityEngine;

public class FortCamera : MonoBehaviour
{
    private Vector2 _moveDir = new Vector2();
    private Transform _dropperTransform;
    private CinemachineCamera _cmCamera;

    private Vector3 _startingPos = Vector3.zero;
    private float _angleInRad = 0;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothingSpeed = 1.0f;
    [SerializeField] private float _maxDistFromDropper = 50.0f;

    public Vector3 FarCameraPos { get; private set; } = new Vector3();

    private void Start()
    {
        _startingPos = transform.position;
        //_dropperTransform = FindFirstObjectByType<Dropper>().gameObject.transform;
        //Debug.Assert(_dropperTransform != null, "No Dropper found in the scene!!!", this);
        _cmCamera = GetComponent<CinemachineCamera>();
        Debug.Assert(_cmCamera, "Script not attached to a valid cinemachine camera object!!!", this);

        float halfFOV = _cmCamera.Lens.FieldOfView / 2;
        _angleInRad = halfFOV * Mathf.Deg2Rad;
        _moveDir.x = Mathf.Cos(_angleInRad);
        _moveDir.y = Mathf.Sin(_angleInRad);
        _moveDir.Normalize();
        FarCameraPos = transform.position;
    }

    private void FixedUpdate()
    {
        if(!_dropperTransform)
            _dropperTransform = FindFirstObjectByType<Dropper>().gameObject.transform;
        UpdateCameraPos();
    }

    private void UpdateCameraPos()
    {
        if (_dropperTransform.position.y > (transform.position.y - _offset.y))
        {
            float doubleDist = (transform.position - _dropperTransform.position).sqrMagnitude;

            float desirableY = _dropperTransform.position.y + _offset.y;
            float distMultiplier = (desirableY - transform.position.y) / _moveDir.y;
            float farZ = _startingPos.z - ((transform.position.y - _startingPos.y) / Mathf.Tan(_angleInRad));
            ////float farZ = transform.position.z - (_moveDir.x * distMultiplier);
            //float desirableZ = transform.position.z;

            //if (doubleDist < _maxDistFromDropper * _maxDistFromDropper)
            //{
            //    desirableZ = farZ;
            //}

            //Vector3 desirablePos = new Vector3(transform.position.x, desirableY, desirableZ);
            //Vector3 lerpCameraPos = Vector3.Lerp(transform.position, desirablePos, _smoothingSpeed * Time.deltaTime);
            //transform.position = lerpCameraPos;

            //// set the FarCameraPos
            FarCameraPos = new Vector3(transform.position.x, transform.position.y, farZ);
        }
    }
}
