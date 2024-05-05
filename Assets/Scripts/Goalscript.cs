using UnityEngine;

public class TriggerText : MonoBehaviour
{
    public GameObject textToShow; // Der Text, der angezeigt werden soll
    private bool playerInsideTrigger = false; // Flag, um zu verfolgen, ob der Spieler im Trigger ist

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Spieler ist im Trigger
            playerInsideTrigger = true;

            // Setze isDead im PlayerMovement-Skript auf true, um die Bewegung des Spielers zu stoppen
            other.GetComponent<PlayerMovement>().SetIsDead(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Spieler hat den Trigger verlassen
            playerInsideTrigger = false;
        }
    }

    private void Update()
    {
        if (playerInsideTrigger)
        {
            // Aktiviere den Text, wenn der Spieler den Trigger betritt
            textToShow.SetActive(true);
        }
        else
        {
            // Deaktiviere den Text, wenn der Spieler den Trigger verlässt
            textToShow.SetActive(false);
        }
    }
}
