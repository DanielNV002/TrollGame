using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerReset playerReset = other.GetComponent<PlayerReset>();
            if (playerReset != null)
            {
                playerReset.ResetPosition(); // Llamamos al método de reinicio
                Debug.Log("¡Jugador tocó los pinchos! Reiniciando posición...");
            }
        }
    }
}
