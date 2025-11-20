using UnityEngine;

/// <summary>
/// Controla la fireball del jefe: avanza hacia adelante, aumenta su tamaño
/// progresivamente y se desactiva al pasar 1 segundo, reiniciando su escala
/// para poder reutilizarse.
/// </summary>


public class Fireball : MonoBehaviour
{
    private float cronometro;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 6);
        transform.localScale += new Vector3(3,3,3) * Time.deltaTime;

        cronometro += 1 * Time.deltaTime;

        if (cronometro > 1f)
        {
            transform.localScale = new Vector3(1,1,1);
            gameObject.SetActive(false);
            cronometro = 0;
        }
    }
}
