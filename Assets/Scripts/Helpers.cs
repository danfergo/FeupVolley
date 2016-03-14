using UnityEngine;
using System.Collections;

public class Helpers : MonoBehaviour {

	public Transform ballPositionHelper;
	public Transform ballFallPositionHelper;
	public Vector3 ballFallPosition;

	// Use this for initialization
	void Start () {
		ballPositionHelper.position = new Vector3 (transform.position.x, 0.0f, transform.position.z);
		ballFallPositionHelper.position = new Vector3 (ballPositionHelper.position.x, 0.0f, ballPositionHelper.position.z);
		ballFallPosition = ballFallPositionHelper.position;
	}
	
	// Update is called once per frame
	void Update(){

		// ball position helper
		ballPositionHelper.position = new Vector3(transform.position.x, 0.0f ,transform.position.z);
		float helperZoom = transform.position.y == 0 ? transform.localScale.x : transform.localScale.x / transform.position.y;
		ballPositionHelper.localScale = new Vector3 (helperZoom, 0.0f, helperZoom);


		//ball fall final position helper
		Vector3 bfP = predictBallFallPosition(this.transform.position, this.GetComponent<Rigidbody>().velocity, new Vector3(0, -10, 0));
		if (bfP.x > 0) {
			ballFallPosition = new Vector3 (bfP.x, 0.01f, bfP.z);
		}

		float step = 10.0f * Time.deltaTime;
		ballFallPositionHelper.position = Vector3.MoveTowards (ballFallPositionHelper.position, ballFallPosition, step);
	}



	Vector3 predictBallFallPosition(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity) {

		Vector3 last_pos = initialPosition;
		Vector3 velocity = initialVelocity;

		for (int i = 0; i < 300; i++) {
			velocity += gravity * Time.fixedDeltaTime;
			RaycastHit hit;
			if (Physics.Linecast(last_pos,  (last_pos + (velocity * Time.fixedDeltaTime)), out hit)) {
			//	Debug.Log ("Hit!");
				velocity = Vector3.Reflect (velocity * 0.9f, hit.normal);
				last_pos = hit.point;

				if (hit.collider.gameObject.transform.name == "Ground" || hit.collider.gameObject.transform.name=="Sphere") {
					return last_pos;
				}
			}

			last_pos += velocity * Time.fixedDeltaTime;
		}

		return new Vector3(-999.0f, -999.0f, -999.0f);
	}
}
