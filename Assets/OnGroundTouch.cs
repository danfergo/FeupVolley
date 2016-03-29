using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnGroundTouch : MonoBehaviour {

	private Rigidbody rb;
	private GameController gameController;

	void Start(){
		rb = GetComponent<Rigidbody>();
		gameController = GetComponent<GameController> ();
	}

	void OnCollisionExit(Collision collision){
		Transform other = collision.gameObject.transform;

		if(other.name == "Ground")
		{
			if(rb.position.z > 9)
			{
				gameController.pointTo (0);
			}
			else
			{
				gameController.pointTo (1);

			}

		}

	}


}
