using UnityEngine;
/// <summary>
/// Controla qué arma se muestra en el jugador.
/// - Recibe un número y activa únicamente esa arma del arreglo.
/// - Asegura que solo un arma esté activa a la vez.
/// </summary>

public class ShowSword : MonoBehaviour
{

    public GameObject[] armas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivarArmar(int numero)
    {
        for (int i = 0; i < armas.Length; i++)
        {
            armas[i].SetActive(true);
        }

        armas[numero].SetActive(true);
    }

}

