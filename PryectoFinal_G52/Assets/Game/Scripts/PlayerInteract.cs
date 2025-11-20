using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    void Start()
    {
    }

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private LayerMask doorLayerMask; // Nueva layer para puertas

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 2f;

            // Intenta raycast primero para objetos pick-up
            if (Physics.Raycast(playerCameraTransform.position,
                               playerCameraTransform.forward,
                               out RaycastHit raycastHit,
                               interactRange,
                               pickUpLayerMask))
            {
                Debug.Log(raycastHit.transform);
                if (raycastHit.transform.TryGetComponent(out ObjetoRecolecionable objetoRecoleccionable))
                {
                    Debug.Log(objetoRecoleccionable);
                    objetoRecoleccionable.Recolectar();
                }
            }

            // Intenta raycast para puertas
            if (Physics.Raycast(playerCameraTransform.position,
                               playerCameraTransform.forward,
                               out RaycastHit doorHit,
                               interactRange,
                               doorLayerMask))
            {
                if (doorHit.transform.TryGetComponent(out Door door))
                {
                    door.Interactuar();
                }
            }

            // Si no hay nada que recoger, busca NPCs
            Physics.OverlapSphere(transform.position, interactRange);
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact(transform);
                }
            }
        }
    }

    public NPCInteractable GetInteractableObject()
    {
        List<NPCInteractable> npcInteractableList = new List<NPCInteractable>();
        float interactRange = 4f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                npcInteractableList.Add(npcInteractable);
            }
        }
        NPCInteractable closestNPCInteractable = null;
        foreach (NPCInteractable npcInteractable in npcInteractableList)
        {
            if (closestNPCInteractable == null)
            {
                closestNPCInteractable = npcInteractable;
            }
            else
            {
                if (Vector3.Distance(transform.position, npcInteractable.transform.position) <
                    Vector3.Distance(transform.position, closestNPCInteractable.transform.position))
                {
                    closestNPCInteractable = npcInteractable;
                }
            }
        }
        return closestNPCInteractable;
    }
}