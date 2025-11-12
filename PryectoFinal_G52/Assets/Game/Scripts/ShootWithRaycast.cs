using UnityEngine;
using System.Collections;

public class ShootWithRaycast : MonoBehaviour
{
    public enum Opcion { Opcion1, Opcion2 }
    public Opcion opcionSeleccionada = Opcion.Opcion1;

    [Header("Configuración del Rayo")]
    public float rayDistance = 10f;
    public float cooldown = 1f;
    public float tiempoCambioTurno = 3.5f;

    // Variable estática compartida por todos los RayShooters
    private static Opcion opcionActiva = Opcion.Opcion1;
    private static bool turnoControlIniciado = false; // asegura que solo uno cambie el turno

    private bool puedeDisparar = true;

    private void Start()
    {
        StartCoroutine(DispararRayo());

        // Solo el primer RayShooter que arranque iniciará el cambio de turnos
        if (!turnoControlIniciado)
        {
            StartCoroutine(ControlarTurnosGlobal());
            turnoControlIniciado = true;
        }
    }

    IEnumerator DispararRayo()
    {
        while (true)
        {
            if (puedeDisparar && opcionSeleccionada == opcionActiva)
            {
                Color color = opcionSeleccionada == Opcion.Opcion1 ? Color.red : Color.blue;
                StartCoroutine(ShootRayColor(color));

                puedeDisparar = false;
                yield return new WaitForSeconds(cooldown);
                puedeDisparar = true;
            }

            yield return null;
        }
    }

    IEnumerator ShootRayColor(Color color)
    {
        Vector3 direction = transform.up; // dirección eje verde (Z local)
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log($"{name} golpeó al Player: {hit.collider.name}");
                // Aquí puedes usar este golpe para daño o eventos
            }
        }

        // Dibuja el rayo en la escena
        Debug.DrawRay(transform.position, direction * rayDistance, color, 0.2f);
        yield return null;
    }

    //  Solo uno controla el cambio global
    IEnumerator ControlarTurnosGlobal()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoCambioTurno);

            opcionActiva = opcionActiva == Opcion.Opcion1 ? Opcion.Opcion2 : Opcion.Opcion1;
            Debug.Log($"Turno global cambiado a: {opcionActiva}");
        }
    }
}