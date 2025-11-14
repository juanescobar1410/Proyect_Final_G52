using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{



    [SerializeField] private GameObject containerGameobject;
    [SerializeField] private PlayerInteract playerInteract;


    private void Update()
    {
        if(playerInteract.GetInteractableObject() != null)
        { 
            mostrar();
        }
        else
        {
            esconder();
        }
        
         
    }



    private void mostrar() {
    
        containerGameobject.SetActive(true);
    }

    private void esconder()
    {
        containerGameobject.SetActive(false);
    }



}
