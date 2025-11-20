using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
/// <summary>
/// Administra el progreso de la escena.
/// Lleva el conteo de objetos recolectados, gestiona las llaves obtenidas,
/// actualiza la UI y permite cambiar de escena cuando el jugador llega al punto indicado.
/// </summary>

public class ControllerScene1 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoContador;
    [SerializeField] private string nombreEscenaSiguiente = "Dungeon_2";

    private int objetosRecolectados = 0;
    private List<string> llavesRecolectadas = new List<string>();

    public void ObjetoRecolectado()
    {
        objetosRecolectados++;
        textoContador.text = objetosRecolectados.ToString();
    }

    public void AgregarLlave(string idLlave)
    {
        if (!llavesRecolectadas.Contains(idLlave))
        {
            llavesRecolectadas.Add(idLlave);
            Debug.Log("Llave agregada: " + idLlave);
        }
    }

    public bool TieneLlave(string idLlave)
    {
        return llavesRecolectadas.Contains(idLlave);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaSiguiente);
        }
    }
}