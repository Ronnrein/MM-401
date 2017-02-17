using UnityEngine;

namespace Assets.Scripts.Utils {
    public class LookAt : MonoBehaviour {

        /// <summary>
        /// Target to look at
        /// </summary>
        public Transform Target;

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            transform.LookAt(Target);
        }
    }
}
