using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int MenuSceneIndex = 0;
    public int GameSceneIndex = 1;

    public bool IsCursorVisibleInGame = false;

    // Awake.
    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Start.
    private void Start()
    {
        Debug.unityLogger.logEnabled = Debug.isDebugBuild;
        
        ChangeGameState(CurrentGameState);
    }

    // Change game state.
    private void ChangeGameState(GameState gameState)
    {
        var previousGameState = CurrentGameState;

        CurrentGameState = gameState;

        Cursor.visible = (CurrentGameState != GameState.Menu) ? IsCursorVisibleInGame : true;

        if (previousGameState == GameState.Menu && CurrentGameState == GameState.Game)
        {
            SceneManager.LoadSceneAsync(GameSceneIndex, LoadSceneMode.Single);
        }
    }
}