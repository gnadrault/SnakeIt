using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

/// <summary>
/// Manage game stages
/// </summary>
public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    
    [SerializeField] private List<StageConfig> listStages;
    [SerializeField] private TextMeshProUGUI stageText;
    private int _currentStageIndex;
    private Scene _currentSceneStage;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        LoadStage(listStages[0]); // Load the first stage
    }

    /// <summary>
    /// Reset all stages (NewGame+)
    /// </summary>
    public void Reset()
    {
        _currentStageIndex = 0;
        Start();
        GameManager.Instance.SetGameState(GameState.Gameplay);
    }
    
    /// <summary>
    /// Loose -> Reload the same stage
    /// </summary>
    private void Retry()
    {
        LoadStage(listStages[_currentStageIndex]);
    }
    
    /// <summary>
    /// Win -> Load the next Stage
    /// </summary>
    private void LoadNextStage()
    {
        int nextStageIndex = _currentStageIndex + 1;
        if (nextStageIndex >= listStages.Count)
        {
            print("End");
            GameManager.Instance.SetGameState(GameState.End);
            return;
        }
        print("Next Stage: " + nextStageIndex);
        LoadStage(listStages[nextStageIndex]);
        _currentStageIndex = nextStageIndex;
    }

    /// <summary>
    /// Manage stages loading
    /// Unload the current stage & Load the new stage
    /// </summary>
    /// <param name="nextStage"></param>
    private void LoadStage(StageConfig nextStage)
    {
        SceneUtils.UnloadIfLoaded(_currentSceneStage); // Unload the current stage scene
        AsyncOperation op = SceneManager.LoadSceneAsync(nextStage.stageName, LoadSceneMode.Additive);
        op.completed += (operation) => OnStageLoaded(nextStage);
    }
    
    /// <summary>
    /// When the new stage is loaded, send the event
    /// </summary>
    /// <param name="nextStage"></param>
    private void OnStageLoaded(StageConfig nextStage)
    {
        Scene nextStageScene = SceneManager.GetSceneByName(nextStage.stageName);
        _currentSceneStage = nextStageScene;
        stageText.text = $"Stage {nextStage.stageNum}";
        
        SceneManager.SetActiveScene(nextStageScene);
        GameEvents.OnStageLoaded?.Invoke(nextStage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // For debug only
        {
            LoadNextStage();
        }
    }

    private void OnEnable()
    {
        GameEvents.OnSnakeCollision += Retry;
        GameEvents.OnTimerEnd += Retry;
        GameEvents.OnStageComplete += LoadNextStage;
    }

    private void OnDisable()
    {
        GameEvents.OnSnakeCollision -= Retry;
        GameEvents.OnTimerEnd -= Retry;
        GameEvents.OnStageComplete -= LoadNextStage;
    }
}
