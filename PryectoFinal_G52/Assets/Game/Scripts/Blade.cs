using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Golpeaste al enemigo: " + other.name);
        }
    }
}