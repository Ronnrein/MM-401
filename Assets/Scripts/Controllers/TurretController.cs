using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Stationary turret
    /// </summary>
    public class TurretController : MonoBehaviour {

        /// <summary>
        /// Interval between each shot
        /// </summary>
        public float ShotInterval = 5f;

        /// <summary>
        /// Max range of turret
        /// </summary>
        public float MaxRange = 1000f;

        /// <summary>
        /// The target to aim for, will be updated by script
        /// </summary>
        public Transform Target;

        /// <summary>
        /// The point to spawn projectiles
        /// </summary>
        public Transform Spawner;

        /// <summary>
        /// Projectile prefab
        /// </summary>
        public GameObject ProjectilePrefab;

        /// <summary>
        /// Sound of shot
        /// </summary>
        public AudioClip ProjectileAudio;

        /// <summary>
        /// Speed of projectile
        /// </summary>
        private float _projectileSpeed;

        /// <summary>
        /// Player object
        /// </summary>
        private ShipController _player;

        /// <summary>
        /// Time of last shot
        /// </summary>
        private float _lastShot;

        /// <summary>
        /// Audiosource component
        /// </summary>
        private AudioSource _audioSource;

        /// <summary>
        /// Fires when game is started, after Awake
        /// </summary>
        public void Start () {
            GameObject tempShot = Instantiate(ProjectilePrefab, Vector3.zero, Quaternion.identity);
            _projectileSpeed = tempShot.GetComponent<LaserController>().MovementSpeed;
            Destroy(tempShot);
            _player = GameController.Instance.Player;
            _lastShot = Time.time;
            _audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            if (_player.transform.position.z > transform.position.z) {
                return;
            }
            UpdateTarget();
            if (Time.time - _lastShot > ShotInterval && Vector3.Distance(transform.position, _player.transform.position) < MaxRange) {
                Shoot();
            }
        }

        /// <summary>
        /// Update target transform based on interception point
        /// </summary>
        private void UpdateTarget() {
            Vector3 interceptionPoint = Helpers.InterceptionPoint(
                transform.position,
                Vector3.zero,
                _player.transform.position,
                _player.ManualVelocity.Velocity,
                _projectileSpeed
            );
            if (interceptionPoint != transform.position) {
                interceptionPoint = new Vector3(
                    interceptionPoint.x,
                    interceptionPoint.y,
                    interceptionPoint.z
                );
                Target.position = interceptionPoint;
            }
        }

        /// <summary>
        /// Shot a projectile
        /// </summary>
        private void Shoot() {
            _lastShot = Time.time;
            Instantiate(ProjectilePrefab, Spawner.position, Spawner.rotation);
            _audioSource.PlayOneShot(ProjectileAudio);
        }
    }
}
