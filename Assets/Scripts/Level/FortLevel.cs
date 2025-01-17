using Unity.Cinemachine;
using UnityEngine;

public class FortLevel : Level
{
    private FortDropper _fortDropper;
    private CinemachineBrain _cinemachineBrain;
    private float _dropperSensitivity;
    private void Start()
    {
        base.Start();
        _fortDropper = _dropper.GetComponent<FortDropper>();
        _dropperSensitivity = _fortDropper.DropperSensitivity * -1.0f;
        _cinemachineBrain = FindFirstObjectByType<CinemachineBrain>();
    }

    public void UpdateDropperPosition(Vector2 screenPos)
    {
        Camera outputCamera = _cinemachineBrain.OutputCamera;
        float camZ = (outputCamera.transform.position.z - _dropperTransform.position.z) * 2.0f;
        Vector3 sPos = new Vector3(screenPos.x, outputCamera.pixelHeight - screenPos.y, outputCamera.transform.position.z * _dropperSensitivity);
        Vector3 worldPoint = outputCamera.ScreenToWorldPoint(sPos);
        worldPoint.z = _dropperTransform.position.z;
        Debug.Log("Position: " + worldPoint);
        _fortDropper.UpdatePosition(worldPoint);
    }

    public void SpawnCreamer()
    {
        _fortDropper.SpawnAtBase();
    }

    public void ReleaseCreamer()
    {
        _fortDropper.ReleaseCreamer();
    }
}
