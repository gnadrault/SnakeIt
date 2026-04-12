using Environment.Interface;
using Feedback;
using Snake;
using UnityEngine;

namespace Environment
{
    public class Portal : MonoBehaviour, IInteract
    {
        public GameObject twinPortal;
        
        /// <summary>
        /// Teleport the player to the twin portal
        /// </summary>
        public void Interact()
        {
            SnakeController.Instance.transform.position = twinPortal.transform.position + SnakeController.Instance.currentDirection;
            ScreenShake.instance.Shake(0.3f, 0.1f);
            FreezeFrame.instance.Freeze(0.3f);
        }
        
        void Update()
        {
            float t = Time.time;
            transform.localScale = Vector3.one * (1f + Mathf.Sin(t * 6f) * 0.05f);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(t * 4f) * 3f);
        }
    }
}
