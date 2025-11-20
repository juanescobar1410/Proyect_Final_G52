using UnityEngine;

/// <summary>
/// Maneja objetos coleccionables del juego.
/// - Cura: aumenta la vida del jugador y actualiza la UI.
/// - Moneda: suma monedas al jugador y agrega puntaje al GameManager.
/// Cada objeto se destruye automáticamente tras ser recogido.
/// </summary>


public class Collectable : MonoBehaviour
{
    public enum TipoObjeto { Cura, Moneda }
    public TipoObjeto tipo;

    public int healAmount = 30;
    public int coinAmount = 1;
    public int coinValue = 10;

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

                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.AddItem();
                    }

                    UIManager uiManager = FindObjectOfType<UIManager>();
                    if (uiManager != null)
                    {
                        uiManager.ActualizarScore();
                    }

                    break;

                case TipoObjeto.Moneda:
                    player.monedaTotal += coinAmount;

                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.AddScore(coinValue);
                        Debug.Log($"Score actual: {GameManager.Instance.Score}");
                    }

                    // Actualizar UI
                    if (player.monedasText != null)
                        player.monedasText.text = " " + player.monedaTotal;

                    break;
            }

            Destroy(gameObject);
        }
    }
}
