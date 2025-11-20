using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string llaveRequerida = "llave1";
    private ControllerScene1 controllerScene;

    void Start()
    {
        controllerScene = FindFirstObjectByType<ControllerScene1>();
    }

    public void Interactuar()
    {
        // Verifica si el jugador tiene la llave
        if (controllerScene.TieneLlave(llaveRequerida))
        {
            Debug.Log("Puerta abierta con llave: " + llaveRequerida);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Necesitas la llave: " + llaveRequerida);
        }
    }
}