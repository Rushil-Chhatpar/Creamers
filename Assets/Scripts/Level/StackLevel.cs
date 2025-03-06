using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class StackLevel : Level
{
    public readonly int InitialCamPriority = 10;
    private StackDropper _stackDropper;
    private CinemachineCamera _dynamicCam;

    [SerializeField]
    private List<Rigidbody> _staticCreamers;

    private Vector3 _explosionForce;

    private float _blendTime = 2f;
    private void Start()
    {
        base.Start();
        _stackDropper = _dropper.GetComponent<StackDropper>();
        _stackDropper.gameObject.SetActive(false);
        DynamicCamera dynamicCamera = FindFirstObjectByType<DynamicCamera>();
        dynamicCamera.InitDropperTransform(_stackDropper.gameObject.transform);
        _dynamicCam = dynamicCamera.GetComponent<CinemachineCamera>();

        _blendTime = FindFirstObjectByType<CinemachineBrain>().DefaultBlend.Time;
        _explosionForce = new Vector3(0, 0, 50.0f);
    }

    public void InitialSequence()
    {
        foreach (Rigidbody body in _staticCreamers)
        {
            body.AddForce(_explosionForce, ForceMode.Impulse);
            body.GetComponent<DisableAfterTime>().BeginTimer();
        }
        StartCoroutine(InitialSequenceBeginTimer(_blendTime));
    }

    public void TapButtonPressed()
    {
        _stackDropper.TapButtonPressed();
    }

    private IEnumerator InitialSequenceBeginTimer(float time)
    {
        _dynamicCam.Priority = InitialCamPriority;
        yield return new WaitForSeconds(time);
        _stackDropper.gameObject.SetActive(true);
        ScreenManager.Instance.ViewScreen<MainHUDScreen>();
    }
}
