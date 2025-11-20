using UnityEngine;
using UnityEngine.AI;



/// <summary>
/// Controla el comportamiento básico de un NPC que patrulla entre varios puntos.
/// Selecciona objetivos aleatorios, se mueve hacia ellos usando NavMeshAgent,
/// y reproduce una animación de caminar mientras está en movimiento.
/// </summary>

public class NpcsIA : MonoBehaviour
{
    public NavMeshAgent AI;
    public float Velocidad;
    public Transform[] Objetivos;
    Transform Objetivo;
    public float Distancia;

    private Animation anim; 

    void Start()
    {
        anim = GetComponent<Animation>(); 
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
            
            if (AI.velocity.magnitude > 0.1f)
            {
                if (!anim.IsPlaying("Walking")) 
                {
                    anim.CrossFade("Walking", 0.2f);
                }
            }
        }
    }
}