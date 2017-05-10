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
        /// Whether speed is local to object or not
        /// </summary>
        public bool Local = false;

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            if (Local) {
                transform.position += transform.forward * Speed.z * Time.deltaTime;
                transform.position += transform.right * Speed.x * Time.deltaTime;
                transform.position += transform.up * Speed.y * Time.deltaTime;
                return;
            }
            transform.position += Speed * Time.deltaTime;
        }
    }
}
