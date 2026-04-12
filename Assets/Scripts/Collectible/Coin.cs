using Snake;

namespace Collectible
{
    /// <summary>
    /// Default collectible
    /// </summary>
    public class Coin : Collectible
    {
        protected override void ApplyEffect()
        {
            SnakeController.Instance.Grow();
            base.ApplyEffect();
        }
    }
}
