using UnityEngine;

public class MapPieceController : MonoBehaviour {

    public float speed = 50f;
    public float destroyMargin = 50f;

    [HideInInspector]
    public GameObject predecessor;

    private bool spawnedNext;

	public void Update () {
	    float pos = transform.position.z - transform.localScale.x / 2;
	    float playerPos = GameController.Instance.player.transform.position.z - destroyMargin;
        if (!spawnedNext && pos < playerPos) {
	        SpawnNext();
	    }
	}

    public void SpawnNext() {
        spawnedNext = true;
        GameObject newPiece = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.localScale.x), transform.rotation);
        newPiece.GetComponent<MapPieceController>().predecessor = gameObject;
        newPiece.name = gameObject.name;
        Destroy(predecessor);
    }
}
