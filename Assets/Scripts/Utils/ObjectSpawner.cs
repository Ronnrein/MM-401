using UnityEngine;

namespace Assets.Scripts.Utils {

    /// <summary>
    /// Spawns a prefab on a random point in a mesh
    /// </summary>
    public class ObjectSpawner : MonoBehaviour {

        /// <summary>
        /// Whether it spawns automatically
        /// </summary>
        public bool Active = true;

        /// <summary>
        /// Interval between each spawn
        /// </summary>
        public float Interval = 2f;

        /// <summary>
        /// Prefab to spawn
        /// </summary>
        public GameObject Prefab;

        /// <summary>
        /// Transform with mesh to use, if not set uses self
        /// </summary>
        public Transform MeshTransform;

        /// <summary>
        /// Time of last spawn
        /// </summary>
        private float _lastSpawn;

        /// <summary>
        /// Bounds of mesh
        /// </summary>
        private Bounds _bounds;

        /// <summary>
        /// Fires when game is started, after Awake
        /// </summary>
        public void Start() {
            if (MeshTransform == null) {
                MeshTransform = transform;
            }
            _bounds = MeshTransform.GetComponent<MeshFilter>().mesh.bounds;
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update() {
            if (Active && Time.time - Interval > _lastSpawn) {
                Spawn();
                _lastSpawn = Time.time;
            }
        }

        /// <summary>
        /// Spawn an object
        /// </summary>
        public void Spawn() {
            Vector3 position = new Vector3(
                Random.Range(-_bounds.extents.x, _bounds.extents.x),
                Random.Range(-_bounds.extents.y, _bounds.extents.y),
                Random.Range(-_bounds.extents.z, _bounds.extents.z)
            );
            GameObject o = Instantiate(Prefab, position, MeshTransform.rotation);
            o.transform.parent = MeshTransform;
            o.transform.localPosition = position;
        }
    }
}
