using UnityEngine;

public class ObjetoRecolecionable : MonoBehaviour
{
    private ControllerScene1 controllerScene;

    [SerializeField] private bool esLlave = false;
    [SerializeField] private string idLlave = "llave1";

    void Start()
    {
        controllerScene = FindFirstObjectByType<ControllerScene1>();
    }

    public void Recolectar()
    {
        Debug.Log("Objeto recolectado: " + gameObject.name);
        controllerScene.ObjetoRecolectado();

        if (esLlave)
        {
            controllerScene.AgregarLlave(idLlave);
        }

        Destroy(gameObject);
    }
}