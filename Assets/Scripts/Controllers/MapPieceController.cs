using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPieceController : MonoBehaviour {

    public float speed = 50f;
    public float destroyMargin = 50f;

    [HideInInspector]
    public GameObject predecessor;

    private bool spawnedNext;

	public void Update () {
		transform.Translate(speed * Time.deltaTime, 0, 0);
	    if (!spawnedNext && transform.position.z - transform.localScale.x / 2 < GameController.Player.transform.position.z - destroyMargin) {
	        spawnedNext = true;
	        GameObject newPiece = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.localScale.x), transform.rotation);
	        newPiece.GetComponent<MapPieceController>().predecessor = gameObject;
	        newPiece.name = gameObject.name;
            Destroy(predecessor);
	    }
	}
}
