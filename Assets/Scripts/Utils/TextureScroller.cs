using UnityEngine;

namespace Assets.Scripts.Utils {

    /// <summary>
    /// Scrolls texture of attached object
    /// </summary>
    public class TextureScroller : MonoBehaviour {

        /// <summary>
        /// Speed to scroll at
        /// </summary>
        public Vector2 Speed;

        /// <summary>
        /// Mesh to scroll
        /// </summary>
        private Mesh _mesh;

        /// <summary>
        /// Fires when game is started
        /// </summary>
        public void Awake () {
            _mesh = GetComponent<MeshFilter>().mesh;
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            Vector2[] uv = _mesh.uv;
            for (var i = 0; i < uv.Length; i++) {
                uv[i] += new Vector2(Speed.x * Time.deltaTime, Speed.y * Time.deltaTime);
            }
            _mesh.uv = uv;
        }
    }
}
