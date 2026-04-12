using Snake;
using UnityEngine;

namespace Collectible
{
    /// <summary>
    /// Collectible boost speed of the player
    /// </summary>
    public class Surge: Collectible
    {
        [SerializeField] private float duration = 1f;
        [SerializeField] private float speed = 3f;
        
        protected override void ApplyEffect()
        {
            speed *= GameManager.Instance.speedFactor;
            SnakeController.Instance.Boost(duration, speed);
            base.ApplyEffect();
        }
    }
}