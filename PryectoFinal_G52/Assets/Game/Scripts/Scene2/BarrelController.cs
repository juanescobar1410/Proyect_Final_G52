
using UnityEngine;

public class BarrelController : MonoBehaviour
{

    /// <summary>
    /// Controla el comportamiento del barril dentro del puzzle.
    /// 
    /// - Permite mover el barril en una dirección específica cuando un libro es activado.
    /// - Usa un raycast hacia adelante para detectar paredes u obstáculos; si detecta uno, detiene el movimiento.
    /// - Si el barril debe reaparecer, se mueve automáticamente al punto de respawn asignado en resetPoint.
    /// - Reproduce un sonido corto cada vez que se inicia un movimiento (si moveSound está asignado).
    /// - Si el barril colisiona con un objeto con tag "Blades", se considera destruido y reaparece en el resetPoint.
    /// 
    /// Este script se usa para que el barril se desplace en línea recta hasta chocar con algo,
    /// permitiendo resolver los puzzles basados en movimiento direccional.
    /// </summary>

    [Header("Movimiento del barril")]
    public float moveSpeed = 5f;

    [Tooltip("Punto donde reaparecerá el barril")]
    public Transform resetPoint;

    private Vector3 moveDirection;
    private bool isMoving = false;

    [Header("Detección de pared")]
    public float detectionDistance = 0.6f;

    [Header("Audio")]
    public AudioSource moveSound;   // 🔊 ← NUEVO

    void Update()
    {
        if (isMoving)
        {
            // 👉 Raycast al frente
            if (Physics.Raycast(transform.position, moveDirection, detectionDistance))
            {
                Debug.Log("Pared detectada. Parando barril.");
                isMoving = false;
                return;
            }

            // Movimiento continuo
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    public void MoveInDirection(BookController.Direction dir)
    {
        switch (dir)
        {
            case BookController.Direction.Norte:
                moveDirection = Vector3.forward;
                break;

            case BookController.Direction.Sur:
                moveDirection = Vector3.back;
                break;

            case BookController.Direction.Oriente:
                moveDirection = Vector3.right;
                break;

            case BookController.Direction.Occidente:
                moveDirection = Vector3.left;
                break;
        }

        isMoving = true;
        Debug.Log($"El barril se mueve hacia {dir}");

        // 🔊 Reproducir sonido una sola vez
        if (moveSound != null)
            moveSound.Play();
    }

    void ResetPosition()
    {
        if (resetPoint != null)
        {
            transform.position = resetPoint.position;
        }
        else
        {
            Debug.LogWarning(" No se asignó un Reset Point en el barril.");
        }

        isMoving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Blades"))
        {
            Debug.Log("El barril fue destruido por una Blade. Reapareciendo...");
            ResetPosition();
        }
    }
}