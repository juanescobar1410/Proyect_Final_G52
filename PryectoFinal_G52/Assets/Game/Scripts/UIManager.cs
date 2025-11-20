using UnityEngine;
using UnityEngine.UI;
using TMPro; // Si usas TextMeshPro

public class UIManager : MonoBehaviour
{
    [Header("UI en Juego")]
    public Text scoreText; // Si usas UI Text normal
    public TextMeshProUGUI scoreTextTMP; // Si usas TextMeshPro
    public Text tiempoText;
    public TextMeshProUGUI tiempoTextTMP;

    [Header("Panel de Victoria")]
    public GameObject panelVictoria;
    public Text scoreFinalText;
    public TextMeshProUGUI scoreFinalTextTMP;
    public Text tiempoFinalText;
    public TextMeshProUGUI tiempoFinalTextTMP;
    public Text itemsFinalText;
    public TextMeshProUGUI itemsFinalTextTMP;

    private float tiempoEscena = 0f;

    void Start()
    {
        // Asegurarse de que el panel de victoria esté oculto
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }

        // Actualizar UI inicial
        ActualizarScore();
        ActualizarTiempo();
    }

    void Update()
    {
        // Contar tiempo de la escena actual
        tiempoEscena += Time.deltaTime;
        ActualizarTiempo();
    }

    public void ActualizarScore()
    {
        if (GameManager.Instance != null)
        {
            int score = GameManager.Instance.Score;

            // Actualizar texto normal
            if (scoreText != null)
                scoreText.text = "Score: " + score;

            // Actualizar TextMeshPro
            if (scoreTextTMP != null)
                scoreTextTMP.text = "Score: " + score;
        }
    }

    void ActualizarTiempo()
    {
        if (GameManager.Instance != null)
        {
            float tiempoTotal = GameManager.Instance.GlobalTime + tiempoEscena;
            string tiempoFormateado = FormatearTiempo(tiempoTotal);

            // Actualizar texto normal
            if (tiempoText != null)
                tiempoText.text = "Tiempo: " + tiempoFormateado;

            // Actualizar TextMeshPro
            if (tiempoTextTMP != null)
                tiempoTextTMP.text = "Tiempo: " + tiempoFormateado;
        }
    }

    public void MostrarPanelVictoria()
    {
        if (panelVictoria == null) return;

        // Pausar el juego
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Guardar el tiempo de esta escena en el GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddTime(tiempoEscena);
        }

        // Mostrar panel
        panelVictoria.SetActive(true);

        // Actualizar estadísticas finales
        ActualizarEstadisticasFinales();
    }

    void ActualizarEstadisticasFinales()
    {
        if (GameManager.Instance == null) return;

        // Score final
        int scoreFinal = GameManager.Instance.Score;
        if (scoreFinalText != null)
            scoreFinalText.text = "Score Total: " + scoreFinal;
        if (scoreFinalTextTMP != null)
            scoreFinalTextTMP.text = "Score Total: " + scoreFinal;

        // Tiempo final
        float tiempoFinal = GameManager.Instance.GlobalTime;
        string tiempoFormateado = FormatearTiempo(tiempoFinal);
        if (tiempoFinalText != null)
            tiempoFinalText.text = "Tiempo Total: " + tiempoFormateado;
        if (tiempoFinalTextTMP != null)
            tiempoFinalTextTMP.text = "Tiempo Total: " + tiempoFormateado;

        // Items recolectados
        int itemsTotal = GameManager.Instance.ItemsCount;
        if (itemsFinalText != null)
            itemsFinalText.text = "Items: " + itemsTotal;
        if (itemsFinalTextTMP != null)
            itemsFinalTextTMP.text = "Items: " + itemsTotal;

        Debug.Log($"=== VICTORIA ===\nScore: {scoreFinal}\nTiempo: {tiempoFormateado}\nItems: {itemsTotal}");
    }

    string FormatearTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        return string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    // Método público para llamar desde otros scripts
    public void GameOver()
    {
        MostrarPanelVictoria();
    }
}