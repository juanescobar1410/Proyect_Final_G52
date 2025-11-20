using UnityEngine;

public class SawbladeMovement : MonoBehaviour
{

    /// <summary>
    /// Controla el movimiento y rotación de una sierra (sawblade) en el escenario.
    ///
    /// FUNCIONAMIENTO:
    ///
    /// - Permite elegir el eje en el que la sierra se desplazará mediante un movimiento de vaivén:
    ///       * Eje X   Izquierda / Derecha
    ///       * Eje Z   Adelante / Atrás
    ///
    /// - El movimiento se basa en una función seno, lo que genera un patrón suave de ir y venir
    ///   desde la posición inicial, con un rango definido por moveDistance y una velocidad
    ///   determinada por moveSpeed.
    ///
    /// - Mientras se desplaza, la sierra rota constantemente sobre su eje Z a la velocidad
    ///   indicada en rotationSpeed, simulando el giro de una cuchilla.
    ///
    /// - startPos almacena la posición inicial para que todo el vaivén se mantenga
    ///   centrado respecto al punto donde comienza la sierra.
    ///
    /// Este script permite crear trampas que se mueven horizontal o frontalmente mientras giran.
    /// </summary>
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