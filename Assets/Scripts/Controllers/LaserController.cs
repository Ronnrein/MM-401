using UnityEngine;

namespace Assets.Scripts.Controllers {
    public class LaserController : MonoBehaviour {

        /// <summary>
        /// Speed at which laser moves at
        /// </summary>
        public float MovementSpeed = 100f;

        /// <summary>
        /// Maximum time until destroyed
        /// </summary>
        public float TimeToLive = 5f;

        /// <summary>
        /// Effect on collision
        /// </summary>
        public GameObject HitEffect;

        /// <summary>
        /// Fires when game is started, after Awake
        /// </summary>
        public void Start() {

            // If still alive in after this amount of seconds, destroy
            Destroy(gameObject, TimeToLive);

        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {

            // Move shot forward
            transform.position += transform.forward * MovementSpeed * Time.deltaTime;
        }

        /// <summary>
        /// Fires when collision is detected
        /// </summary>
        /// <param name="col"></param>
        public void OnCollisionEnter(Collision col) {

            // If collision is detected, fire off effect and destroy self
            Destroy(Instantiate(HitEffect, transform.position, Quaternion.identity), 5f);
            Destroy(gameObject);
        }
    }
}
