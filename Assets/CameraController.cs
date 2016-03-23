using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Camera thisCamera;
    public GameObject ball;
    public GameObject player1, player2;
    public GameObject net;
    public float speed;

    private float anguloFeito, originalFieldOfView;
    private bool initialMove = true, alreadyLooking = false, rotatePlayer, didCheer = false;
    private Vector3 Camera_initialPosition, Camera_initialRotation;
    private int playerOnFocus;


    // Use this for initialization
    void Start()
    {
        rotatePlayer = false;
        playerOnFocus = 1;
        anguloFeito = 0;
        Camera_initialPosition = thisCamera.transform.position;
        Camera_initialRotation = thisCamera.transform.eulerAngles;
        transform.LookAt(net.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (initialMove)
        {
            // Show Player 2 on camera
            if (anguloFeito > 90)
            {

                if (playerOnFocus == 1)
                {
                    playerCamera(player1);
                }
                else
                {
                    playerCamera(player2);
                }
            }
            else
            {
                transform.RotateAround(net.transform.position, Vector3.up, speed * Time.deltaTime);
                anguloFeito += speed * Time.deltaTime;
            }
        }
    }


    void playerCamera(GameObject player)
    {
        int z = 4, angle = 80;
        if (player2 == player)
        {
            z = 16;
            angle = -120;
        }
        
        // Change camera position
        transform.position = new Vector3(15, 5, z);
        transform.eulerAngles = new Vector3(10, -90, 0);

        // Make the player look directly to the camera
        if (!rotatePlayer)
        {
            Debug.Log("Rotate");
            player.transform.Rotate(new Vector3(0, angle, 0));
            rotatePlayer = true;
        }

        // Make the player start the animation
        if (!alreadyLooking)
        {
            player.GetComponent<Animator>().SetBool("Inicio", true);
            transform.LookAt(player.transform.position);
            alreadyLooking = true;
        }

        if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Cheer"))
        {
            didCheer = true;
        }


        if (didCheer && player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Grounded") && player.GetComponent<Animator>().GetBool("Inicio"))
        {

            if (player == player2)
            {
                // Initial animation is over
                initialMove = false;
            }

            playerOnFocus = 2;
            player.GetComponent<Animator>().SetBool("Inicio", false);
            didCheer = false;
            player.transform.Rotate(new Vector3(0, - angle, 0));
            rotatePlayer = false;
            alreadyLooking = false;
            // Avoid any reload.
        }
    }

}