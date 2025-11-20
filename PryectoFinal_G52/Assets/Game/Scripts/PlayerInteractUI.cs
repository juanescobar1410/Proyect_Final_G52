using TMPro;
using UnityEngine;


/// <summary>
/// Controla la UI de interacción del jugador.
/// Muestra un panel con texto cuando el jugador puede interactuar
/// con un NPC u objeto, y lo oculta cuando no hay nada interactuable.
/// </summary>


public class PlayerInteractUI : MonoBehaviour
{



    [SerializeField] private GameObject containerGameobject;
  
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;
  

    private void Update()
    {
        if(playerInteract.GetInteractableObject() != null)
        { 
            mostrar(playerInteract.GetInteractableObject());
        }
        else
        {
            esconder();
        }

        
    }

   

    private void mostrar(NPCInteractable npcInteractable) {
    
        containerGameobject.SetActive(true);
        interactTextMeshProUGUI.text = npcInteractable.GetInteractText();
    }

    private void esconder()
    {
        containerGameobject.SetActive(false);
    }



}
