using UnityEngine;

/// <summary>
/// Game states.
/// </summary>
public enum GameState
{
    Menu,   // Menu.
    Intro,  // Intro (start cutscene).
    Game,   // Game.
    Outro   // Outro (end cutscene).
}

/// <summary>
/// Game controller. Controls game states.
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameState CurrentGameState;

    // Awake.
    private void Awake()
    {
        Instance = this;
    }

    // Start.
    private void Start()
    {
        ChangeGameState(CurrentGameState);
    }

    // Change game state.
    private void ChangeGameState(GameState gameState)
    {
        CurrentGameState = gameState;
    }
}