using UnityEngine;

/// <summary>
/// Representa un NPC con el que el jugador puede interactuar.
/// Al interactuar, muestra un chat bubble con diálogo y activa
/// una animación de hablar si existe un Animator.
/// También proporciona el texto que aparece en la UI de interacción.
/// </summary>

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private string textoDialogo;
    [SerializeField] private GameObject chatBubblePrefab;
    private Animator animator;
   

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Interact(Transform interactorTransform)
    {
        GameObject bubble = Instantiate(chatBubblePrefab, transform);
        bubble.transform.localPosition = new Vector3(3f, 3f, 0f);
        bubble.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); 
        bubble.GetComponent<ChatBubble3D>().ShowMessage(textoDialogo);

        if (animator != null)
        {
            Debug.Log("Trigger Talk activado");
            animator.SetTrigger("Talk");
        }
    }
    public string GetInteractText()
    {
        return interactText;
    }
}