using UnityEngine;

public class GoalController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            Debug.Log("Barril llegó a la meta");
            SceneController.Instance.CompletePuzzle();
        }
    }
}
