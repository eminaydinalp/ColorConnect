using UnityEngine;
using ElephantSDK;
using Rentire.Core;

public class ElephantManager : Singleton<ElephantManager>
{
    GameState previousState = GameState.WaitingToStart;
    int currentLevelNo => LevelManager.Instance.CurrentLevelNo;


    #region Level Events

    public void LevelCompleted(int _level)
    {
        Elephant.LevelCompleted(_level);
    }
    public void LevelStarted(int _level)
    {
        Elephant.LevelStarted(_level);
    }
    public void LevelFailed(int _level)
    {
        Elephant.LevelFailed(_level);
    }

    #endregion

    #region Enable / Disable / State Control

    private void GameStateChanged()
    {
        var state = gameState;

        if (state == GameState.Running)
        {
            LevelStarted(currentLevelNo);
        }

        else if (state == GameState.Success)
        {
            LevelCompleted(currentLevelNo);
        }

        else if (state == GameState.Fail)
        {
            LevelFailed(currentLevelNo);
        }

        previousState = state;
    }

    private void OnEnable()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.GameStatus.OnValueChange += GameStateChanged;
        }
    }



    private void OnDisable()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.GameStatus.OnValueChange -= GameStateChanged;
        }
    }

    #endregion


}