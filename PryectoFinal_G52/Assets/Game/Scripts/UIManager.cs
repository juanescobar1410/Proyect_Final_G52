using UnityEngine;
using UnityEngine.UI;
using TMPro; // Si usas TextMeshPro

/// <summary>
/// Administra toda la interfaz del juego. Actualiza en pantalla el puntaje y el tiempo
/// durante la partida, llevando también el tiempo de la escena. Al ganar, pausa el juego,
/// muestra el panel de victoria y presenta las estadísticas finales (puntaje total,
/// tiempo acumulado e ítems recolectados). Incluye formato de tiempo en MM:SS y un método
/// público para activar la pantalla de victoria desde otros scripts.
/// </summary>

public class UIManager : MonoBehaviour
{
    [Header("UI en Juego")]
    public TextMeshProUGUI scoreTextTMP; // Si usas TextMeshPro
    public TextMeshProUGUI tiempoTextTMP;

    [Header("Panel de Victoria")]
    public GameObject panelVictoria;
    public TextMeshProUGUI scoreFinalTextTMP;
    public TextMeshProUGUI tiempoFinalTextTMP;
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
        ActualizarScore();
        ActualizarTiempo();
    }

    public void ActualizarScore()
    {
        if (GameManager.Instance != null)
        {
            int score = GameManager.Instance.Score;

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
        if (scoreFinalTextTMP != null)
            scoreFinalTextTMP.text = "Score Total: " + scoreFinal;

        // Tiempo final
        float tiempoFinal = GameManager.Instance.GlobalTime;
        string tiempoFormateado = FormatearTiempo(tiempoFinal);

        if (tiempoFinalTextTMP != null)
            tiempoFinalTextTMP.text = "Tiempo Total: " + tiempoFormateado;

        // Items recolectados
        int itemsTotal = GameManager.Instance.ItemsCount;

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