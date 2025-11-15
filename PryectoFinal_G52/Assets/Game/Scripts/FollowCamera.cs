using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 0.125f;

    public float minDistance = 1f;     // Distancia mínima a la que la cámara puede acercarse
    public float collisionPadding = 0.2f; // Bloquea ligeramente antes para evitar ver dentro de la pared

    private Vector3 finalOffset;
    private float distActual;

    private void Start()
    {
        distActual = offset.magnitude;
        finalOffset = offset.normalized;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Posición ideal sin colisión
        Vector3 desiredPosition = target.position + target.TransformDirection(finalOffset * distActual);

        RaycastHit hit;

        // Raycast desde el jugador hacia la posición ideal de la cámara
        if (Physics.Raycast(target.position, (desiredPosition - target.position).normalized, out hit, distActual))
        {
            float distanciaColision = Mathf.Max(hit.distance - collisionPadding, minDistance);

            desiredPosition = target.position +
                              target.TransformDirection(finalOffset * distanciaColision);
        }

        // Suavizado del movimiento
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Mirar al jugador
        transform.LookAt(target);
    }
}
