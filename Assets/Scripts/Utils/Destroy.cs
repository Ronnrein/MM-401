using UnityEngine;

namespace Assets.Scripts.Utils {

    /// <summary>
    /// Utility to destroy object after a set amount of time
    /// </summary>
    public class Destroy : MonoBehaviour {

        /// <summary>
        /// Delay in seconds before object is destroyed
        /// </summary>
        public float Delay = 0f;

        /// <summary>
        /// Fires when game is started
        /// </summary>
        public void Awake () {
		    Destroy(gameObject, Delay);
        }
	
    }
}
