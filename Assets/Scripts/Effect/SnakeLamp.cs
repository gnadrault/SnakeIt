using Snake;
using UnityEngine;

namespace Effect
{
    public class SnakeLamp : MonoBehaviour
    {
        private void Update()
        {
            Vector3 snakeDirection = SnakeController.Instance.currentDirection;
            if (snakeDirection != Vector3.zero)
            {
                // Update the snake lamp rotation from snake direction
                float angle = Mathf.Atan2(snakeDirection.y, snakeDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            }
        }
    }
}
