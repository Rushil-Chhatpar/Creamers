using Unity.Cinemachine;
using UnityEngine;

public class FallDetectionTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;

    private StackLevel _level;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!_boxCollider)
            _boxCollider = GetComponent<BoxCollider>();
        Debug.Assert(_boxCollider, "No Collider Found!!!", this);
        _level = Game.Instance.CurrentLevel as StackLevel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CreamerBase>())
        {
            _level.SetCamera(CameraController.CameraType.ZoomOut);
        }
    }
}
