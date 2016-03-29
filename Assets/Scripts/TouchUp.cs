using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchUp : MonoBehaviour {
	private Vector3 airTargetPosition;
	private Vector3 groundTargetPosition;
	private float airStrikeHeight = 7.0f;
	private Rigidbody rb;

    //related to UI updates
    public Text player1Score;
    public Text player2Score;
    private int player1Counter = 0;
    private int player2Counter = 0;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

		airTargetPosition = new Vector3 (4.5f, 9.0f, 9f); // x position is not relevant since it's the players x velocity
		groundTargetPosition = new Vector3 (4.5f, 8f, 9f); // x position is not relevant since it's the players x velocity

        player1Score.text = player1Counter.ToString();
        player2Score.text = player2Counter.ToString();
    }
	
	void OnCollisionExit(Collision collision){


		Transform other = collision.gameObject.transform;
        //Debug.Log (other.tag);

        if (other.tag == "Player")
        {
			//Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
			if (other.transform.position.y < airStrikeHeight)
			{
				Debug.Log (airTargetPosition);

				Vector3 assistedVelocity = (airTargetPosition - other.position).normalized * 10.0f;
				rb.velocity = assistedVelocity; // new Vector3(rb.velocity.normalized.x * 15.0f, assistedVelocity.y, assistedVelocity.z);
			}
			else {
				// Debug.Log ("Down Strike!");
				Vector3 assistedVelocity = (groundTargetPosition - other.position).normalized * 10.0f;
				rb.velocity = assistedVelocity; // new Vector3(rb.velocity.normalized.x * 20.0f, assistedVelocity.y, assistedVelocity.z);
			}

            //Vector3 assistedVelocity = (groundTargetPosition - other.position) * 
            //				(	1f + 
            //					(other.transform.position.y/12f)*1f
            //				);
            //}
            //float zPPosition = other.position.z / 8.0f;

            // Debug.Log ("Assist: " + new Vector3(0.0f, 9.0f, assistZ/zPPosition));

            //otherRb.velocity  = new Vector3(otherRb.velocity.x, zPPosition*assistY, assistZ - assistZ*zPPosition);
        }
        else if(other.name == "Ground")
        {
            if(rb.position.z > 9)
            {
                //increase score for player 1
                player1Counter++;
            }
            else
            {
                //increase score for player 2
                player2Counter++;
            }

            player1Score.text = player1Counter.ToString();
            player2Score.text = player2Counter.ToString();
        }
	}
}
