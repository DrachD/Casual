using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////
// class for managing the state of the game
////////////////////////////////////////////////////////
public class MenuPaused : MonoBehaviour
{
    // select the key to stop the game 
    [SerializeField] KeyCode _keyMenuPaused;

    public bool isMenuPaused = false;

    public bool gameOver = false;

    private void Update()
    {
        ActivateMenu();
    }

    // resume game
    public void NoPause()
    {
        gameOver = false;
        isMenuPaused = false;
        Time.timeScale = 1f;
    }

    // real-time game state management
    void ActivateMenu()
    {
        if (Input.GetKeyDown(_keyMenuPaused))
        {
            isMenuPaused = !isMenuPaused;
        }

        if (!gameOver && !isMenuPaused)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }
}
