using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchUp : MonoBehaviour {
	private Vector3 airTargetPosition;
	private Vector3 groundTargetPosition;
	private float airStrikeHeight = 7.0f;
	private Rigidbody rb;


		// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

		airTargetPosition = new Vector3 (4.5f, 9.0f, 9f); // x position is not relevant since it's the players x velocity
		groundTargetPosition = new Vector3 (4.5f, 8f, 9f); // x position is not relevant since it's the players x velocity
	
    }
	
	void OnCollisionExit(Collision collision){


		Transform other = collision.gameObject.transform;
		//Debug.Log (other.tag);

		if (other.tag == "Player") {
			//Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
			if (other.transform.position.y < airStrikeHeight) {
				Vector3 assistedVelocity = (airTargetPosition - other.position).normalized * 10.0f;
				rb.velocity = assistedVelocity; // new Vector3(rb.velocity.normalized.x * 15.0f, assistedVelocity.y, assistedVelocity.z);
			} else {
				// Debug.Log ("Down Strike!");
				Vector3 assistedVelocity = (groundTargetPosition - other.position).normalized * 10.0f;
				rb.velocity = assistedVelocity; // new Vector3(rb.velocity.normalized.x * 20.0f, assistedVelocity.y, assistedVelocity.z);
			}
		}
	}

}
