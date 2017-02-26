using UnityEngine;

namespace Assets.Scripts.Utils {

    /// <summary>
    /// Lerps transform towards a target
    /// </summary>
    public class LerpTowards : MonoBehaviour {

        /// <summary>
        /// Target transform (Overrides TargetPosition of set)
        /// </summary>
        public Transform Target;

        /// <summary>
        /// Target vector
        /// </summary>
        public Vector3 TargetPosition;

        /// <summary>
        /// Duration of interpolation
        /// </summary>
        public float Duration = 1.0f;

        /// <summary>
        /// Whether or not to smooth the transition
        /// </summary>
        public bool Smooth;

        /// <summary>
        /// The starting position of the transform
        /// </summary>
        private Vector3 _startPosition;

        /// <summary>
        /// The current state of the interpolation
        /// </summary>
        private float _t;

        /// <summary>
        /// Fires when game is started, after Awake
        /// </summary>
        public void Start () {
            if (Target != null) {
                TargetPosition = Target.position;
            }
            _startPosition = transform.position;
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            if (_t < 1) {
                float step = Smooth ? Mathf.SmoothStep(0.0f, 1.0f, _t) : _t;
                transform.position = Vector3.Lerp(_startPosition, TargetPosition, step);
                _t += Time.deltaTime / Duration;
            }
        }

        /// <summary>
        /// Creates a component of this type on the provided object and initiates an interpolation
        /// </summary>
        /// <param name="obj">Object to interpolate</param>
        /// <param name="target">Target to interpolate towards</param>
        /// <param name="duration">Duration of interpolation</param>
        /// <param name="smooth">Whether or not to smooth transition</param>
        /// <returns>The component that was added</returns>
        public static LerpTowards LerpObject(GameObject obj, Vector3 target, float duration, bool smooth = false) {
            LerpTowards lerp = obj.AddComponent<LerpTowards>();
            lerp.TargetPosition = target;
            lerp.Smooth = smooth;
            lerp.Duration = duration;
            return lerp;
        }
    }
}
