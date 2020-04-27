using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Text healthText;
    [SerializeField] Text gameOverText;

    private void Start()
    {
        healthText.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        health -= healthDecrease;
        healthText.text = health.ToString();

        if(health <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        gameOverText.text = "Game Over";
        print("Game has ended");
    }
}
