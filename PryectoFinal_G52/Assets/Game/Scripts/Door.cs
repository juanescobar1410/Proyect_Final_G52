using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string llaveRequerida = "llave1";
    private ControllerScene1 controllerScene;

    void Start()
    {
        controllerScene = FindFirstObjectByType<ControllerScene1>();
    }

    void Update()
    {
        // Verifica constantemente si el jugador tiene la llave
        if (controllerScene.TieneLlave(llaveRequerida))
        {
            Debug.Log("Puerta eliminada - jugador tiene la llave: " + llaveRequerida);
            Destroy(gameObject);
        }
    }
}