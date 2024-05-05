using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;

    private List<GameObject> players = new List<GameObject>(); // Liste der Spieler auf dem Feld

    void Start()
    {
        // Holen der vorhandenen Spieler auf dem Spielfeld
        GameObject[] existingPlayers = GameObject.FindGameObjectsWithTag("Player");

        // Überprüfen, ob bereits genau zwei Spieler auf dem Spielfeld vorhanden sind
        if (existingPlayers.Length == 2)
        {
            foreach (GameObject player in existingPlayers)
            {
                players.Add(player);
            }
        }
        else
        {
            Debug.LogWarning("Es sollten genau zwei Spieler auf dem Spielfeld vorhanden sein.");
            return;
        }
    }

    // Methode zum Spawnen zusätzlicher Spieler, falls erforderlich
    void SpawnPlayers()
    {
        // Überprüfen, ob bereits genau zwei Spieler vorhanden sind
        if (players.Count != 2)
        {
            Debug.LogWarning("Es sollten genau zwei Spieler vorhanden sein, bevor neue Spieler gespawnt werden können.");
            return;
        }

        // Spawnen neuer Spieler basierend auf den vorhandenen Spielern
        foreach (GameObject player in players)
        {
            Vector3 spawnPosition = player.transform.position; // Verwende die Position des vorhandenen Spielers als Spawnposition
            Instantiate(player, spawnPosition, Quaternion.identity);
        }
    }

    public void GameOver()
    {
        // Game Over UI anzeigen
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        // Neustart der Szene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        // Zurück zum Hauptmenü
        SceneManager.LoadScene("MainMenu");
    }
}
