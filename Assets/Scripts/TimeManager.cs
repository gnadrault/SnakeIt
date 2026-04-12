using UnityEngine;
using Utils;

/// <summary>
/// Centralized class to manage TimeScale
/// </summary>
public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Check if autorised to change timeScale
    /// </summary>
    /// <returns></returns>
    private bool AllowTimeModification()
    {
        return GameManager.Instance.State == GameState.Gameplay;
    }

    /// <summary>
    /// Update the timeScale
    /// </summary>
    /// <param name="scale"></param>
    public void SetTimeScale(float scale)
    {
        if (!AllowTimeModification())
            return;

        Time.timeScale = scale;
    }

    private void OnEnable()
    {
        GameEvents.OnGameStateChanged += HandleState;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStateChanged -= HandleState;
    }

    /// <summary>
    /// Handle the timeScale according to the new game state received
    /// </summary>
    /// <param name="state"></param>
    private void HandleState(GameState state)
    {
        switch (state)
        {
            case GameState.Gameplay:
                Time.timeScale = 1f;
                break;
            case GameState.Pause:
            case GameState.End:
                Time.timeScale = 0f;
                break;
        }
    }
}
