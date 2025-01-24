using UnityEngine;

public class DropperHeightTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider _heightTrigger;

    private StackDropper _parent;

    private void Start()
    {
        _parent = GetComponentInParent<StackDropper>();
        Debug.Assert(_heightTrigger, "Height Trigger not found!!!");

        ScoreManager.Instance.ScoreEvent.AddListener(ScoreEvent);
    }

    private void ScoreEvent(int score)
    {
        //CheckForTrigger();
    }

    public void CheckForTrigger()
    {
        int num = 0;
        Vector3 center = _heightTrigger.transform.TransformPoint(_heightTrigger.center);
        Vector3 halfExtents = _heightTrigger.size / 2.0f;
        Quaternion rotation = _heightTrigger.transform.rotation;

        Collider[] hitColliders = Physics.OverlapBox(center, halfExtents, rotation, ~0, QueryTriggerInteraction.Collide);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.isTrigger && hitCollider.GetComponent<CreamerBase>()?.IsLanded == true)
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
