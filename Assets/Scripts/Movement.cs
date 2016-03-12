using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    private Rigidbody rb;
    public float speed; 
	public Transform playerPositionHelper;
	private Vector3 airTargetPosition;
	private Vector3 groundTargetPosition;
	private float airStrikeHeight = 6.0f;

    // Use this for initialization
    void Start()
    {
        Input.GetAxis("Horizontal2");
        Input.GetAxis("Vertical2");

        rb = GetComponent<Rigidbody>();

		airTargetPosition = new Vector3 (0.0f, 12.0f, 9f); // x position is not relevant since it's the players x velocity
		groundTargetPosition = new Vector3 (0.0f, 8.0f, 9f); // x position is not relevant since it's the players x velocity
	
    }


	void Update(){
		playerPositionHelper.position = new Vector3 (transform.position.x, 0.2f, transform.position.z);
	}

    // Update is called once per frame
    void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal2");
        float v = Input.GetAxis("Vertical2");
		rb.velocity =  new Vector3(-v * speed, rb.velocity.y, h* speed );

		if(Input.GetButtonDown("Jump2") ){
			rb.velocity = new Vector3( rb.velocity.x, 7f, rb.velocity.z);
		}
    }


	void OnCollisionExit(Collision collision){
		Transform other = collision.gameObject.transform;

		if (other.name == "Ball") {
			Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody> ();
			if (rb.position.y < airStrikeHeight) {
				Vector3 assistedVelocity = (airTargetPosition - other.position).normalized * 15.0f;
				otherRb.velocity = new Vector3 (rb.velocity.x, assistedVelocity.y, assistedVelocity.z);
			} else {
				Vector3 assistedVelocity = (groundTargetPosition - other.position).normalized * 30.0f;
				otherRb.velocity = new Vector3 (rb.velocity.x, assistedVelocity.y, assistedVelocity.z);
			}
			//float zPPosition = other.position.z / 8.0f;

			// Debug.Log ("Assist: " + new Vector3(0.0f, 9.0f, assistZ/zPPosition));

			//otherRb.velocity  = new Vector3(otherRb.velocity.x, zPPosition*assistY, assistZ - assistZ*zPPosition);
		}
	
	}


}
