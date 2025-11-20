using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [Header("Audio")]
    public AudioSource loopAudio;   // 

    [Header("Salas/Puzzles")]
    public GameObject puzzle1;
    public GameObject puzzle2;
    public GameObject puzzle3;

    [Header("Puertas / Bloqueos")]
    public GameObject puzzle1Lock;
    public GameObject puzzle2Lock;
    public GameObject puzzle3Lock;

    [Header("Timer")]
    public Timer timer;

    [Header("Sistema de Puntaje")]
    public int baseScore = 100;
    public float bonusTimeLimit = 45f;
    public int bonusMultiplier = 5;

    private int currentPuzzle = 1;

    private float[] puzzleTimes = new float[3];
    private int[] puzzleScores = new int[3];

    [Header("UI Final")]
    public GameObject finalPanel;
    public TextMeshProUGUI p1TimeText;
    public TextMeshProUGUI p2TimeText;
    public TextMeshProUGUI p3TimeText;

    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;
    public TextMeshProUGUI p3ScoreText;

    public TextMeshProUGUI totalTimeText;
    public TextMeshProUGUI totalScoreText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //  Reproducir música looping si está asignada
        if (loopAudio != null)
        {
            loopAudio.loop = true;
            loopAudio.Play();
        }

        InitializeLocks();
        StartPuzzle(1);
    }

    private void InitializeLocks()
    {
        if (puzzle1Lock != null) puzzle1Lock.SetActive(false);
        if (puzzle2Lock != null) puzzle2Lock.SetActive(true);
        if (puzzle3Lock != null) puzzle3Lock.SetActive(true);
    }

    public void StartPuzzle(int puzzleIndex)
    {
        currentPuzzle = puzzleIndex;

        if (puzzleIndex == 1) puzzle1.SetActive(true);
        if (puzzleIndex == 2) puzzle2.SetActive(true);
        if (puzzleIndex == 3) puzzle3.SetActive(true);

        timer.TimerReset();
        timer.TimerStart();

        Debug.Log("Inició Puzzle " + puzzleIndex);
    }

    public void CompletePuzzle()
    {
        timer.TimerStop();
        float finalTime = timer.StopTime;

        puzzleTimes[currentPuzzle - 1] = finalTime;

        int bonus = Mathf.Max(0, (int)((bonusTimeLimit - finalTime) * bonusMultiplier));

        int puzzleScore = baseScore + bonus;
        puzzleScores[currentPuzzle - 1] = puzzleScore;

        GameManager.Instance.AddTime(finalTime);
        GameManager.Instance.AddScore(puzzleScore);

        Debug.Log($"Puzzle {currentPuzzle} completado. Tiempo: {finalTime}, Score: {puzzleScore}");

        UnlockNextPuzzle();
    }

    private void UnlockNextPuzzle()
    {
        if (currentPuzzle == 1)
        {
            if (puzzle2Lock != null) puzzle2Lock.SetActive(false);
            StartPuzzle(2);
        }
        else if (currentPuzzle == 2)
        {
            if (puzzle3Lock != null) puzzle3Lock.SetActive(false);
            StartPuzzle(3);
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Juego terminado.");

        finalPanel.SetActive(true);

        p1TimeText.text = puzzleTimes[0].ToString("0.00") + " s";
        p2TimeText.text = puzzleTimes[1].ToString("0.00") + " s";
        p3TimeText.text = puzzleTimes[2].ToString("0.00") + " s";

        p1ScoreText.text = puzzleScores[0].ToString();
        p2ScoreText.text = puzzleScores[1].ToString();
        p3ScoreText.text = puzzleScores[2].ToString();

        totalTimeText.text = GameManager.Instance.GlobalTime.ToString("0.00") + " s";
        totalScoreText.text = GameManager.Instance.Score.ToString();
    }

}