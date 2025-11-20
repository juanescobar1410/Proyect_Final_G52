using UnityEngine;
using UnityEngine.AI;

public class Npcs : MonoBehaviour
{
    public NavMeshAgent AI;
    public float Velocidad;
    public Transform[] Objetivos;
    Transform Objetivo;
    public float Distancia;

    private Animation anim; // COMPONENTE ANIMATION

    void Start()
    {
        anim = GetComponent<Animation>(); // OBTENER ANIMATION
        Objetivo = Objetivos[Random.Range(0, Objetivos.Length)];
    }

    void Update()
    {
        Distancia = Vector3.Distance(transform.position, Objetivo.position);

        if (Distancia < 2)
        {
            Objetivo = Objetivos[Random.Range(0, Objetivos.Length)];
        }

        AI.destination = Objetivo.position;
        AI.speed = Velocidad;

        // CONTROLAR ANIMACIÓN SEGÚN VELOCIDAD
        if (anim != null)
        {
            // Si el agente se está moviendo, reproducir animación de caminar
            if (AI.velocity.magnitude > 0.1f)
            {
                if (!anim.IsPlaying("Walking")) // Nombre de tu animación de caminar
                {
                    anim.CrossFade("Walking", 0.2f);
                }
            }
        }
    }
}