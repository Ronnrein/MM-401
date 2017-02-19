using UnityEngine;

namespace Assets.Scripts.Controllers {
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
        /// Turrets of the ship
        /// </summary>
        public Transform[] Turrets;

        /// <summary>
        /// Laser ray prefab
        /// </summary>
        public GameObject LaserPrefab;

        /// <summary>
        /// Ship model containing animations
        /// </summary>
        public GameObject Model;

        /// <summary>
        /// Time of last shot
        /// </summary>
        private float _lastShot;

        /// <summary>
        /// Index of last turret shot
        /// </summary>
        private int _lastTurret;

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update() {
            UpdatePosition();

            // If fire button is pressed and appropriate amount of time has passed, shoot
            if (Input.GetButton("Fire1") && Time.time - _lastShot > ShotInterval) {
                Fire();
            }
        }

        /// <summary>
        /// Fires when collision is detected
        /// </summary>
        /// <param name="col">Object containing collision information</param>
        public void OnCollisionEnter(Collision col) {

            // If collision is with laser do damage, otherwize destroy
            if (col.transform.GetComponent<LaserController>() != null) {
                // TODO: Damage logic
                return;
            }
            Model.GetComponent<Animation>().Play("PlayerEnter");
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
        private void Fire() {

            // Cycle through turrets
            _lastTurret = _lastTurret + 1 >= Turrets.Length ? 0 : _lastTurret + 1;
            Transform turret = Turrets[_lastTurret];

            // Instantiate shot and ignore ship collision
            GameObject shot = Instantiate(LaserPrefab, turret.position, turret.rotation);
            Physics.IgnoreCollision(GetComponent<Collider>(), shot.GetComponent<Collider>());

            // Append MovementSpeed of ship to MovementSpeed of the shot
            shot.GetComponent<LaserController>().MovementSpeed += GameController.Instance.GameSpeed;

            // Update timer
            _lastShot = Time.time;
        }

    }
}
