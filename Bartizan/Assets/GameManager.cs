using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    [SerializeField] private int gold;
    // text field for gold
    [SerializeField] private TMPro.TextMeshProUGUI goldText;

    [SerializeField] private int lives;
    // text field for lives
    [SerializeField] private TMPro.TextMeshProUGUI livesText;

    public void Awake()
    {
        setGold(gold);
        setLives(lives);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
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
