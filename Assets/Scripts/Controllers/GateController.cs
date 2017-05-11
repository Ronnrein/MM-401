using UnityEngine;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Controller for gate
    /// </summary>
    public class GateController : MonoBehaviour {

        /// <summary>
        /// The doors to trigger animation for
        /// </summary>
        public Animation[] Doors;

        /// <summary>
        /// Whether the animation has triggered yet
        /// </summary>
        private bool _triggered;

        /// <summary>
        /// Fires when collider enters trigger
        /// </summary>
        /// <param name="col">The collider</param>
        public void OnTriggerEnter(Collider col) {
            if (!_triggered && col.tag == "Player") {
                foreach (Animation door in Doors) {
                    door.Play("DoorOpen");
                }
            }
        }
    }
}
