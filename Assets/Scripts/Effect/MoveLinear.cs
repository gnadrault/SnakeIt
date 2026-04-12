using UnityEngine;

namespace Effect
{
    public class MoveLinear : MonoBehaviour
    {
        [SerializeField] private float moveRange = 3f;
        [SerializeField] private float speed = 2f;
        [SerializeField] private Vector3 direction;
        
        private Vector3 _startPosition;
    
        void Start()
        {
            _startPosition = transform.position;
            speed *= GameManager.Instance.speedFactor;
        }

        private void Update()
        {
            float offset = Mathf.PingPong(Time.time * speed, moveRange);
            transform.position = _startPosition + direction * (offset - moveRange / 2f);
        }
    }
}
