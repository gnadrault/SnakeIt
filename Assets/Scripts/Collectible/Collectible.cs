using Feedback;
using Snake;
using UnityEngine;
using Utils;

namespace Collectible
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleSystemPrefab;
        [SerializeField] private AudioClip collectSound;

        private ParticleSystem _particleSystem;

        private void Start()
        {
            // Init particle system
            Color particleColor = new Color(
                gameObject.GetComponent<SpriteRenderer>().color.r, 
                gameObject.GetComponent<SpriteRenderer>().color.g, 
                gameObject.GetComponent<SpriteRenderer>().color.b, 
                0.5f);
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            _particleSystem = Instantiate(particleSystemPrefab, position, Quaternion.identity);
            var main = _particleSystem.main;
            main.startColor = particleColor;
        }
        
        /// <summary>
        /// Collision with the player
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<SnakeController>())
            {
                ApplyEffect();
                GameEvents.OnCollect?.Invoke();
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Apply the object effect
        /// </summary>
        protected virtual void ApplyEffect()
        {
            ScreenShake.instance.Shake(0.2f, 0.05f);
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            _particleSystem.Play();
        }
    }
}
