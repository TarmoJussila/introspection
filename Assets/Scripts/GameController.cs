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
/// Game controller. Controls game states. Persistent.
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameState CurrentGameState;

    public int MenuSceneIndex = 0;
    public int GameSceneIndex = 1;

    public bool IsCursorVisibleInGame = false;

    // Awake. Persistent.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start.
    private void Start()
    {
        Debug.unityLogger.logEnabled = Debug.isDebugBuild;
        
        ChangeGameState(CurrentGameState);
    }

    // Update.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            if (CurrentGameState == GameState.Menu)
            {
                if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick1Button7))
                {
                    StartGame();
                }
                else
                {
                    Application.Quit();
                }
            }
            else if (CurrentGameState == GameState.Game)
            {
                ChangeGameState(GameState.Menu);
            }
        }
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
        else if (previousGameState == GameState.Game && CurrentGameState == GameState.Menu)
        {
            SceneManager.LoadSceneAsync(MenuSceneIndex, LoadSceneMode.Single);
        }
    }

    // Start game.
    public void StartGame()
    {
        ChangeGameState(GameState.Game);
    }
}