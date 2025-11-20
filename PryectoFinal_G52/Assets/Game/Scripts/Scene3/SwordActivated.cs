using UnityEngine;

/// <summary>
/// Activa un arma específica cuando el jugador entra en el trigger
/// y destruye el objeto que contiene este script.
/// </summary>
public class SwordActivated : MonoBehaviour
{
    public ShowSword cogerArmas;
    public int numeroArma;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cogerArmas.ActivarArmar(numeroArma);
            Destroy(gameObject);
        }
    }
}