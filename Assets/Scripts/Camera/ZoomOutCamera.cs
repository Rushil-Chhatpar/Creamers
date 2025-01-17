using UnityEngine;

public class ZoomOutCamera : MonoBehaviour
{

    [SerializeField] private DynamicCamera _dynamicCamera;
    [SerializeField] private FortCamera _fortCamera;

    private enum levelType
    {
        Stack,
        Fort
    }

    private levelType _levelType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Game.Instance.CurrentLevel as StackLevel)
        {
            if (!_dynamicCamera)
                _dynamicCamera = FindFirstObjectByType<DynamicCamera>();
            Debug.Assert(_dynamicCamera, "No Dynamic Camera in the scene!!!", this);
            _levelType = levelType.Stack;
        }
        else if (Game.Instance.CurrentLevel as FortLevel)
        {
            if(!_fortCamera)
                _fortCamera = FindFirstObjectByType<FortCamera>();
            Debug.Assert(_fortCamera, "No Fort Camera in the scene!!!", this);
            _levelType = levelType.Fort;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_levelType == levelType.Stack)
            transform.position = _dynamicCamera.FarCameraPos;
        else if (_levelType == levelType.Fort)
            transform.position = _fortCamera.FarCameraPos;
    }
}