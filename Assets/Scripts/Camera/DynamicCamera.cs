using Unity.Cinemachine;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    private Vector2 _moveDir = new Vector2();
    private Transform _dropperTransform;
    private CinemachineCamera _cmCamera;

    private Vector3 _startingPos = Vector3.zero;
    private float _angleInRad = 0f;

    private Quaternion _initialRotation;
    private Vector3 _heightAxis;
    private Vector3 _depthAxis;
    private Vector3 _lateralAxis;
    private float _initialHeight;
    private float _initialDepth;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothingSpeed = 1.0f;
    [SerializeField] private float _maxDistFromDropper = 50.0f;

    public Vector3 FarCameraPos { get; private set; } = new Vector3();

    private void Start()
    {
        _startingPos = transform.position;
        _cmCamera = GetComponent<CinemachineCamera>();
        Debug.Assert(_cmCamera, "Script not attached to a valid CinemachineCamera object!!!", this);

        // Calculate initial orientation axes based on the camera's starting rotation
        // This makes the script work for ANY initial orientation (not just world Y/Z)
        _initialRotation = transform.rotation;
        _heightAxis = _initialRotation * Vector3.up;      // "up" direction for following the dropper
        _depthAxis = _initialRotation * Vector3.forward;  // "depth" direction for pulling the camera back
        _lateralAxis = _initialRotation * Vector3.right;  // lateral axis (unchanged, like old X)

        _initialHeight = Vector3.Dot(_startingPos, _heightAxis);
        _initialDepth = Vector3.Dot(_startingPos, _depthAxis);

        // FOV calculations (unchanged - still uses vertical FOV for the frustum math)
        float halfFOV = _cmCamera.Lens.FieldOfView / 2f;
        _angleInRad = halfFOV * Mathf.Deg2Rad;
        _moveDir.x = Mathf.Cos(_angleInRad);
        _moveDir.y = Mathf.Sin(_angleInRad);
        _moveDir.Normalize();

        FarCameraPos = transform.position;
    }

    private void FixedUpdate()
    {
        UpdateCameraPos();
    }

    public void InitDropperTransform(Transform dropperTransform)
    {
        _dropperTransform = dropperTransform;
    }

    private void UpdateCameraPos()
    {
        if (!_dropperTransform) return;

        // Project everything onto the initial orientation axes
        float cameraHeight = Vector3.Dot(transform.position, _heightAxis);
        float cameraDepth = Vector3.Dot(transform.position, _depthAxis);
        float dropperHeight = Vector3.Dot(_dropperTransform.position, _heightAxis);
        float lateral = Vector3.Dot(transform.position, _lateralAxis);

        if (dropperHeight > (cameraHeight - _offset.y))
        {
            float doubleDist = (transform.position - _dropperTransform.position).sqrMagnitude;

            float desirableHeight = dropperHeight + _offset.y;

            float farDepth = _initialDepth - ((cameraHeight - _initialHeight) / Mathf.Tan(_angleInRad));

            float desirableDepth = cameraDepth;
            if (doubleDist < _maxDistFromDropper * _maxDistFromDropper)
            {
                desirableDepth = farDepth;
            }

            // Reconstruct world position keeping lateral axis unchanged (like old X)
            Vector3 desirablePos = lateral * _lateralAxis +
                                   desirableHeight * _heightAxis +
                                   desirableDepth * _depthAxis;

            Vector3 lerpCameraPos = Vector3.Lerp(transform.position, desirablePos, _smoothingSpeed * Time.deltaTime);
            transform.position = lerpCameraPos;

            // Update FarCameraPos to use the new (lerped) height + lateral, but forced far depth
            float newCameraHeight = Vector3.Dot(transform.position, _heightAxis);
            float newLateral = Vector3.Dot(transform.position, _lateralAxis);
            FarCameraPos = newLateral * _lateralAxis +
                           newCameraHeight * _heightAxis +
                           farDepth * _depthAxis;
        }
    }
}