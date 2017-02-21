using UnityEngine;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Controls the health and damage of an object
    /// </summary>
    public class HealthController : MonoBehaviour {

        /// <summary>
        /// Health of this object
        /// </summary>
        public float Health = 5;

        /// <summary>
        /// Gets called when object is hit
        /// </summary>
        /// <param name="damage">Damage to be applied to this object</param>
        public void Hit(float damage) {
            Health -= damage;
            if (Health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
