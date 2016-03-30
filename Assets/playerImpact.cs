using UnityEngine;
using System.Collections;

public class playerImpact : MonoBehaviour {

    public static int playerCollision;
    public static int playerWithNoDie;
    public static bool activePowerUp;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
        if (playerWithNoDie == 1 && transform.position.z < 9)
        {
            if (transform.position.y < 4 && activePowerUp)
            {
                transform.position = new Vector3(transform.position.x, 4.1f, transform.position.z);
            }
        } else if (playerWithNoDie == 2 && transform.position.z > 9)
        {
            if (transform.position.y < 4 && activePowerUp)
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
