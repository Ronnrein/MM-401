using UnityEngine;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Moves the target the ship moves towards within a quad based on user input
    /// </summary>
    public class MovementController : MonoBehaviour {
        
        /// <summary>
        /// Target the ship will follow
        /// </summary>
        public Transform MovementTarget;

        /// <summary>
        /// Multiplier to adjust mouse movement
        /// </summary>
        public float MouseMultiplier = 0.5f;

        /// <summary>
        /// Movement speed of the input-target
        /// </summary>
        public float TargetMovementSpeed = 1f;

        /// <summary>
        /// Original starting position of movement target
        /// </summary>
        private Vector3 _originalLocalTargetPosition;

        /// <summary>
        /// Fires when game is started, after Awake
        /// </summary>
        public void Start() {

            // Set movement controller to be this script
            GameController.Instance.Movement = this;
            _originalLocalTargetPosition = MovementTarget.localPosition;
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update() {
            if (!GameController.Instance.Player.IsAnimated) {
                UpdateTarget();
                ContainTarget();
            }
        }

        /// <summary>
        /// Resets movement target to starting position
        /// </summary>
        public void ResetTarget() {
            MovementTarget.localPosition = _originalLocalTargetPosition;
        }

        /// <summary>
        /// Forces the target to stay within the boundaries of the movement area quad
        /// </summary>
        private void ContainTarget() {
            float xDiff = 0f;
            float yDiff = 0f;
            if (MovementTarget.position.x > transform.position.x + transform.localScale.x / 2) {
                xDiff = (transform.position.x + transform.localScale.x / 2) - MovementTarget.position.x;
            }
            else if (MovementTarget.position.x < transform.position.x - transform.localScale.x / 2) {
                xDiff = Mathf.Abs(MovementTarget.position.x - (transform.position.x - transform.localScale.x / 2));
            }
            if (MovementTarget.position.y > transform.position.y + transform.localScale.y / 2) {
                yDiff = (transform.position.y + transform.localScale.y / 2) - MovementTarget.position.y;
            }
            else if (MovementTarget.position.y < transform.position.y - transform.localScale.y / 2) {
                yDiff = Mathf.Abs(MovementTarget.position.y - (transform.position.y - transform.localScale.y / 2));
            }
            MovementTarget.position = new Vector3(
                MovementTarget.position.x + xDiff,
                MovementTarget.position.y + yDiff,
                MovementTarget.position.z
            );
        }

        /// <summary>
        /// Update position of movement target
        /// </summary>
        private void UpdateTarget() {
            Vector3 movement = GetInput() * TargetMovementSpeed;
            MovementTarget.position += movement;
        }

        /// <summary>
        /// Get input from directional and mouse axis
        /// </summary>
        /// <returns>Normalized input</returns>
        private Vector3 GetInput() {
            return new Vector3(
                Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X") * MouseMultiplier,
                Input.GetAxis("Vertical") + Input.GetAxis("Mouse Y") * MouseMultiplier,
                0
            );
        }
    }
}
