using Unity.Cinemachine;
using UnityEngine;

public class FortLevel : Level
{
    private FortDropper _fortDropper;
    private CinemachineBrain _cinemachineBrain;
    private void Start()
    {
        base.Start();
        _fortDropper = _dropper.GetComponent<FortDropper>();
        _cinemachineBrain = FindFirstObjectByType<CinemachineBrain>();
    }

    public void UpdateDropperPosition(Vector2 screenPos)
    {
        Camera outputCamera = _cinemachineBrain.OutputCamera;
        float camZ = _dropperTransform.position.z - outputCamera.transform.position.z;
        Vector3 sPos = new Vector3(screenPos.x,Screen.height - screenPos.y, camZ);
        Vector3 worldPoint = outputCamera.ScreenToWorldPoint(sPos);
        worldPoint.z = _dropperTransform.position.z;
        //Debug.Log("Position: " + worldPoint);
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
