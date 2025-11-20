using UnityEngine;

public class PanelTrigger : MonoBehaviour
{
    /// <summary>
    /// Controla la activación de un panel de UI cuando el jugador entra en un área con un trigger.
    /// 
    /// Al iniciar, el panel asignado se mantiene desactivado.
    /// Cuando un objeto con tag "Player" entra al trigger, el panel se activa.
    /// Si la opción freezePlayer está habilitada, se pausa el juego usando Time.timeScale = 0.
    /// 
    /// El método ClosePanel() permite cerrar el panel desde un botón en la UI
    /// y, si el juego estaba pausado, lo reanuda.
    /// 
    /// Este script se usa para mostrar avisos, instrucciones o mensajes cuando el jugador
    /// ingresa a una zona específica dentro del nivel.
    /// </summary>


    [Header("Panel que se activará")]
    public GameObject uiPanel;

    [Header("Opcional: bloquear al player mientras el panel está activo")]
    public bool freezePlayer = false;

    private void Start()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(true);

            if (freezePlayer)
                Time.timeScale = 0f;  // pausa el juego si quieres

            Debug.Log("Panel activado por colisión con el Player");
        }
    }

    // Llamado desde el botón del panel
    public void ClosePanel()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);

        if (freezePlayer)
            Time.timeScale = 1f; // reanuda el juego

        Debug.Log("Panel cerrado");
    }
}