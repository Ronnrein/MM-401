using System.Collections;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Controls the functions of the player ship
    /// </summary>
    public class ShipController : MonoBehaviour {

        /// <summary>
        /// Horizontal and vertical movement MovementSpeed of ship
        /// </summary>
        public float MovementSpeed = 1f;

        /// <summary>
        /// Interval between each shot
        /// </summary>
        public float ShotInterval = 1f;

        /// <summary>
        /// Target for ship to move towards
        /// </summary>
        public Transform MovementTarget;

        /// <summary>
        /// Target to shoot towards
        /// </summary>
        public Transform TargetReticule;

        /// <summary>
        /// Turrets of the ship
        /// </summary>
        public Transform[] Turrets;

        /// <summary>
        /// Laser ray prefab
        /// </summary>
        public GameObject LaserPrefab;

        /// <summary>
        /// Audio for laser shot
        /// </summary>
        public AudioClip LaserAudio;

        /// <summary>
        /// Ship model containing animations
        /// </summary>
        public GameObject Model;

        /// <summary>
        /// Script that defines the movement of the ship
        /// </summary>
        public LinearMovement MovementScript;

        /// <summary>
        /// Velocity of ship
        /// </summary>
        [HideInInspector]
        public ManualVelocity ManualVelocity;

        /// <summary>
        /// Time of last shot
        /// </summary>
        private float _lastShot;

        /// <summary>
        /// Index of last turret shot
        /// </summary>
        private int _lastTurret;

        /// <summary>
        /// Audio source component
        /// </summary>
        private AudioSource _audioSource;

        /// <summary>
        /// Animation component of model
        /// </summary>
        private Animation _animation;

        /// <summary>
        /// Health controller of ship
        /// </summary>
        private HealthController _health;

        /// <summary>
        /// Whether or not the ship is currently animated
        /// </summary>
        public bool IsAnimated {
            get { return _animation.isPlaying; }
        }

        /// <summary>
        /// Fires when game is started, after Awake
        /// </summary>
        public void Start() {

            // Set the player to be this script
            GameController.Instance.Player = this;
            _animation = Model.GetComponent<Animation>();
            _audioSource = GetComponent<AudioSource>();
            ManualVelocity = GetComponent<ManualVelocity>();
            _health = GetComponent<HealthController>();
            _health.OnObjectDestroyed += Destroyed;
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update() {
            UpdatePosition();

            // If fire button is pressed and appropriate amount of time has passed, shoot
            if (!IsAnimated && Input.GetButton("Fire1") && Time.time - _lastShot > ShotInterval) {
                Shoot();
            }
        }

        /// <summary>
        /// Fires when collision is detected
        /// </summary>
        /// <param name="col">Object containing collision information</param>
        public void OnCollisionEnter(Collision col) {

            // If player is in animation or collides with shots, ignore, else kill
            if (IsAnimated) {
                return;
            }
            if (col.transform.GetComponent<LaserController>() != null) {
                _animation.Play("Hit");
                return;
            }
            _health.Kill();
        }

        /// <summary>
        /// Play ending animation
        /// </summary>
        public void PlayEndingAnimation() {
            _animation.Play("PlayerLeave");
            Helpers.CallAfter(GameController.EndLevel, _animation["PlayerLeave"].length);
        }

        /// <summary>
        /// Interpolates position of ship towards the target
        /// </summary>
        private void UpdatePosition() {
            Vector3 target = new Vector3(
                MovementTarget.position.x,
                MovementTarget.position.y,
                transform.position.z
            );
            transform.position = Vector3.Lerp(transform.position, target, MovementSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Fires a laser beam from one of the turrets
        /// </summary>
        private void Shoot() {

            // Cycle through turrets
            _lastTurret = _lastTurret + 1 >= Turrets.Length ? 0 : _lastTurret + 1;
            Transform turret = Turrets[_lastTurret];

            // Instantiate shot and ignore ship collision
            GameObject shot = Instantiate(LaserPrefab, turret.position, turret.rotation);
            Physics.IgnoreCollision(GetComponent<Collider>(), shot.GetComponent<Collider>());

            // Rotate shot towards target reticule and add speed of ship to shot
            shot.transform.LookAt(GetAimTarget());

            // Update timer
            _lastShot = Time.time;

            // Shoot sound
            _audioSource.PlayOneShot(LaserAudio);
        }

        /// <summary>
        /// Get aim target by checking the object that the reticule is over at the moment
        /// </summary>
        /// <returns>Position in reticule</returns>
        private Vector3 GetAimTarget() {
            Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(TargetReticule.position));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                return hit.point;
            }
            return TargetReticule.position;
        }

        /// <summary>
        /// Gets called when player ship is destroyed
        /// </summary>
        private void Destroyed() {
            GameController.Instance.Lives--;
            ResetShip();
        }

        /// <summary>
        /// Reset ship and health
        /// </summary>
        private void ResetShip() {
            _animation.Play("PlayerEnter");
            GameController.Instance.Movement.ResetTarget();
            GetComponent<HealthController>().ResetHealth();
            GetComponent<Collider>().enabled = false;
            Helpers.CallAfter(AnimationDone, _animation["PlayerEnter"].length);
        }

        /// <summary>
        /// Gets called when animation is done
        /// </summary>
        private void AnimationDone() {
            GetComponent<Collider>().enabled = true;
        }
    }
}
