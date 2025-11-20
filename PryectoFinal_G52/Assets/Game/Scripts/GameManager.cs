using System.IO;
using UnityEngine;

/// <summary>
/// Administra las variables globales del juego mediante un patrón Singleton.
/// Lleva el registro del tiempo total, el puntaje acumulado y la cantidad de
/// ítems obtenidos, permitiendo sumarlos desde cualquier escena o script.
/// </summary>


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float globalTime = 0f;
    private int score = 0;
    private int itemsCount = 0;

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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddTime(float timeScene)
    {
        globalTime += timeScene;
    }

    public void AddScore(int scoreItem)
    {
        score += scoreItem;
    }

    public void AddItem()
    {
        itemsCount++;
    }

    public void SaveMetricsToJSON()
    {
        PlayerMetrics data = new PlayerMetrics()
        {
            score = this.score,
            items = this.itemsCount,
            time = this.globalTime,
            date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        string json = JsonUtility.ToJson(data, true);
        string path = Application.persistentDataPath + "/player_metrics.json";

        File.WriteAllText(path, json);

        Debug.Log("📁 JSON guardado en: " + path);
    }
}

[System.Serializable]
public class PlayerMetrics
{
    public int score;
    public int items;
    public float time;
    public string date;
}