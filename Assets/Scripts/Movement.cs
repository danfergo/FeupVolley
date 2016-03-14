using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    private Rigidbody rb;
    public float speed; 
	public Transform playerPositionHelper;


    // Use this for initialization
    void Start()
    {
        Input.GetAxis("Horizontal2");
        Input.GetAxis("Vertical2");

        rb = GetComponent<Rigidbody>();

    }


	void Update(){
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





}
