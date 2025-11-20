using UnityEngine;

/// <summary>
/// Maneja el comportamiento de los objetos coleccionables:
/// - Puede ser una cura o una moneda.
/// - Al entrar en contacto con el jugador:
///   - Cura suma vida hasta el máximo permitido.
///   - Moneda aumenta el total de monedas del jugador.
/// - Después de aplicar su efecto, el objeto se destruye.
/// </summary>
public class Collectables : MonoBehaviour
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
