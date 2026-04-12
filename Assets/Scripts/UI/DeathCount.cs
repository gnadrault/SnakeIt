using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    public class DeathCount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI deathCountText;
        
        private int _deathCount;
    
        private void OnEnable()
        {
            GameEvents.OnSnakeCollision += Count;
            GameEvents.OnTimerEnd += Count;
        }

        private void OnDisable()
        {
            GameEvents.OnSnakeCollision -= Count;
            GameEvents.OnTimerEnd -= Count;
        }

        /// <summary>
        /// Update the death count
        /// </summary>
        private void Count()
        {
            _deathCount++;
            deathCountText.text = $"Death {_deathCount}";
        }
    }
}
