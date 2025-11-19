using UnityEngine;
using UnityEngine.AI;

public class Npcs : MonoBehaviour
{
    public NavMeshAgent AI;
    public float Velocidad;
    public Transform[] Objetivos;
    Transform Objetivo;
    public float Distancia;

    void Start()
    {
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
    }
}