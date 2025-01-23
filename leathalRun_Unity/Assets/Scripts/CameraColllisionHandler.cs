using UnityEngine;

public class CameraCollisionHandler : MonoBehaviour
{
    public Transform player;          // El objeto que la c�mara sigue (por ejemplo, el jugador)
    public float collisionRadius = 0.3f; // Radio del SphereCast
    public LayerMask collisionLayers;    // Capas con las que la c�mara debe colisionar
    public float smoothSpeed = 10f;      // Velocidad de ajuste de la c�mara
    public float minVerticalAngle = -30f; // �ngulo m�nimo para mirar hacia abajo
    public float maxVerticalAngle = 60f; // �ngulo m�ximo para mirar hacia arriba

    private Vector3 defaultOffset;       // Offset inicial de la c�mara respecto al jugador
    private float verticalRotation = 0f; // Rotaci�n vertical acumulada

    void Start()
    {
        // Calcula la posici�n inicial de la c�mara respecto al jugador
        defaultOffset = transform.localPosition;
    }

    void LateUpdate()
    {
        // Manejo de la rotaci�n vertical de la c�mara
        float mouseY = Input.GetAxis("Mouse Y");
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        // Aplica la rotaci�n vertical a la c�mara
        transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0f);

        // Calcula la posici�n deseada de la c�mara
        Vector3 desiredPosition = player.position + player.TransformVector(defaultOffset);

        // Lanza un SphereCast desde el jugador hacia la posici�n deseada
        Ray ray = new Ray(player.position + Vector3.up * 1.0f, (desiredPosition - (player.position + Vector3.up * 1.0f)).normalized);
        RaycastHit hit;

        if (Physics.SphereCast(ray, collisionRadius, out hit, defaultOffset.magnitude, collisionLayers))
        {
            // Ajusta la posici�n de la c�mara al punto de colisi�n
            float minDistance = 0.5f; // Distancia m�nima permitida
            float adjustedDistance = Mathf.Max(hit.distance - collisionRadius, minDistance);
            transform.position = Vector3.Lerp(transform.position, player.position + ray.direction * adjustedDistance, Time.deltaTime * smoothSpeed);
        }
        else
        {
            // Si no hay colisi�n, mueve la c�mara a la posici�n deseada
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }
    }
}