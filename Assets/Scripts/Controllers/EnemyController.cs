using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Controller for enemy
    /// </summary>
    public class EnemyController : MonoBehaviour {

        /// <summary>
        /// Time before enemy flies away
        /// </summary>
        public float LifeTime = 5f;

        /// <summary>
        /// Time enemy started living
        /// </summary>
        private float _startTime;

        /// <summary>
        /// Model animation object
        /// </summary>
        private Animation animation;

        /// <summary>
        /// Fires when game is started, after Awake
        /// </summary>
        public void Start () {
            _startTime = Time.time;
            animation = GetComponentInChildren<Animation>();
            GetComponent<Collider>().enabled = false;
            Helpers.CallAfter(IntroFinished, animation["EnemyEnter"].length);
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            if (Time.time - _startTime > LifeTime) {
                animation.Play("EnemyLeave");
                Destroy(gameObject, animation["EnemyLeave"].length);
                GetComponent<Collider>().enabled = false;
            }
        }

        public void IntroFinished() {
            GetComponent<Collider>().enabled = true;
        }
    }
}
