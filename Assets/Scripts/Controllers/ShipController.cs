using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public float animationSpeed = 1f;
    public Transform movementTarget;
	
	public void Update () {
        Vector3 target = new Vector3(
            movementTarget.position.x,
            movementTarget.position.y,
            transform.position.z
        );
	    transform.position = Vector3.Lerp(transform.position, target, animationSpeed * Time.deltaTime);
	}
}
