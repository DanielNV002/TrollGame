using UnityEngine;

public class MovingPlatform2D : MonoBehaviour
{
    [Header("Movimiento de la Plataforma")]
    public Vector2 moveDirection = Vector2.up; // Dirección editable
    public float moveDistance = 5f; // Distancia editable
    public float moveSpeed = 2f; // Velocidad de movimiento

    [Header("Detección del Jugador")]
    public Transform activationZone; // Empty Object con BoxCollider2D

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool moving = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtiene el Rigidbody2D
        if (rb == null)
        {
            Debug.LogError("Falta un Rigidbody2D en la plataforma.");
            return;
        }

        startPosition = transform.position;
        targetPosition = startPosition + moveDirection.normalized * moveDistance;
    }

    void Update()
    {
        // Si no se está moviendo y el jugador está en la zona de activación
        if (!moving && PlayerInZone())
        {
            moving = true; // Marca la plataforma como en movimiento
            Debug.Log("Jugador detectado en la zona de activación.");
            StartCoroutine(MovePlatform());
        }
    }

    bool PlayerInZone()
    {
        Collider2D col = Physics2D.OverlapBox(activationZone.position, activationZone.localScale, 0);
        if (col != null && col.CompareTag("Player"))
        {
            Debug.Log("El jugador está dentro del área de activación.");
            return true;
        }
        return false;
    }

    System.Collections.IEnumerator MovePlatform()
    {
        float elapsedTime = 0f;
        while (elapsedTime < moveDistance / moveSpeed)
        {
            rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, elapsedTime / (moveDistance / moveSpeed)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rb.MovePosition(targetPosition);
    }

    // Método para resetear la plataforma
    public void ResetPlatform()
    {
        transform.position = startPosition; // Resetea la posición de la plataforma
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Detiene cualquier movimiento de la plataforma
        }
        moving = false; // Resetea el estado de movimiento
        Debug.Log("Plataforma reiniciada.");
    }

    void OnDrawGizmos()
    {
        if (activationZone)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(activationZone.position, activationZone.localScale);
        }
    }
}
