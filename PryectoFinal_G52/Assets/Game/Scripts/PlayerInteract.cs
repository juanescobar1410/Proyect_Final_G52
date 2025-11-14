using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 2f;
            Physics.OverlapSphere(transform.position, interactRange);
            Collider[] colliderArray = Physics.OverlapSphere(transform.position,interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }
                        
            }

        }
       


    }

    public NPCInteractable GetInteractableObject()
    {
        float interactRange = 2f;
        Physics.OverlapSphere(transform.position, interactRange);
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                return npcInteractable;
            }

        }
        return null;
    }
}
