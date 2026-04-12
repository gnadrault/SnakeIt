using System;

namespace Utils
{
    /// <summary>
    /// Centralized class for game events
    /// </summary>
    public static class GameEvents
    {
        public static Action OnFistMove;
        public static Action OnCollect;
        public static Action OnStageComplete;
        public static Action OnTimerEnd;
        public static Action OnSnakeCollision;
        public static Action<StageConfig> OnStageLoaded;
        public static Action<GameState> OnGameStateChanged;
    }
}