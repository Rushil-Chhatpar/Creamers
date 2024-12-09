using UnityEngine;

public class CreamerBottomTrigger : MonoBehaviour
{
    [SerializeField] private MeshCollider _collider;
    void Start()
    {
        if (_collider)
        {
            
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<CreamerTopTrigger>())
        {
            Debug.Log("Bottom collided with Top");
        }
    }
}
