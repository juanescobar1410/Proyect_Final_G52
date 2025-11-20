using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    public void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaSiguiente);
        }
    }
}