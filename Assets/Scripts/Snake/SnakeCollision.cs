using Environment.Interface;
using UnityEngine;
using Utils;

namespace Snake
{
    public class SnakeCollision : MonoBehaviour
    {
        [SerializeField] private AudioClip collisionSound;
        
        private bool _hasCollided;
        
        /// <summary>
        /// Manage the snake collisions
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            IInteract interact = collision.collider.GetComponent<IInteract>();
            if (collision.gameObject.GetComponent<Environment.Obstacle>())
            {
                if (_hasCollided) return; // Prevent infinite collision from the snake
                _hasCollided = true;
                GameEvents.OnSnakeCollision?.Invoke();
                AudioSource.PlayClipAtPoint(collisionSound, transform.position);

            }
            else if (interact != null)
                interact.Interact();
        }
    }
}
