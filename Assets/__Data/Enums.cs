
public enum GameState
{
    WaitingToStart,
    Running,
    Success,
    Fail
}

public enum PlayerState
{
    Idle,
    Running,
    SlowingDown,
    Jumping,
    Finish,
}

public enum UpdateType
{
    Update,
    FixedUpdate,
    LateUpdate
}

public enum DirectionFlow
{
    Right,
    Left,
    middle
}
