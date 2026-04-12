using Snake;

namespace Collectible
{
    /// <summary>
    /// Collectible activate ghost mode (invisible)
    /// </summary>
    public class Ghost: Collectible
    {
        public float duration = 1f;
        
        protected override void ApplyEffect()
        {
            SnakeController.Instance.ActivateGhost(duration);
            base.ApplyEffect();
        }
    }
}