using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0; // Player's score
    public bool isGameOver = false; // Game over flag

    private EnemyController[] enemies;
    public WinningUI winningUI;
      

    void Awake()
    {
        
        enemies = Object.FindObjectsByType<EnemyController>(FindObjectsSortMode.None);


        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()   
    {
        CheckEnemiesStatus();
    }

    void CheckEnemiesStatus()
    {
        bool allEnemiesDead = true;

        foreach (var enemy in enemies)
        {
            if (enemy != null && enemy.currentHealth > 0)
            {
                allEnemiesDead = false;
                break;
            }
        }

        if (allEnemiesDead)
        {
            // Show the winning UI
            FindObjectOfType<WinningUI>(includeInactive: true).ShowWinningUI();
        }
    }


    public void AddScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            // Update UI or other game elements
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        // Handle game over logic, e.g., show game over screen, stop game, etc.
    }

    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        // Restart game logic
    }
}

