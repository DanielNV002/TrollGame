using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    private Vector2 startPosition; // Posición inicial del jugador
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Guarda la posición inicial
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Si el jugador presiona "R"
        {
            ResetGame();
        }
    }

    public void ResetPosition()
    {
        ResetGame(); // Llama a la función principal de reinicio
    }

    private void ResetGame()
    {
        transform.position = startPosition; // Reinicia la posición del jugador
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Detiene el movimiento
        }

        // Usamos FindObjectsByType para encontrar todas las plataformas
        MovingPlatform2D[] platforms = Object.FindObjectsByType<MovingPlatform2D>(FindObjectsSortMode.None);
        foreach (MovingPlatform2D platform in platforms)
        {
            platform.ResetPlatform(); // Llama a ResetPlatform en cada plataforma
        }

        Debug.Log("Juego reiniciado: Jugador y plataformas reiniciados.");
    }
}
