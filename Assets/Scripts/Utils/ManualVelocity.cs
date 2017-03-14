using UnityEngine;

namespace Assets.Scripts.Utils {

    /// <summary>
    /// Provides a manual velocity for non-rigidbody objects by using world position
    /// </summary>
    public class ManualVelocity : MonoBehaviour {

        /// <summary>
        /// Velocity of object based on world position
        /// </summary>
        public Vector3 Velocity {
            get { return _currentPosition == _previousPosition ? Vector3.zero : (_currentPosition - _previousPosition) / Time.deltaTime; }
        }

        /// <summary>
        /// Current position
        /// </summary>
        private Vector3 _currentPosition;

        /// <summary>
        /// Position previous update
        /// </summary>
        private Vector3 _previousPosition;

        /// <summary>
        /// Fires when game is started
        /// </summary>
        public void Awake () {
            _previousPosition = transform.position;
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            _previousPosition = _currentPosition;
            _currentPosition = transform.position;
        }
    }
}
