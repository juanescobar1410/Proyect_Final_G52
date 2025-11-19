using UnityEngine;
using UnityEngine.UI;

public class ControllerSceneBoss : MonoBehaviour
{
    [Header("Referencias principales")]
    public Boss boss;
    public PuertaFinal puertaFinal;
    public GameObject player;

    [Header("Spawn de Enemigos")]
    public GameObject enemigoPrefab;
    public Transform[] puntosSpawn;
    public int cantidadEnemigos = 2;
    private bool enemigosSpawneados = false;


    [Header("Audio")]
    public AudioSource musicaNormal;
    public AudioSource musicaBoss;


    [Header("Cinematica/Eventos")]
    private bool peleaIniciada = false;
    private bool bossEnFase2 = false;
    private bool bossDerrotado = false;

    [System.Obsolete]
    void Start()
    {
        // Buscar referencias automáticamente si no están asignadas
        if (boss == null)
        {
            boss = FindObjectOfType<Boss>();
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Configuración inicial
        if (puertaFinal != null)
        {
            puertaFinal.puertaBloqueada = true;
        }


    }

    void Update()
    {
        if (boss == null) return;

        // Detectar inicio de la pelea
        if (!peleaIniciada && boss.musica.enabled)
        {
            IniciarPeleaBoss();
        }

        // Detectar cambio a Fase 2
        if (!bossEnFase2 && boss.fase == 2)
        {
            CambiarAFase2();
        }

        // Detectar muerte del Boss
        if (!bossDerrotado && boss.Hp_min <= 0)
        {
            BossDerrotado();
        }
    }

    // Cuando el jugador entra en rango del Boss
    void IniciarPeleaBoss()
    {
        peleaIniciada = true;
        Debug.Log("¡Pelea con el Boss iniciada!");


        // Cambiar música
        if (musicaNormal != null)
        {
            musicaNormal.Stop();
        }

        if (musicaBoss != null && !musicaBoss.isPlaying)
        {
            musicaBoss.Play();
        }


    }

    // Cuando el Boss entra en Fase 2
    void CambiarAFase2()
    {
        bossEnFase2 = true;
        Debug.Log("¡Boss en Fase 2!");

        // Spawnear enemigos
        if (!enemigosSpawneados && enemigoPrefab != null)
        {
            SpawnEnemigos();
            enemigosSpawneados = true;
        }


        // Hacer el Boss más rápido (opcional)
        boss.speed *= 1.2f;
    }

    // Cuando el Boss muere
    void BossDerrotado()
    {
        bossDerrotado = true;
        Debug.Log("¡Boss derrotado!");

        // Desbloquear puerta
        if (puertaFinal != null)
        {
            puertaFinal.DesbloquearPuerta();
        }

        // Cambiar música
        if (musicaBoss != null)
        {
            musicaBoss.Stop();
        }


        // Destruir enemigos spawneados (opcional)
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemigo in enemigos)
        {
            Destroy(enemigo, 2f); // Los destruye después de 2 segundos
        }
    }

    // Sistema de spawn de enemigos
    void SpawnEnemigos()
    {
        Debug.Log("Spawneando " + cantidadEnemigos + " enemigos...");

        if (puntosSpawn.Length == 0)
        {
            // Spawn en círculo alrededor del boss
            float anguloEntreCadaEnemigo = 360f / cantidadEnemigos;
            float radio = 5f;

            for (int i = 0; i < cantidadEnemigos; i++)
            {
                float angulo = i * anguloEntreCadaEnemigo;
                float x = boss.transform.position.x + Mathf.Cos(angulo * Mathf.Deg2Rad) * radio;
                float z = boss.transform.position.z + Mathf.Sin(angulo * Mathf.Deg2Rad) * radio;

                Vector3 posicion = new Vector3(x, boss.transform.position.y, z);
                GameObject enemigo = Instantiate(enemigoPrefab, posicion, Quaternion.identity);


            }
        }
        else
        {
            // Usar puntos de spawn definidos
            for (int i = 0; i < puntosSpawn.Length && i < cantidadEnemigos; i++)
            {
                if (puntosSpawn[i] != null)
                {
                    GameObject enemigo = Instantiate(enemigoPrefab, puntosSpawn[i].position, puntosSpawn[i].rotation);


                }
            }
        }
    }

    // Método público para reiniciar la escena desde otros scripts
    public void ReiniciarEscenaBoss()
    {
        peleaIniciada = false;
        bossEnFase2 = false;
        bossDerrotado = false;
        enemigosSpawneados = false;
    }
}