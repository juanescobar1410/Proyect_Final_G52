using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona las interacciones del jugador.
/// Permite recoger objetos mediante raycast y hablar con NPCs dentro de un rango.
/// También identifica el NPC interactuable más cercano para mostrar información en la UI.
/// </summary>

public class PlayerInteract : MonoBehaviour
{
    void Start()
    {
    }

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 2f;

            
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
        float interactRange = 2f;
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
                if (Vector3.Distance(transform.position, npcInteractable.transform.position)<
                    Vector3.Distance(transform.position, closestNPCInteractable.transform.position))
                {
                    closestNPCInteractable = npcInteractable;
                }
            }
        }
        return closestNPCInteractable;
    }
}