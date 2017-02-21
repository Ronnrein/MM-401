using UnityEngine;

namespace Assets.Scripts.Utils {

    /// <summary>
    /// Applies linear movement to object
    /// </summary>
    public class LinearMovement : MonoBehaviour {

        /// <summary>
        /// Speed of movement in each direction
        /// </summary>
        public Vector3 Speed;

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            transform.position += Speed * Time.deltaTime;
        }
    }
}
