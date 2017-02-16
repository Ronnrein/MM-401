using UnityEngine;

public class ShipController : MonoBehaviour {

    public float animationSpeed = 1f;
    public float shotInterval = 1f;
    public Transform movementTarget;
    public Transform[] turrets;
    public GameObject laserPrefab;

    private float lastShot;
    private int lastTurret;
	
	public void Update () {
        UpdatePosition();
	    if (Input.GetButton("Fire1") && Time.time - lastShot > shotInterval) {
	        Fire();
	    }
	}

    private void UpdatePosition() {
        Vector3 target = new Vector3(
            movementTarget.position.x,
            movementTarget.position.y,
            transform.position.z
        );
        transform.position = Vector3.Lerp(transform.position, target, animationSpeed * Time.deltaTime);
    }

    private void Fire() {
        lastTurret = lastTurret + 1 >= turrets.Length ? 0 : lastTurret + 1;
        Transform turret = turrets[lastTurret];
        GameObject shot = Instantiate(laserPrefab, turret.position, turret.rotation);
        shot.GetComponent<LaserController>().speed += GameController.Instance.gameSpeed;
        lastShot = Time.time;
    }
}
