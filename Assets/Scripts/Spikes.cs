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
                playerReset.ResetPosition(); // Llamamos al m�todo de reinicio
                Debug.Log("�Jugador toc� los pinchos! Reiniciando posici�n...");
            }
        }
    }
}
