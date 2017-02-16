using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float movementSpeed = 1f;
    public ShipController player;
    public Transform movementArea;
    public Transform movementTarget;

    public static ShipController Player { get; private set; }

    public void Awake() {
        Player = player;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	public void Update () {
	    Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X"),
            Input.GetAxis("Vertical") + Input.GetAxis("Mouse Y"),
            0
        ) * movementSpeed * Time.deltaTime;
	    Vector3 oldPosition = movementTarget.position;
        movementTarget.position += movement;
	    if (movementTarget.position.x > movementArea.position.x + movementArea.localScale.x / 2 ||
            movementTarget.position.x < movementArea.position.x - movementArea.localScale.x / 2) {
	        movementTarget.position = new Vector3(
                oldPosition.x,
                movementTarget.position.y,
                movementTarget.position.z
            );
	    }
        if (movementTarget.position.y > movementArea.position.y + movementArea.localScale.y / 2 ||
            movementTarget.position.y < movementArea.position.y - movementArea.localScale.y / 2) {
            movementTarget.position = new Vector3(
                movementTarget.position.x,
                oldPosition.y,
                movementTarget.position.z
            );
        }
    }
}
