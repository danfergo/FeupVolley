using UnityEngine;
using System.Collections;

public class powerUpController : MonoBehaviour
{

    public GameObject player1, player2;

    private enum powerUps { NoDie, lowJump, Null }
    private powerUps current_powerup;
    private bool playerWithPowerUp;
    private float spawnPowerUp, time_playerhadwithpowerup;
    private int player_to_give_powerUp; //  [1|2]

    // Use this for initialization
    void Start()
    {
        playerWithPowerUp = false;
        spawnPowerUp = 0;
        time_playerhadwithpowerup = 0;
        current_powerup = powerUps.Null;
    }

    // Update is called once per frame
    void Update()
    {

        // Rotate cube for neat(ish) purposes
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);

        // Check if a power needs to stop
        if (playerWithPowerUp && Time.time - time_playerhadwithpowerup > 10 || !playerWithPowerUp && Time.time - spawnPowerUp > 20)
        {
            current_powerup = powerUps.Null;
            playerWithPowerUp = false;
            time_playerhadwithpowerup = Time.time;
            spawnPowerUp = Time.time;
            usePowerUps(player_to_give_powerUp, false);
        }

        // Check if we need to render the object
        //GetComponent<MeshRenderer>().enabled = current_powerup != powerUps.Null;

        // Check if a powerUp can spawn
        if (current_powerup == powerUps.Null)
        {
            // can generate a powerUp

            if (Random.Range(0, 100) == 1)
            {
                current_powerup = (powerUps)Random.Range(0, System.Enum.GetNames(typeof(powerUps)).Length - 1);
                spawnPowerUp = Time.time;

            }
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Ball")
        {
            player_to_give_powerUp = playerImpact.playerCollision;
            time_playerhadwithpowerup = Time.time;
            playerWithPowerUp = true;

            usePowerUps(player_to_give_powerUp,true);
        }
    }

    void usePowerUps(int player, bool activate)
    {
        switch (current_powerup)
        {
            case powerUps.lowJump:
                if (player == 1)
                {
                    if (activate)
                        player2.GetComponent<Rigidbody>().mass = 120;
                    else
                        player2.GetComponent<Rigidbody>().mass = 80;
                }
                else
                {
                    if (activate)
                        player1.GetComponent<Rigidbody>().mass = 120;
                    else
                        player1.GetComponent<Rigidbody>().mass = 80;
                }
                break;
            case powerUps.NoDie:

                if (activate)
                {
                    playerImpact.activePowerUp = player;
                    playerImpact.playerWithNoDie = true;
                }
                else
                {
                    playerImpact.activePowerUp = 0;
                    playerImpact.playerWithNoDie = false;
                }
                break;
            default:
                break;
        }
    }
}
