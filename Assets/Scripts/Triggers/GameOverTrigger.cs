using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        Debug.Assert(_boxCollider, "No Collider Found!!!", this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CreamerBase>())
        {
            if (CreamerBase.creamerCount > 1)
            {
                Debug.Log("GAME OVER!!!");
                Game.GameOverEvent.Invoke();
            }
        }
    }
}
