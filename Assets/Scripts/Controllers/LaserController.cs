using UnityEngine;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Controls laser shots
    /// </summary>
    public class LaserController : MonoBehaviour {

        /// <summary>
        /// Speed at which laser moves at
        /// </summary>
        public float MovementSpeed = 100f;

        /// <summary>
        /// Maximum time until shots and effects are cleaned up
        /// </summary>
        public float TimeToLive = 5f;

        /// <summary>
        /// Damage caused by this laser
        /// </summary>
        public float Damage = 1f;

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
        public void Update() {

            // Move shot forward
            transform.position += transform.forward * MovementSpeed * Time.deltaTime;
        }

        /// <summary>
        /// Fires when collision is detected
        /// </summary>
        /// <param name="col">Object containing collision information</param>
        public void OnCollisionEnter(Collision col) {

            // If collision is detected, fire off effect and destroy self
            Destroy(Instantiate(HitEffect, transform.position, Quaternion.identity), TimeToLive);
            Destroy(gameObject);

            // If collider has health, apply damage
            if (col.transform.GetComponent<HealthController>() != null) {
                col.gameObject.SendMessage("Hit", Damage);
            }
        }
    }
}
