using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // graffiti
    public Text uiText;

    //states
    public enum State { NotStarted, Playing, GameOver, WonGame }

    //Mode
    public enum Mode { Normal, Crazy }
    public Mode currMode;

    // current state
    public State currState;

    public void PauseOrStartGame()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }


    // Enemy Manager
    EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {

        // start as not playing
        currState = State.NotStarted;

        // refresh UI
        RefreshUI();

        // find the enemy manager
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();

        // log error if it wasn't found
        if(enemyManager == null)
        {
            Debug.LogError("there needs to be an EnemyManager in the scene");
        }
    }

    void RefreshUI()
    {
        // act according to the state
        switch(currState)
        {
            case State.NotStarted:
                uiText.text = "Shoot here to begin";
                break;

            case State.Playing:
                uiText.text = "Enemies left: " + enemyManager.numEnemies;
                break;

            case State.GameOver:
                uiText.text = "Game Over! Shoot here";
                break;

            case State.WonGame:
                uiText.text = "YOU WON! Shoot here";
                break;
        }  

    }


    public void InitGame()
    {
        
        //don't initiate the game if the game is already running!
        if (currState == State.Playing) return;

        // set the state
        currState = State.Playing;

        // create enemy wave
        enemyManager.CreateEnemyWave(currMode);

        // show text on the graffiti
        RefreshUI();
    }

    // game over
    public void GameOver()
    {
        // do nothing if we were already on game over
        if (currState == State.GameOver) return;

        // set the state to game over
        currState = State.GameOver;

        // show text on the graffiti
        RefreshUI();

        // remove all enemies
        enemyManager.KillAll();
    }

    // checks whether we've won, and if we did win, refresh UI
    public void HandleEnemyDead()
    {
        if (currState != State.Playing) return;

        RefreshUI();

        // have we won the game?
        if(enemyManager.numEnemies <= 0)
        {
            // set the state of the game
            currState = State.WonGame;

            // show text on the graffiti
            RefreshUI();

            // remove all enemies
            enemyManager.KillAll();

            // change to crazymode
            currMode = Mode.Crazy;
        }
    }
}
