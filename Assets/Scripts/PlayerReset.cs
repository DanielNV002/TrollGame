using UnityEngine;
using TMPro;

public class PlayerReset : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 checkpointPosition;
    private bool hasCheckpoint = false;
    private Rigidbody2D rb;

    public GameObject checkpointFlag;
    public TextMeshProUGUI deathCounterText;
    private int deathCount = 0;

    public Transform groundCheck; // Objeto vacío en los pies del jugador
    public LayerMask groundLayer; // Capa del suelo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        checkpointPosition = startPosition;

        if (checkpointFlag != null)
        {
            checkpointFlag.SetActive(false);
        }

        UpdateDeathCounter();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }

        if (Input.GetKeyDown(KeyCode.Q) && IsGrounded()) // Solo coloca checkpoint si está en el suelo
        {
            SetCheckpoint();
        }
    }

    public void SetCheckpoint()
    {
        checkpointPosition = transform.position;
        hasCheckpoint = true;

        if (checkpointFlag != null)
        {
            checkpointFlag.SetActive(true);
            checkpointFlag.transform.position = checkpointPosition + Vector2.up * 0.5f;
        }

        Debug.Log("Checkpoint establecido en: " + checkpointPosition);
    }

    public void ResetGame()
    {
        transform.position = hasCheckpoint ? checkpointPosition : startPosition;
        deathCount++;
        UpdateDeathCounter();

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        MovingPlatform2D[] platforms = Object.FindObjectsByType<MovingPlatform2D>(FindObjectsSortMode.None);
        foreach (MovingPlatform2D platform in platforms)
        {
            platform?.ResetPlatform();
        }

        Debug.Log("Jugador reiniciado en: " + transform.position);
    }

    private void UpdateDeathCounter()
    {
        if (deathCounterText != null)
        {
            deathCounterText.text = "Muertes: " + deathCount;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
    }
}
