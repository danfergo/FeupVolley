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
    private int playerWithTheboo;


    // Use this for initialization
    void Start()
    {
        rotatePlayer = false;
        playerOnFocus = 1;
        anguloFeito = 0;
        Camera_initialPosition = thisCamera.transform.position;
        Camera_initialRotation = thisCamera.transform.eulerAngles;
        transform.LookAt(net.transform.position);

        playerWithTheboo = Random.Range(1, 3);

        GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().useGravity = false;
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
                    if (playerWithTheboo == 1)
                        playBooSound();
                    else playCheerSound();

                    playerCamera(player1);

                }
                else
                {
                    if (playerWithTheboo == 1)
                        playCheerSound();
                    else playBooSound();

                    playerCamera(player2);
                }
            }
            else
            {
                transform.RotateAround(net.transform.position, Vector3.up, speed * Time.deltaTime);
                anguloFeito += speed * Time.deltaTime;
            }
		} else {
			transform.position = Camera_initialPosition;
			transform.eulerAngles = Camera_initialRotation;
			transform.LookAt(net.transform.position);

            GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().useGravity = true;
		}
    }

    void playerCamera(GameObject player)
    {
        float z = 2.6f, angle = 80;
        if (player2 == player)
        {
            z = 16;
            angle = -120;
        }
        
        // Change camera position
        transform.position = new Vector3(37, 5, z);
        transform.eulerAngles = new Vector3(10, -90, 0);

        // Make the player look directly to the camera
        if (!rotatePlayer)
        {
			player.transform.LookAt (thisCamera.transform.position);
            //player.transform.Rotate(new Vector3(0, angle, 0));
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

    // Play boo sound
    void playBooSound()
    {

        AudioSource[] temp = GameObject.FindGameObjectWithTag("Boo_Sound").GetComponents<AudioSource>();
        AudioSource[] temp_1 = GameObject.FindGameObjectWithTag("Cheer_Sound").GetComponents<AudioSource>();
        AudioSource[] temp_2 = GameObject.FindGameObjectWithTag("General_Sound").GetComponents<AudioSource>();

        // Play boo sound
        for (var i = 0; i < temp.Length; i++)
        {
            if (!temp[i].isPlaying)
            {
                temp[i].volume = 0.3f;
                temp[i].Play();
            }
        }

        // Stop Cheer sound if playing
        for (var i = 0; i < temp_1.Length; i++)
        {
            if (temp_1[i].isPlaying)
            {
                temp_1[i].volume = 0.0f;
                temp_1[i].Stop();
            }
        }

        // Reduce the ambient sound
        for (var i = 0; i < temp_2.Length; i++)
        {
            if (temp_2[i].isPlaying)
            {
                temp_2[i].volume = 0.1f;
            }
        }
    }

    // Play Cheer sound
    void playCheerSound()
    {
        AudioSource[] temp = GameObject.FindGameObjectWithTag("Boo_Sound").GetComponents<AudioSource>();
        AudioSource[] temp_1 = GameObject.FindGameObjectWithTag("Cheer_Sound").GetComponents<AudioSource>();
        AudioSource[] temp_2 = GameObject.FindGameObjectWithTag("General_Sound").GetComponents<AudioSource>();

        // Stop boo sound
        for (var i = 0; i < temp.Length; i++)
        {
            if (temp[i].isPlaying)
            {
                temp[i].volume = 0.0f;
                temp[i].Stop();
            }
        }

        // Play Cheer sound if playing
        for (var i = 0; i < temp_1.Length; i++)
        {
            if (!temp_1[i].isPlaying)
            {
                temp_1[i].volume = 0.4f;
                temp_1[i].Play();
            }
        }

        // Reduce the ambient sound
        for (var i = 0; i < temp_2.Length; i++)
        {
            if (temp_2[i].isPlaying)
            {
                temp_2[i].volume = 0.1f;
            }
        }
    }
}