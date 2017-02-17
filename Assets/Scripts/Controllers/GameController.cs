using UnityEngine;

namespace Assets.Scripts.Controllers {
    public class GameController : MonoBehaviour {

        /// <summary>
        /// Movement speed of the game
        /// </summary>
        public float GameSpeed = 50f;

        /// <summary>
        /// Movement speed of the input-target
        /// </summary>
        public float TargetMovementSpeed = 1f;

        /// <summary>
        /// Multiplier for mouse movement
        /// </summary>
        public float MouseMultiplier = 0.5f;

        /// <summary>
        /// The ship
        /// </summary>
        public ShipController Player;

        /// <summary>
        /// The quad representing the allowed movement area
        /// </summary>
        public Transform MovementArea;

        /// <summary>
        /// The input-target on the movement area
        /// </summary>
        public Transform MovementTarget;

        /// <summary>
        /// Static instance of this class
        /// </summary>
        public static GameController Instance { get; private set; }

        /// <summary>
        /// Fires when game is started
        /// </summary>
        public void Awake() {

            // Create a static instance of this class and lock cursor
            Instance = this;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
        
            // Move game forwards
            transform.position += Vector3.forward * GameSpeed * Time.deltaTime;
            UpdateTarget();
            ContainTarget();
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateTarget() {
            Vector3 movement = GetInput() * TargetMovementSpeed;
            MovementTarget.position += movement;
        }

        /// <summary>
        /// Forces the target to stay within the boundaries of the movement area quad
        /// </summary>
        public void ContainTarget() {
            float xDiff = 0f;
            float yDiff = 0f;
            if (MovementTarget.position.x > MovementArea.position.x + MovementArea.localScale.x / 2) {
                xDiff = (MovementArea.position.x + MovementArea.localScale.x / 2) - MovementTarget.position.x;
            }
            else if (MovementTarget.position.x < MovementArea.position.x - MovementArea.localScale.x / 2) {
                xDiff = Mathf.Abs(MovementTarget.position.x - (MovementArea.position.x - MovementArea.localScale.x / 2));
            }
            if (MovementTarget.position.y > MovementArea.position.y + MovementArea.localScale.y / 2) {
                yDiff = (MovementArea.position.y + MovementArea.localScale.y / 2) - MovementTarget.position.y;
            }
            else if (MovementTarget.position.y < MovementArea.position.y - MovementArea.localScale.y / 2) {
                yDiff = Mathf.Abs(MovementTarget.position.y - (MovementArea.position.y - MovementArea.localScale.y / 2));
            }
            MovementTarget.position = new Vector3(
                MovementTarget.position.x + xDiff,
                MovementTarget.position.y + yDiff,
                MovementTarget.position.z
            );
        }

        /// <summary>
        /// Get input from directional and mouse axis
        /// </summary>
        /// <returns>Normalized input</returns>
        public Vector3 GetInput() {
            return new Vector3(
                Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X") * MouseMultiplier,
                Input.GetAxis("Vertical") + Input.GetAxis("Mouse Y") * MouseMultiplier,
                0
            );
        }
    }
}
