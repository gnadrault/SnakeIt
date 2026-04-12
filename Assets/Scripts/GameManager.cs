using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public enum GameState
{
    Gameplay,
    Pause,
    End
}

/// <summary>
/// Manage global game configuration
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float speedFactor = 1f; // Use to increase stages difficulty (NewGame+)
    public GameState State { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Init the game with Gameplay state
    /// </summary>
    public void Start()
    {
        GameEvents.OnGameStateChanged.Invoke(GameState.Gameplay);
    }

    /// <summary>
    /// Update the game state
    /// </summary>
    public void SetGameState(GameState newState)
    {
        State = newState;
        GameEvents.OnGameStateChanged.Invoke(newState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TooglePauseMenu();
        }
    }

    /// <summary>
    /// Open/Close Pause menu
    /// </summary>
    private void TooglePauseMenu()
    {
        switch (State)
        {
            case GameState.Pause:
                SetGameState(GameState.Gameplay);
                break;
            case GameState.Gameplay:
                SetGameState(GameState.Pause);
                break;
        }
    }

    /// <summary>
    /// Handle new game state event received
    /// </summary>
    /// <param name="state"></param>
    private void HandleState(GameState state)
    {
        switch (state)
        {
            case GameState.Gameplay: // Unload Pause/End menus if present
                SceneUtils.UnloadIfLoaded("PauseMenu");
                SceneUtils.UnloadIfLoaded("EndMenu");
                break;
            case GameState.Pause: // Load Pause menu
                StopAllCoroutines();
                SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
                break;
            case GameState.End: // Load End menu
                StopAllCoroutines();
                SceneManager.LoadSceneAsync("EndMenu", LoadSceneMode.Additive);
                break;
        }
    }
    
    private void OnEnable()
    {
        GameEvents.OnGameStateChanged += HandleState;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStateChanged -= HandleState;
    }
}
