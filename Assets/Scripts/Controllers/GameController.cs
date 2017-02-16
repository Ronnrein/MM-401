using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float gameSpeed = 50f;
    public float targetMovementSpeed = 1f;
    public float mouseMultiplier = 0.5f;
    public ShipController player;
    public Transform movementArea;
    public Transform movementTarget;

    public static GameController Instance { get; private set; }

    public void Awake() {
        Instance = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	public void Update () {
        transform.position += Vector3.forward * gameSpeed * Time.deltaTime;
        ContainTarget();
    }

    public void UpdateTarget() {
        Vector3 movement = GetInput();
        movementTarget.position += movement;
    }

    public void ContainTarget() {
        float xDiff = 0f;
        float yDiff = 0f;
        if (movementTarget.position.x > movementArea.position.x + movementArea.localScale.x / 2) {
            xDiff = (movementArea.position.x + movementArea.localScale.x / 2) - movementTarget.position.x;
        }
        else if (movementTarget.position.x < movementArea.position.x - movementArea.localScale.x / 2) {
            xDiff = Mathf.Abs(movementTarget.position.x - (movementArea.position.x - movementArea.localScale.x / 2));
        }
        if (movementTarget.position.y > movementArea.position.y + movementArea.localScale.y / 2) {
            yDiff = (movementArea.position.y + movementArea.localScale.y / 2) - movementTarget.position.y;
        }
        else if (movementTarget.position.y < movementArea.position.y - movementArea.localScale.y / 2) {
            yDiff = Mathf.Abs(movementTarget.position.y - (movementArea.position.y - movementArea.localScale.y / 2));
        }
        movementTarget.position = new Vector3(
            movementTarget.position.x + xDiff,
            movementTarget.position.y + yDiff,
            movementTarget.position.z
        );
    }

    public Vector3 GetInput() {
        return new Vector3(
            Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X") * mouseMultiplier,
            Input.GetAxis("Vertical") + Input.GetAxis("Mouse Y") * mouseMultiplier,
            0
        ) * targetMovementSpeed * Time.deltaTime;
    }
}
