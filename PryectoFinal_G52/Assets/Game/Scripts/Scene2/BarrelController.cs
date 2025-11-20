
using UnityEngine;

public class BarrelController : MonoBehaviour
{
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