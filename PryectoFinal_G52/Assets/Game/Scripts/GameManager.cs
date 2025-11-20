using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Gestiona las métricas globales del juego (score, tiempo, items).
/// Persiste entre escenas y guarda los datos en JSON al finalizar.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Métricas del Juego")]
    private float globalTime = 0f;
    private int score = 0;
    private int itemsCount = 0;

    [Header("Configuración de Guardado")]
    public string nombreArchivo = "game_stats.json";

    // Propiedades públicas
    public float GlobalTime { get => globalTime; set => globalTime = value; }
    public int Score { get => score; set => score = value; }
    public int ItemsCount { get => itemsCount; set => itemsCount = value; }

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager inicializado");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Resetear métricas al inicio de una nueva partida
        ResetearMetricas();
    }

    public void AddTime(float timeScene)
    {
        globalTime += timeScene;
        Debug.Log($"Tiempo total acumulado: {FormatearTiempo(globalTime)}");
    }

    public void AddScore(int scoreItem)
    {
        score += scoreItem;
        Debug.Log($"Score actualizado: {score}");
    }

    public void AddItem()
    {
        itemsCount++;
        Debug.Log($"Items recolectados: {itemsCount}");
    }

    // ==================== GUARDADO JSON ====================

    /// <summary>
    /// Guarda las métricas actuales en un archivo JSON
    /// </summary>
    public void GuardarMetricas()
    {
        GameStats stats = new GameStats
        {
            score = this.score,
            tiempo = this.globalTime,
            items = this.itemsCount,
            fecha = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        string json = JsonUtility.ToJson(stats, true); // true = formato legible
        string rutaArchivo = GetRutaArchivo();

        try
        {
            File.WriteAllText(rutaArchivo, json);
            Debug.Log($"✓ Métricas guardadas en: {rutaArchivo}");
            Debug.Log($"Score: {stats.score} | Tiempo: {FormatearTiempo(stats.tiempo)} | Items: {stats.items}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al guardar métricas: {e.Message}");
        }
    }

    /// <summary>
    /// Carga las métricas desde el archivo JSON
    /// </summary>
    public GameStats CargarMetricas()
    {
        string rutaArchivo = GetRutaArchivo();

        if (!File.Exists(rutaArchivo))
        {
            Debug.LogWarning("No existe archivo de métricas guardado");
            return null;
        }

        try
        {
            string json = File.ReadAllText(rutaArchivo);
            GameStats stats = JsonUtility.FromJson<GameStats>(json);
            Debug.Log($"✓ Métricas cargadas: Score={stats.score}, Tiempo={FormatearTiempo(stats.tiempo)}, Items={stats.items}");
            return stats;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al cargar métricas: {e.Message}");
            return null;
        }
    }

    /// <summary>
    /// Resetea todas las métricas
    /// </summary>
    public void ResetearMetricas()
    {
        score = 0;
        globalTime = 0f;
        itemsCount = 0;
        Debug.Log("Métricas reseteadas");
    }

    /// <summary>
    /// Abre la carpeta donde se guardan los archivos
    /// </summary>
    public void AbrirCarpetaGuardado()
    {
        string ruta = Application.persistentDataPath;
        Application.OpenURL("file://" + ruta);
        Debug.Log($"Carpeta de guardado: {ruta}");
    }

    // Obtiene la ruta completa del archivo
    private string GetRutaArchivo()
    {
        return Path.Combine(Application.persistentDataPath, nombreArchivo);
    }

    // Formatea el tiempo en formato MM:SS
    private string FormatearTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        return $"{minutos:00}:{segundos:00}";
    }

    // Método útil para debugging
    public void MostrarMetricas()
    {
        Debug.Log($"=== MÉTRICAS ACTUALES ===\nScore: {score}\nTiempo: {FormatearTiempo(globalTime)}\nItems: {itemsCount}");
    }
}

// ==================== CLASE DE DATOS ====================

/// <summary>
/// Estructura de datos para guardar las métricas
/// </summary>
[System.Serializable]
public class GameStats
{
    public int score;
    public float tiempo;
    public int items;
    public string fecha;

    public override string ToString()
    {
        int min = Mathf.FloorToInt(tiempo / 60f);
        int seg = Mathf.FloorToInt(tiempo % 60f);
        return $"Score: {score} | Tiempo: {min:00}:{seg:00} | Items: {items} | Fecha: {fecha}";
    }
}