using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private string textoDialogo;
    [SerializeField] private GameObject chatBubblePrefab;
    private Animator animator;
    private NPCHeadLookAt npcHeadLookAt;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Interact(Transform interactorTransform)
    {
        GameObject bubble = Instantiate(chatBubblePrefab, transform);
        bubble.transform.localPosition = new Vector3(3f, 3f, 0f);
        bubble.GetComponent<ChatBubble3D>().ShowMessage(textoDialogo);

        if (animator != null)
        {
            Debug.Log("Trigger Talk activado");
            animator.SetTrigger("Talk");
        }

        //float playerHeight = 1.7f;
        //npcHeadLookAt.LookAtPosition(interactorTransform.position + Vector3.up * playerHeight);
    }

    public string GetInteractText()
    {
        return interactText;
    }
}