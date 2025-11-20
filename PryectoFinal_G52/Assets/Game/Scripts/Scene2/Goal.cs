using UnityEngine;

public class GoalController : MonoBehaviour
{
    /// <summary>
    /// Detecta cuando el barril entra en la meta.
    /// Si el objeto que entra tiene el tag "Barrel",
    /// llama a SceneController para completar el puzzle.
    /// Debe usarse en un collider con "Is Trigger".
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            Debug.Log("Barril llegó a la meta");
            SceneController.Instance.CompletePuzzle();
        }
    }
}
