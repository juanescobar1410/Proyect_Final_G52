//using UnityEngine;

//public class BarrelController : MonoBehaviour
//{
//    [Header("Movimiento del barril")]
//    public float moveSpeed = 5f;
//    public Vector3 resetPosition = new Vector3(0, 1, 0); // Punto de reaparici�n
//    public float boundaryLimit = 50f; // L�mite del �rea (si se sale, reaparece)

//    private Vector3 moveDirection;
//    private bool isMoving = false;
//    private Rigidbody rb;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        if (rb == null)
//        {
//            Debug.LogError("El Rigidbody no est� asignado en el barril.");
//        }
//    }

//    void Update()
//    {
//        // Verifica si el barril se sali� del �rea
//        if (Mathf.Abs(transform.position.x) > boundaryLimit ||
//            Mathf.Abs(transform.position.z) > boundaryLimit)
//        {
//            Debug.Log("El barril sali� del �rea. Reapareciendo...");
//            ResetPosition();
//        }
//        if (isMoving)
//        {
//            rb.AddForce(moveDirection * moveSpeed, ForceMode.Impulse);
//        }
//    }

//    public void MoveInDirection(BookController.Direction dir)
//    {
//        switch (dir)
//        {
//            case BookController.Direction.Norte:
//                moveDirection = Vector3.forward;
//                break;
//            case BookController.Direction.Sur:
//                moveDirection = Vector3.back;
//                break;
//            case BookController.Direction.Oriente:
//                moveDirection = Vector3.right;
//                break;
//            case BookController.Direction.Occidente:
//                moveDirection = Vector3.left;
//                break;
//        }

//        // Limpia la velocidad anterior para evitar acumulaci�n
//        rb.linearVelocity = Vector3.zero;


//        isMoving = true;
//        // Aplica una fuerza instant�nea hacia la direcci�n elegida




//        Debug.Log($"El barril se mueve hacia {dir}");
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        if (!isMoving) return;

//        Debug.Log($"El barril choc� con {collision.gameObject.name}");
//        isMoving = false;

//        // Detiene el movimiento f�sico inmediatamente
//        rb.linearVelocity = Vector3.zero;
//        rb.angularVelocity = Vector3.zero;
//    }

//    void ResetPosition()
//    {
//        // Teletransporta el barril al punto inicial y detiene su movimiento
//        rb.linearVelocity = Vector3.zero;
//        rb.angularVelocity = Vector3.zero;
//        rb.position = resetPosition;
//        isMoving = false;
//    }
//}
//using UnityEngine;

//public class BarrelController : MonoBehaviour
//{
//    [Header("Movimiento del barril")]
//    public float moveSpeed = 5f;
//    public Vector3 resetPosition = new Vector3(0, 1, 0); // Punto de reaparición
//    public float boundaryLimit = 50f; // Límite del área (si se sale, reaparece)

//    private Vector3 moveDirection;
//    private bool isMoving = false;

//    private Rigidbody rb;

//    void Awake()
//    {
//        rb = GetComponent<Rigidbody>();
//    }

//    void Update()
//    {
//        // Tu código tal cual
//        if (isMoving)
//        {
//            // Eliminamos el Translate (para que no se “pegue” en Z)
//            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

//            // Si sale del área, reaparece
//            if (Mathf.Abs(transform.position.x) > boundaryLimit ||
//                Mathf.Abs(transform.position.z) > boundaryLimit)
//            {
//                Debug.Log("El barril salió del área. Reapareciendo...");
//                ResetPosition();
//            }
//        }
//    }

//    void FixedUpdate()
//    {
//        // NUEVO: Velocidad constante y suave con Rigidbody
//        //if (isMoving)
//        //{
//        //    rb.linearVelocity = moveDirection * moveSpeed;
//        //}
//    }

//    public void MoveInDirection(BookController.Direction dir)
//    {
//        // Tu código tal cual
//        switch (dir)
//        {
//            case BookController.Direction.Norte:
//                moveDirection = Vector3.forward;
//                break;
//            case BookController.Direction.Sur:
//                moveDirection = Vector3.back;
//                break;
//            case BookController.Direction.Oriente:
//                moveDirection = Vector3.right;
//                break;
//            case BookController.Direction.Occidente:
//                moveDirection = Vector3.left;
//                break;
//        }

//        isMoving = true;
//        Debug.Log($"El barril se mueve hacia {dir}");
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        // Tu código tal cual
//        Debug.Log($"El barril chocó con {collision.gameObject.name}");
//        isMoving = false;

//        // NUEVO: Detener rigidbody cuando se choca
//        rb.linearVelocity = Vector3.zero;
//    }

//    void ResetPosition()
//    {
//        transform.position = resetPosition;
//        isMoving = false;

//        // NUEVO: Resetear velocidad
//        rb.linearVelocity = Vector3.zero;
//    }
//}
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    [Header("Movimiento del barril")]
    public float moveSpeed = 5f;

    [Tooltip("Punto donde reaparecerá el barril")]
    public Transform resetPoint;   // 🔹 Nuevo: puedes asignar un GameObject aquí

    public float boundaryLimit = 50f;

    private Vector3 moveDirection;
    private bool isMoving = false;

    [Header("Detección de pared")]
    public float detectionDistance = 0.6f;

    void Update()
    {
        if (isMoving)
        {
            // 👉 Raycast al frente
            if (Physics.Raycast(transform.position, moveDirection, detectionDistance))
            {
                Debug.Log("Pared detectada. Parando barril.");
                Debug.DrawRay(transform.position, moveDirection * detectionDistance, Color.red);
                isMoving = false;
                return;
            }

            // Movimiento continuo
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Límite del área
            if (Mathf.Abs(transform.position.x) > boundaryLimit ||
                Mathf.Abs(transform.position.z) > boundaryLimit)
            {
                Debug.Log("El barril salió del área. Reapareciendo...");
                ResetPosition();
            }
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
}