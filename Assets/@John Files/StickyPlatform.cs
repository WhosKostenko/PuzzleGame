using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            other.transform.root.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            other.transform.root.SetParent(null);
        }
    }
    
}
