using UnityEngine;

public class ZoomOutCamera : MonoBehaviour
{

    [SerializeField] private DynamicCamera _dynamicCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_dynamicCamera)
            _dynamicCamera = FindFirstObjectByType<DynamicCamera>();
        Debug.Assert(_dynamicCamera, "No Dynamic Camera in the scene!!!", this);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = _dynamicCamera.FarCameraPos;
    }
}