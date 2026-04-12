using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    /// <summary>
    /// Manage the stage timer
    /// </summary>
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        
        private float _currentTime;
        private bool _isRunning;

        private void OnEnable()
        {
            GameEvents.OnFistMove += StartTimer;
            GameEvents.OnStageLoaded += Reset;
        }

        private void OnDisable()
        {
            GameEvents.OnFistMove -= StartTimer;
            GameEvents.OnStageLoaded -= Reset;
        }
    
        /// <summary>
        /// Reset the timer when new stage loaded
        /// </summary>
        /// <param name="stageConfig"></param>
        private void Reset(StageConfig stageConfig)
        {
            _currentTime = Mathf.CeilToInt(stageConfig.timerDuration / GameManager.Instance.speedFactor);
            _isRunning = false;
        }

        void StartTimer()
        {
            _isRunning = true;
        }

        void Update()
        {
            if (_isRunning)
            {
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0)
                {
                    _currentTime = 0f;
                    GameEvents.OnTimerEnd?.Invoke(); // GameOver if timer <= 0
                    _isRunning = false;
                }
            }

            int seconds = Mathf.FloorToInt(_currentTime);
            int milliseconds = Mathf.FloorToInt((_currentTime - seconds) * 100);

            timerText.text = $"{seconds:D2}:{milliseconds:D2}";
        }
    }
}
