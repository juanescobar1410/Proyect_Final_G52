using UnityEngine;

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
