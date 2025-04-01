using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraType
    {
        Dynamic,
        ZoomOut
    };

    private int _currentPriority = 1;
    StackLevel _level;

    private CinemachineCamera _dynamicCamera;
    private CinemachineCamera _zoomOutCamera;

    void Start()
    {
        _level = Game.Instance.CurrentLevel as StackLevel;
        _currentPriority = _level.InitialCamPriority + 10;

        DynamicCamera dynamicCamera = FindFirstObjectByType<DynamicCamera>();
        if (dynamicCamera)
        {
            _dynamicCamera = dynamicCamera.GetComponent<CinemachineCamera>();
        }
        ZoomOutCamera zoomOutCamera = FindFirstObjectByType<ZoomOutCamera>();
        if (zoomOutCamera)
        {
            _zoomOutCamera = zoomOutCamera.GetComponent<CinemachineCamera>();
        }
    }

    public void SetCamera(CameraType cameraType)
    {
        _currentPriority++;
        if (cameraType == CameraType.Dynamic)
        {
            _dynamicCamera.Priority = _currentPriority;
        }
        else if (cameraType == CameraType.ZoomOut)
        {
            _zoomOutCamera.Priority = _currentPriority;
        }
    }

}
