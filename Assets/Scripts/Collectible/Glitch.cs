using Snake;

namespace Collectible
{
    public class Glitch: Collectible
    {
        protected override void ApplyEffect()
        {
            SnakeController.Instance.Invert();
            base.ApplyEffect();
        }
    }
}