using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum TipoObjeto { Cura, Moneda }
    public TipoObjeto tipo;

    public int healAmount = 30;
    public int coinAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        PlayerAttack player = other.GetComponent<PlayerAttack>();

        if (player != null)
        {
            switch (tipo)
            {
                case TipoObjeto.Cura:
                    player.currentHP = Mathf.Min(player.currentHP + healAmount, player.maxHP);

                    // Actualizar UI
                    if (player.vidaText != null)
                        player.vidaText.text = "HP: " + player.currentHP;

                    break;

                case TipoObjeto.Moneda:
                    player.monedaTotal += coinAmount;

                    // Actualizar UI
                    if (player.monedasText != null)
                        player.monedasText.text = " " + player.monedaTotal;

                    break;
            }

            Destroy(gameObject);
        }
    }
}
