using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Cambia la escena cuando el jugador entra en el trigger del objeto.
/// Solo carga la escena cuyo nombre fue asignado en el inspector.
/// </summary>
public class ChangeSceneOnCollision : MonoBehaviour
{
    public string sceneName; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

