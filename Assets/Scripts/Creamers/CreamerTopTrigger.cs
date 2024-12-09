using UnityEngine;

public class CreamerTopTrigger : MonoBehaviour
{
    [SerializeField] private MeshCollider _collider;
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<CreamerBottomTrigger>())
        {
            Debug.Log("Top collided with Bottom");
        }
    }

}
