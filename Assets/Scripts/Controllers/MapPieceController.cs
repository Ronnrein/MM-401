using UnityEngine;

namespace Assets.Scripts.Controllers {
    public class MapPieceController : MonoBehaviour {

        /// <summary>
        /// The smount of space the edge must be behind ship to replace piece
        /// </summary>
        public float DestroyMargin = 50f;

        /// <summary>
        /// The previous piece
        /// </summary>
        [HideInInspector]
        public GameObject Predecessor;

        /// <summary>
        /// Whether or not next piece has been spawned
        /// </summary>
        private bool _hasSpawnedNext;

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {

            // Check if piece mas moved far enough to spawn next piece
            float pos = transform.position.z - transform.localScale.x / 2;
            float playerPos = GameController.Instance.Player.transform.position.z - DestroyMargin;
            if (!_hasSpawnedNext && pos < playerPos) {
                SpawnNext();
            }
        }

        /// <summary>
        /// Spawns next piece of the map
        /// </summary>
        public void SpawnNext() {
            _hasSpawnedNext = true;
            GameObject newPiece = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.localScale.x), transform.rotation);
            newPiece.GetComponent<MapPieceController>().Predecessor = gameObject;
            newPiece.name = gameObject.name;
            Destroy(Predecessor);
        }
    }
}
