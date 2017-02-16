using UnityEngine;

public class LaserController : MonoBehaviour {

    public float speed = 100f;
    public float destroyTime = 5f;
    public GameObject hitEffect;

	public void Awake () {
		Destroy(gameObject, destroyTime);
        Physics.IgnoreCollision(GetComponent<Collider>(), GameController.Instance.player.GetComponent<Collider>());
	}
	
	public void Update () {
	    transform.position += transform.forward * speed * Time.deltaTime;
	}

    public void OnCollisionEnter(Collision col) {
        Destroy(Instantiate(hitEffect, transform.position, Quaternion.identity), 5f);
        Destroy(gameObject);
    }
}
