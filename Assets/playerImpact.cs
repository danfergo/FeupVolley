using UnityEngine;
using System.Collections;

public class playerImpact : MonoBehaviour {

    public static int playerCollision;
    public static bool playerWithNoDie;
    public static int activePowerUp;

    // Use this for initialization
    void Start () {
        playerCollision = 1;
    }
	
	// Update is called once per frame
	void Update () {

        if (activePowerUp == 1 && transform.position.x < 9)
        {
            if (transform.position.y < 4 && playerWithNoDie)
            {
                transform.position = new Vector3(transform.position.x, 4.1f, transform.position.z);
            }
        } else if (activePowerUp == 1 && transform.position.x > 9)
        {
            if (transform.position.y < 4 && playerWithNoDie)
            {
                transform.position = new Vector3(transform.position.x, 4.1f, transform.position.z);
            }
        }
	}


    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            if (obj.gameObject.name == "ThirdPersonController1")
                playerCollision = 1;
            else playerCollision = 2;
        }
    }
}
