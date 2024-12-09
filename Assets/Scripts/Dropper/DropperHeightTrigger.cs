using UnityEngine;

public class DropperHeightTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider _heightTrigger;

    private Dropper _parent;

    private void Start()
    {
        _parent = GetComponentInParent<Dropper>();
        Debug.Assert(_heightTrigger, "Height Trigger not found!!!");

        ScoreManager.Instance.ScoreEvent.AddListener(ScoreEvent);
    }

    private void ScoreEvent(int score)
    {
        //CheckForTrigger();
    }

    private void CheckForTrigger()
    {
        int num = 0;
        Vector3 center = _heightTrigger.transform.TransformPoint(_heightTrigger.center);
        Vector3 halfExtents = _heightTrigger.size / 2.0f;
        Quaternion rotation = _heightTrigger.transform.rotation;

        Collider[] hitColliders = Physics.OverlapBox(center, halfExtents, rotation);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<CreamerBase>()?.IsLanded == true)
            {
                num++;
            }
        }

        _parent.UpwardsMoveSpeedMultiplier = num;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<CreamerBase>()?.IsLanded == true)
    //    {
    //        CheckForTrigger();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<CreamerBase>()?.IsLanded == true)
    //    {
    //        CheckForTrigger();
    //    }
    //}
}
