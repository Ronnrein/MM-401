using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Spawns 
    /// </summary>
    public class EnemySpawnerController : MonoBehaviour {

        /// <summary>
        /// The spawner to trigger
        /// </summary>
        private ObjectSpawner _spawner;

        /// <summary>
        /// Fires when game is started, after Awake
        /// </summary>
        public void Start() {
            _spawner = GetComponent<ObjectSpawner>();
        }

        /// <summary>
        /// Fires when collider enters trigger
        /// </summary>
        /// <param name="col">The collider</param>
        public void OnTriggerEnter(Collider col) {
            if (col.transform.GetComponent<ShipController>() != null) {
                _spawner.Spawn();
            }
        }
    }
}
