using System.Collections.Generic;
using Rentire.Core;

public class PlayerStateManager : RMonoBehaviour
{
    public PlayerState currentPlayerState;
    private readonly ObservedValue<PlayerState> playerState = new(PlayerState.Idle);
    public IList<IPlayerStateObserver> playerObserverList;

    private void OnEnable()
    {
        playerState.OnValueChange += PlayerStateChanged;
        gameManager.GameStatus.OnValueChange += GameStateChanged;
    }

    private void OnDisable()
    {
        playerState.OnValueChange -= PlayerStateChanged;
        if (gameManager)
            gameManager.GameStatus.OnValueChange -= GameStateChanged;
    }

    public void AddListener(IPlayerStateObserver observer)
    {
        if (playerObserverList == null)
            playerObserverList = new List<IPlayerStateObserver>();

        playerObserverList.Add(observer);
    }

    public void SetPlayerRunning()
    {
        playerState.Value = PlayerState.Running;
    }

    public void SetPlayerIdle()
    {
        playerState.Value = PlayerState.Idle;
    }
    
     public void SetPlayerSlowDown()
    {
        playerState.Value = PlayerState.SlowingDown;
    }


    private void PlayerStateChanged()
    {
        currentPlayerState = playerState.Value;
        Log.Info("Player state changed : " + currentPlayerState);
        if(playerObserverList != null)
        for (var i = 0; i < playerObserverList.Count; i++) playerObserverList[i].OnStateChanged();
    }

    private void GameStateChanged()
    {
        if (gameState == GameState.Running)
            SetPlayerRunning();
    }
    
}