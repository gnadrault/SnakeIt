using System.Collections;
using System.Collections.Generic;
using Effect;
using Environment;
using UnityEngine;
using Utils;

namespace Snake
{
    public class SnakeController : MonoBehaviour
    {
        public static SnakeController Instance;
        
        [SerializeField] private GameObject snakeSegment;
        [SerializeField] private float distanceToMoveSegments = 0.4f;
        [HideInInspector] public Vector3 currentDirection = Vector3.zero;
        [Range(0, 2)] public float speed;
        public List<GameObject> listSegments;
        
        private bool _hasMoved;
        private bool _invertControls;
        private float _distanceTraveled;
        private Material _snakeMaterial;
        private Vector3 _wantedDirection = Vector3.zero;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            _snakeMaterial = new Material(sr.sharedMaterial); // Clone snake material for modifications
            sr.material = _snakeMaterial;
            speed *= GameManager.Instance.speedFactor;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            Vector3 inputDirection = Vector3.zero; // Temporary direction, use for invert controls
            // Check inputs
            if (Input.GetKeyDown(KeyCode.UpArrow) && _wantedDirection != Vector3.down)
                inputDirection = Vector3.up;
            if (Input.GetKeyDown(KeyCode.DownArrow) && _wantedDirection != Vector3.up)
                inputDirection = Vector3.down;
            if (Input.GetKeyDown(KeyCode.LeftArrow) && _wantedDirection != Vector3.right)
                inputDirection = Vector3.left;
            if (Input.GetKeyDown(KeyCode.RightArrow) && _wantedDirection != Vector3.left)
                inputDirection = Vector3.right;
            if (inputDirection != Vector3.zero)
            {
                if (_invertControls)
                {
                    inputDirection = -inputDirection;
                }
                _wantedDirection = inputDirection;
            }
        }
        
        private void FixedUpdate()
        {
            _distanceTraveled += speed;
            if (_distanceTraveled >= distanceToMoveSegments)
            {
                MoveSegments();
                _distanceTraveled = 0;
            }
            TryChangeDirection();
            transform.position += currentDirection * speed;
        }
        
        /// <summary>
        /// Move snake segments
        /// </summary>
        private void MoveSegments()
        {
            for (int i = listSegments.Count - 1; i > 0; i--)
            {
                listSegments[i].transform.position = listSegments[i - 1].transform.position;
            }
        }

        /// <summary>
        /// Check the position of the snake on the grid and try to change to the wanted direction
        /// </summary>
        private void TryChangeDirection()
        {
            if (_wantedDirection != currentDirection)
            {
                Vector3 pos = transform.position;
                bool nearGrid = Mathf.Abs(pos.x - Mathf.Round(pos.x)) < 0.4f && 
                               Mathf.Abs(pos.y - Mathf.Round(pos.y)) < 0.4f; // Check if the snake is close enough to the grid
                
                if (nearGrid)
                {
                    currentDirection = _wantedDirection;
                    if (_hasMoved) return;
                    GameEvents.OnFistMove.Invoke();
                    _hasMoved = true;
                }
            }
        }
        
        /// <summary>
        /// Growth the snake, add new segments
        /// </summary>
        public void Grow()
        {
            // Instanciate new snake segments
            for (int i = 0; i < 4; i++)
            {
                GameObject newSegment = Instantiate(snakeSegment);
                newSegment.GetComponent<SpriteRenderer>().material = _snakeMaterial;
                listSegments.Add(newSegment);
                // Si fait parti de mes 9 premiers segments instanciés => enlève obstable
                if (listSegments.Count < 9)
                    Destroy(newSegment.GetComponent<Obstacle>());
            }
        }

        /// <summary>
        /// Apply effect to hide the snake, change the aplha values
        /// </summary>
        public void ActivateGhost(float duration)
        {
            FadeMaterial.Instance.CallFadeMaterial(_snakeMaterial, duration);
        }
        
        /// <summary>
        /// Activate the invert controls
        /// </summary>
        public void Invert()
        {
            _invertControls = true;
        }

        /// <summary>
        /// Increase the speed of the snake
        /// </summary>
        public void Boost(float duration, float boostSpeed)
        {
            StartCoroutine(SpeedUp(duration, boostSpeed));
        }

        private IEnumerator SpeedUp(float duration, float boostSpeed)
        {
            float originalSpeed = speed;
            speed = boostSpeed;
            yield return new WaitForSecondsRealtime(duration);
            speed = originalSpeed;
        }
    }
}
