using UnityEngine;
using TMPro; // Importa la librería de TextMeshPro

public class Cronometro : MonoBehaviour
{
    // Referencia al texto de la UI donde se mostrará el cronómetro
    public TextMeshProUGUI textoCronometro; // Cambia a TextMeshProUGUI

    // Variables para medir el tiempo
    private float tiempo;
    private bool cronometroActivo;

    void Start()
    {
        // Activar el cronómetro inmediatamente al comenzar el juego
        cronometroActivo = true;
        tiempo = 0f;
    }

    void Update()
    {
        if (cronometroActivo)
        {
            // Aumenta el tiempo en función del tiempo transcurrido (realmente son segundos)
            tiempo += Time.deltaTime;

            // Mostrar el tiempo en el formato adecuado (minutos:segundos:milesimas)
            float minutos = Mathf.FloorToInt(tiempo / 60);
            float segundos = Mathf.FloorToInt(tiempo % 60);
            float milisegundos = Mathf.FloorToInt((tiempo * 1000) % 1000);

            // Actualizar el texto del cronómetro
            textoCronometro.text = string.Format("{0:00}:{1:00}:{2:000}", minutos, segundos, milisegundos);
        }
    }
}
