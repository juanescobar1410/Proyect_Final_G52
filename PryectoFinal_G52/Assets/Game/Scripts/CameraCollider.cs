using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    public float minDistancia = 1;
    public float maxDistancia = 4;
    public float suavidad = 10;

    private float distanciaActual;
    private Vector3 direccionOriginal;

    public LayerMask capaColision; // IMPORTANTE

    private Transform pivot;

    void Start()
    {
        pivot = transform.parent;

        direccionOriginal = transform.localPosition.normalized;
        distanciaActual = transform.localPosition.magnitude;
    }

    void LateUpdate()
    {
        Vector3 posicionObjetivo = pivot.TransformPoint(direccionOriginal * maxDistancia);

        RaycastHit hit;

        float distanciaDeseada = maxDistancia;

        if (Physics.Linecast(pivot.position, posicionObjetivo, out hit, capaColision))
        {
            distanciaDeseada = Mathf.Clamp(hit.distance * 0.9f, minDistancia, maxDistancia);
        }

        distanciaActual = Mathf.Lerp(distanciaActual, distanciaDeseada, Time.deltaTime * suavidad);

        transform.localPosition = direccionOriginal * distanciaActual;
    }
}