
using Effect;

namespace Collectible
{
    /// <summary>
    /// Collectible slowdown speed of player
    /// </summary>
    public class Drag : Collectible
    {
        protected override void ApplyEffect()
        {
            SlowTime.Instance.CallSlowTime();
            base.ApplyEffect();
        }
    }
}
