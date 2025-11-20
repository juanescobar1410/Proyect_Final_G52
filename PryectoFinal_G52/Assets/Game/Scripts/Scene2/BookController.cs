using UnityEngine;
using UnityEngine.InputSystem;

public class BookController : MonoBehaviour
{
    /// <summary>
    /// Controla la interacción con cada libro del puzzle.
    /// Detecta clics mediante raycast y verifica si el jugador
    /// está lo suficientemente cerca para activarlo.
    /// Al hacer clic válido, envía al barril una dirección de movimiento
    /// (Norte, Sur, Oriente u Occidente) según lo asignado en el inspector.
    /// </summary>
    public enum Direction { Norte, Sur, Oriente, Occidente }
    [Header("Configuración del Libro")]
    public Direction moveDirection;       // Se asigna en el inspector
    public float clickDistance = 3f;      // Distancia máxima de interacción
    public Camera mainCamera;

    [Header("Referencia al barril")]
    public BarrelController barrel;       // Asignar en el inspector

    private Transform player;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    float distance = Vector3.Distance(transform.position, player.position);

                    if (distance <= clickDistance)
                    {
                        OnBookClicked();
                    }
                    else
                    {
                        Debug.Log("Estás demasiado lejos del libro.");
                    }
                }
            }
        }
    }

    void OnBookClicked()
    {
        if (barrel != null)
        {
            Debug.Log($"Libro {moveDirection} activado.");
            barrel.MoveInDirection(moveDirection);
        }
        else
        {
            Debug.LogWarning("No hay BarrelController asignado en este libro.");
        }
    }
}
