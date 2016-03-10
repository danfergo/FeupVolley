using UnityEngine;
using System.Collections;

public class CameraTarget : MonoBehaviour {


    public GameObject target;
    public GameObject ball;

    public float speed = 10f;
    float angle = 0f;
	// Use this for initialization
	void Start () {
        transform.LookAt(target.transform.position);
    }
	
	// Update is called once per frame
	void Update () {

        if (ball.transform.position.z > target.transform.position.z)
        {
            if (transform.position.z >= 15)
            {
                angle = 0;
            }
            else
            {
                angle = -speed;
            }
        }
        else {
            if (transform.position.z <= 5)
            {
                angle = 0;
            }
            else
            {
                angle = speed;
            }
        }

        transform.RotateAround(target.transform.position, new Vector3(0,1f,0), angle / 2);

    }
}
