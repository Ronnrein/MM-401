using UnityEngine;

public class LookAt : MonoBehaviour {

    public Transform target;

	public void Update () {
		transform.LookAt(target);
	}
}
