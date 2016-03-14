using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform player;


	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.position.x, 0.025f, player.position.z);
	}
}
