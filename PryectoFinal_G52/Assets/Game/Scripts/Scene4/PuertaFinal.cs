using UnityEngine;
using UnityEngine.UI;

public class PuertaFinal : MonoBehaviour
{
    [Header("Estado de la puerta")]
    public bool puertaBloqueada = true;

    [Header("Referencias")]
    public GameObject panelVictoria; // Panel que aparecerá al ganar
   

    [Header("Visual")]
    public Color colorBloqueado = Color.red;
    public Color colorDesbloqueado = Color.green;


    private bool playerCerca = false;
    private Renderer puertaRenderer;

    void Start()
    {
        // Asegurarse de que el panel esté oculto al inicio
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }


        // Configurar visual inicial
        puertaRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Si el player está cerca y presiona E
        if (playerCerca )
        {
            if (!puertaBloqueada)
            {
                AbrirPuerta();
            }
            else
            {
                Debug.Log("La puerta está bloqueada. Derrota al Boss primero.");
            }
        }
    }

    // Método para desbloquear la puerta (será llamado desde el Boss)
    public void DesbloquearPuerta()
    {
        puertaBloqueada = false;
        Debug.Log("¡Puerta desbloqueada!");

    }

    void AbrirPuerta()
    {
        Debug.Log("¡Has ganado!");

        // Mostrar panel de victoria
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
            Time.timeScale = 0f; // Pausar el juego
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = false;

        }
    }
}