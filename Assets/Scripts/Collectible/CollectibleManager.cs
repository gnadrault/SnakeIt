using UnityEngine;
using Utils;

namespace Collectible
{
    public class CollectibleManager : MonoBehaviour
    {
        private int _nbCollectible;

        private void OnEnable()
        {
            GameEvents.OnCollect += DecreaseCollectibleNb;
            GameEvents.OnStageLoaded += SetNbFood;
        }

        private void OnDisable()
        {
            GameEvents.OnCollect -= DecreaseCollectibleNb;
            GameEvents.OnStageLoaded -= SetNbFood;
        }
        
        private void SetNbFood(StageConfig stageConfig)
        {
            _nbCollectible = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;
        }

        /// <summary>
        /// Object collected, decrease _nbCollectible
        /// Stage complete if all collected
        /// </summary>
        private void DecreaseCollectibleNb()
        {
            _nbCollectible--;
            if (_nbCollectible <= 0)
            {
                GameEvents.OnStageComplete?.Invoke();
            }
        }
    }
}
