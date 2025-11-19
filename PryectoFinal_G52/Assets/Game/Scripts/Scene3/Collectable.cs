using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum TipoObjeto { Cura, Moneda }
    public TipoObjeto tipo;

    public int healAmount = 30;
    public int coinAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Buscar el script del jugador
        PlayerAttack player = other.GetComponent<PlayerAttack>();

        if (player != null)
        {
            switch (tipo)
            {
                case TipoObjeto.Cura:
                    player.currentHP = Mathf.Min(player.currentHP + healAmount, player.maxHP);
                    Debug.Log("Jugador curado +" + healAmount);
                    break;

                case TipoObjeto.Moneda:
                    player.monedaTotal += coinAmount;
                    Debug.Log("Moneda recogida. Total = " + player.monedaTotal);
                    break;
            }

            Destroy(gameObject);
        }
    }
}
