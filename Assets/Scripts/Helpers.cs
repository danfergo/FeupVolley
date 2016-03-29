using UnityEngine;
using System.Collections;

public class Helpers : MonoBehaviour {

	public Transform ballPositionHelper;
	public Transform ballFallPositionHelper;
	public Vector3 ballFallPosition;

	// Use this for initialization
	void Start () {
		ballPositionHelper.position = new Vector3 (transform.position.x, 0.1f, transform.position.z);
		ballFallPositionHelper.position = new Vector3 (ballPositionHelper.position.x, 0.1f, ballPositionHelper.position.z);
		ballFallPosition = ballFallPositionHelper.position;
	}
	
	// Update is called once per frame
	void Update(){

		// ball position helper
		ballPositionHelper.position = new Vector3(transform.position.x, 0.1f ,transform.position.z);
		float helperZoom = transform.position.y == 0 ? transform.localScale.x : transform.localScale.x / transform.position.y;
		ballPositionHelper.localScale = new Vector3 (helperZoom, 0.0f, helperZoom);


		//ball fall final position helper
		Vector3 bfP = predictBallFallPosition(this.transform.position, this.GetComponent<Rigidbody>().velocity, new Vector3(0, -10, 0));
		if (bfP.x > 0) {
			ballFallPosition = new Vector3 (bfP.x, 0.1f, bfP.z);
		}

		float step = 20.0f * Time.deltaTime;
		ballFallPositionHelper.position = Vector3.MoveTowards (ballFallPositionHelper.position, ballFallPosition, step);
	}



	Vector3 predictBallFallPosition(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity) {

		Vector3 last_pos = initialPosition;
		Vector3 velocity = initialVelocity;
		int hitCounter = 0;
		for (int i = 0; i < 300; i++) {
			velocity += gravity * Time.fixedDeltaTime;
			RaycastHit hit;
			//			Debug.Log ((float)gameObject.GetComponent<SphereCollider> ().radius);

			float maxDistance = (velocity * Time.fixedDeltaTime).magnitude;
			if (Physics.SphereCast(last_pos,0.5f, velocity * Time.fixedDeltaTime,out hit, maxDistance)) {

				//hitted = Physics.Linecast(last_pos + velocity * Time.fixedDeltaTime, sphereHit.point, out hit);
				//Debug.Log ("shpere hit: " + sphereHit.collider.gameObject.transform.name);


				//Debug.Log ("Hit! " +  sphereHit.point);
				//				Debug.Log ("Hitted: " + hitted);
				//Debug.Log ("Hitted: " + hit.collider.gameObject.transform.name);

				velocity = Vector3.Reflect (velocity , hit.normal);
				last_pos = hit.point;


				//if(hitted)
				if (hit.collider.gameObject.transform.name == "Ground" || hit.collider.gameObject.transform.name=="Sphere"
					|| hit.collider.gameObject.transform.tag == "Player") {
					return last_pos;
				}
				hitCounter++;
				if (hitCounter == 5) {
					break;
				}
			}

			last_pos += velocity * Time.fixedDeltaTime;
		}

		return new Vector3(-999.0f, -999.0f, -999.0f);
	}
}
