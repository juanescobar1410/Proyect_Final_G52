using UnityEngine;

/// <summary>
/// Controla una puerta que se destruye automáticamente
/// cuando el jugador posee la llave requerida registrada en el controlador de escena.
/// </summary>

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
        
        if (controllerScene.TieneLlave(llaveRequerida))
        {
            Debug.Log("Puerta eliminada - jugador tiene la llave: " + llaveRequerida);
            Destroy(gameObject);
        }
    }
}