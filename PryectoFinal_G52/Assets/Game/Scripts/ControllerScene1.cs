using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ControllerScene1 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoContador;

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
}