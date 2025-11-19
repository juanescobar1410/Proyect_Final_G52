using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    public int healAmount = 30;   // Cantidad de vida que cura

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si quien entró es el jugador y tiene el script PlayerAttack
        PlayerAttack player = other.GetComponent<PlayerAttack>();

        if (player != null)
        {
            // Curar sin pasar del máximo
            player.currentHP = Mathf.Min(player.currentHP + healAmount, player.maxHP);

            // Mensaje opcional
            Debug.Log("Jugador curado + " + healAmount);

            // Destruir la botella después de usarla
            Destroy(gameObject);
        }
    }
}
