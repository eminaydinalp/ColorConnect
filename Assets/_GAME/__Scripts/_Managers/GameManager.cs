using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rentire.Core;

public class GameManager : Singleton<GameManager>
{
    public ObservedValue<GameState> GameStatus = new ObservedValue<GameState>(GameState.WaitingToStart);

    public GameState CurrentGameState = GameState.WaitingToStart;

    public bool isFinish;
    private void Start()
    {
        CurrentGameState = GameState.WaitingToStart;
    }

    /// <summary>
    /// Change game state to Running
    /// </summary>
    public void SetGameRunning()
    {
        SetGameState(GameState.Running);
    }

    /// <summary>
    /// Change Game state to Success
    /// </summary>
    public void SetGameSuccess()
    {
        if(gameState != GameState.Running)
        {
            return;
        }

        SetGameState(GameState.Success);

        LevelManager.Instance.IncreaseLevelNo();
    }

    /// <summary>
    /// Change Game state to Fail
    /// </summary>
    public void SetGameFail()
    {
        if (gameState != GameState.Running)
        {
            return;
        }

        SetGameState(GameState.Fail);
    }

    
    public void SetGameState(GameState gameState)
    {
        GameStatus.Value = gameState;

        Log.Info("Current game state is set to " + gameState);
    }

    private void GameStateChanged()
    {
        CurrentGameState = GameStatus.Value;
    }

    private void OnEnable()
    {
        GameStatus.OnValueChange += GameStateChanged;
    }

    private void OnDisable()
    {
        GameStatus.OnValueChange -= GameStateChanged;
    }

}
