using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    [SerializeField] private int gold; // amount seralized in editor
    // text field for gold
    [SerializeField] private TMPro.TextMeshProUGUI goldText;

    [SerializeField] private int lives; // amount seralized in editor
    // text field for lives
    [SerializeField] private TMPro.TextMeshProUGUI livesText;

    // spawner
    [SerializeField] private GameObject spawner;
    [SerializeField] private int winningRound = 3;

    // Panels for Gameover and Win condtions
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    public void Awake()
    {
        setGold(gold);
        setLives(lives);
    }

    private void Update()
    {
        if (lives == 0)
        {
            loseLevel();
        }

        if (spawner.GetComponent<EnemySpawnManager>().getCurrWave() == winningRound && spawner.GetComponent<EnemySpawnManager>().getEnemiesAlive() == 0 && lives > 0)
        {
            winLevel();
        }


    }

    public void beginRound()
    {
        Debug.Log("Begin Round");
        spawner.GetComponent<EnemySpawnManager>().beginRound();

    }

    public void loseLevel()
    {
        Debug.Log("Game Over");
        spawner.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void winLevel()
    {
        Debug.Log("You Win!");
        spawner.SetActive(false);
        winPanel.SetActive(true);

    }

    // gold methods
    public void setGold(int goldAmt)
    {
        gold = goldAmt;
        goldText.text = "Gold: " + gold;
    }

    public int getGold()
    {
        return gold;
    }

    public void addGold(int amount)
    {
        gold = gold + amount;
    }

    // lives methods
    public void setLives(int livesAmt)
    {
        lives = livesAmt;
        livesText.text = "Lives: " + lives;
    }

    public int getLives()
    {
        return lives;
    }
}
