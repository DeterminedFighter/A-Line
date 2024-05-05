using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TriggerOutline : MonoBehaviour
{
    public Color outlineColor = Color.blue; // Farbe des Rands
    public float outlineWidth = 0.05f; // Breite des Rands
    public float outlineOffset = 0.1f;
    public int numSegments = 1;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        // Zugriff auf den LineRenderer des Triggers
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Einstellungen für den LineRenderer
        lineRenderer.startColor = outlineColor;
        lineRenderer.endColor = outlineColor;
        lineRenderer.startWidth = outlineWidth;
        lineRenderer.endWidth = outlineWidth;
        lineRenderer.loop = true; // Damit die Linie einen geschlossenen Loop bildet

        lineRenderer.material.color = outlineColor;

        // Aktualisiere die Positionen der Linie
        UpdateLinePositions();
    }

    private void UpdateLinePositions()
    {
        Vector3[] positions = new Vector3[4];

        // Berechnung der Eckpunkte des Quadrats basierend auf der Größe des Triggers
        Vector2 halfSize = transform.localScale / 2f + new Vector3(outlineOffset, outlineOffset, 0f);
        positions[0] = new Vector3(-halfSize.x, halfSize.y, 0f) + transform.position; // Obere linke Ecke
        positions[1] = new Vector3(halfSize.x, halfSize.y, 0f) + transform.position;  // Obere rechte Ecke
        positions[2] = new Vector3(halfSize.x, -halfSize.y, 0f) + transform.position; // Untere rechte Ecke
        positions[3] = new Vector3(-halfSize.x, -halfSize.y, 0f) + transform.position; // Untere linke Ecke

        // Setze die Positionen für den LineRenderer
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }
}
