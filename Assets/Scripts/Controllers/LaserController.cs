using UnityEngine;

public class LaserController : MonoBehaviour {

    public float speed = 100f;
    public float destroyTime = 5f;

	public void Awake () {
		Destroy(gameObject, destroyTime);
	}
	
	public void Update () {
	    transform.position += transform.forward * speed * Time.deltaTime;
	}
}
