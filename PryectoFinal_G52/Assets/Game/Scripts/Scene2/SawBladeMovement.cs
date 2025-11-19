using UnityEngine;

public class SawbladeMovement : MonoBehaviour
{
    public enum MoveAxis { X, Z }   // ab Para elegir desplazamiento horizontal o frontal
    public MoveAxis movementAxis = MoveAxis.X;

    [Header("Movimiento")]
    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    [Header("Rotación")]
    public float rotationSpeed = 300f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Movimiento tipo vaivén
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        Vector3 newPos = startPos;

        if (movementAxis == MoveAxis.X)
            newPos.x += offset;
        else
            newPos.z += offset;

        transform.position = newPos;

        // Rotación continua en eje Z
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}