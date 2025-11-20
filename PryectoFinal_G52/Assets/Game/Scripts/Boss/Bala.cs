using UnityEngine;

/// <summary>
/// Controla el comportamiento de una bala: avanza constantemente hacia adelante
/// y se desactiva automáticamente después de 3 segundos para reutilizarse.
/// </summary>


public class Bala : MonoBehaviour
{
    public float cronometro;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cronometro += 1 * Time.deltaTime;
        if (cronometro > 3)
        {
            gameObject.SetActive(false);
            cronometro = 0;
        }
        transform.Translate(Vector3.forward * 15 * Time.deltaTime);
    }
}
