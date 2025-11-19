using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public float damage = 100f; // Daño que hace la espada
    private GameObject player; // Referencia al jugador para ignorarlo

    private void Start()
    {
        // Buscar al jugador para ignorar colisiones con él
        player = GameObject.FindGameObjectWithTag("Player");

        // Verificar que el collider sea trigger
        Collider col = GetComponent<Collider>();
        if (col != null && !col.isTrigger)
        {
            Debug.LogWarning("ADVERTENCIA: El collider de " + gameObject.name + " NO es Trigger. Actívalo en el Inspector!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // IGNORAR al propio jugador
        if (other.gameObject == player || other.CompareTag("Player"))
        {
            return; // Sale de la función sin hacer nada
        }

        Debug.Log("¡Colisión detectada con: " + other.name + " | Tag: " + other.tag);

        // Daño a enemigos normales
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Golpeaste al enemigo: " + other.name);

            // Si el enemigo tiene un script de vida, le hace daño
            // Ejemplo: other.GetComponent<Enemy>().TakeDamage(damage);
        }

        // Daño al Boss
        if (other.CompareTag("Boss"))
        {
            Debug.Log("¡Golpeaste al Boss!");

            // Obtener el componente Boss y restarle vida
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.Hp_min -= damage;
                Debug.Log("Boss HP: " + boss.Hp_min);
            }
            else
            {
                Debug.LogError("El objeto con tag Boss no tiene el componente Boss!");
            }
        }
    }
}