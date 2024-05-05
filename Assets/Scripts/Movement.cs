
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float gridSize = 1f; // Größe eines Grids, könnte je nach deiner Szene variieren
    private Vector2 lastPosition;
    private bool isMoving = false;
    public GameObject tilePrefab; // Prefab des Tiles

    public void SetIsDead(bool value)
    {
        isDead = value;
    }

    public GameManagerScript gameManager;

    private bool isDead;

    private void Update()
    {
        if (!isMoving && !isDead) // Überprüfen, ob der Spieler sich bewegen kann und nicht tot ist
        {
            // Überprüfen, ob eine Bewegungstaste gedrückt wurde
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector2.down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector2.right);
            }
        }
    }

    private void Move(Vector2 direction)
    {
        // Speichere die aktuelle Position als die letzte Position
        lastPosition = transform.position;

        // Zielposition berechnen
        Vector2 nextPosition = (Vector2)transform.position + direction * gridSize;

        // Überprüfen, ob die Zielposition valide ist
        if (IsValidMove(nextPosition))
        {
            // Platziere das Tile auf der aktuellen Position des Spielers
            PlaceTile(transform.position);

            // Bewegung starten
            StartCoroutine(MoveToTarget(nextPosition));

            // Überprüfen, ob der Spieler sich im nächsten Zug nicht mehr bewegen kann
            if (!CanMoveInAnyDirection(nextPosition))
            {
                isDead = true;
                gameManager.GameOver();
                Debug.Log("Du kannst dich nicht mehr bewegen!");
            }
        }
    }

    private bool IsValidMove(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (!collider.isTrigger)
            {
                // Es gibt mindestens einen nicht-trigger Collider an der Position
                return false;
            }
        }
        // Es gibt keine nicht-trigger Collider an der Position
        return true;
    }

    private System.Collections.IEnumerator MoveToTarget(Vector2 targetPosition)
    {
        isMoving = true;

        while (Vector2.Distance((Vector2)transform.position, targetPosition) > 0.01f)
        {
            // Bewegung durchführen
            transform.position = Vector2.MoveTowards((Vector2)transform.position, targetPosition, Time.deltaTime * 5f);
            yield return null;
        }

        // Bewegung beenden
        transform.position = targetPosition;
        isMoving = false;
    }

    private void PlaceTile(Vector2 position)
    {
        // Tile an der Position des Spielers platzieren
        Instantiate(tilePrefab, position, Quaternion.identity);
    }

    private bool CanMoveInAnyDirection(Vector2 position)
    {
        // Überprüfe, ob der Spieler sich in irgendeine Richtung bewegen kann
        return IsValidMove(position + Vector2.up * gridSize) || 
               IsValidMove(position + Vector2.down * gridSize) || 
               IsValidMove(position + Vector2.left * gridSize) || 
               IsValidMove(position + Vector2.right * gridSize);
    }
}