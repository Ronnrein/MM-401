using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Controls the health and damage of an object
    /// </summary>
    public class HealthController : MonoBehaviour {

        /// <summary>
        /// Health of this object
        /// </summary>
        public float StartingHealth = 5;

        /// <summary>
        /// If checked will destroy object rather than call the event
        /// </summary>
        public bool ObjectDestroy;

        /// <summary>
        /// Optional health bar object
        /// </summary>
        public Slider Bar;

        /// <summary>
        /// Explosion particle system
        /// </summary>
        public GameObject ExplosionPrefab;

        /// <summary>
        /// Delegate for object destroyed
        /// </summary>
        public delegate void ObjectDestroyed();

        /// <summary>
        /// Event for object destroyed
        /// </summary>
        public event ObjectDestroyed OnObjectDestroyed;

        /// <summary>
        /// Health property
        /// </summary>
        public float Health {
            get { return _health; }
            set {
                _health = value;
                if (Bar != null) {
                    Bar.value = _health / StartingHealth;
                }
            }
        }

        /// <summary>
        /// Health at start of game
        /// </summary>
        private float _health;

        /// <summary>
        /// Called at the beginning of the game
        /// </summary>
        public void Awake() {
            _health = StartingHealth;
        }

        /// <summary>
        /// Gets called when object is hit
        /// </summary>
        /// <param name="damage">Damage to be applied to this object</param>
        public void Hit(float damage) {
            Health -= damage;
            if (Health <= 0) {
                if (ObjectDestroy) {
                    Destroy(gameObject);
                }
                else {
                    if (OnObjectDestroyed != null) {
                        OnObjectDestroyed();
                    }
                }
                GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 5f);
            }
        }

        /// <summary>
        /// Reset health to starting health
        /// </summary>
        public void ResetHealth() {
            Health = StartingHealth;
        }
    }
}
