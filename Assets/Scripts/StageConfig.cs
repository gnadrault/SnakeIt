using UnityEngine;

/// <summary>
/// Stage configuration
/// Used for each stage to apply specific parameters
/// </summary>
[CreateAssetMenu(menuName = "StageConfig")]
public class StageConfig: ScriptableObject
{
    public string stageName;
    public string stageNum;
    public float timerDuration;
}