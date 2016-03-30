using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class powerUpController : MonoBehaviour
{

    public GameObject player1, player2;

    public GameObject canvasText, shadow;
    private enum powerUps { NoDie, lowJump, lowSpeed,Null }
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
        transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * 90f);
        
        // Check if a power needs to stop
        if ( (playerWithPowerUp && Time.time - time_playerhadwithpowerup > 10) || (!playerWithPowerUp && Time.time - spawnPowerUp > 10))
        {
            spawnPowerUp = Time.time;

            if (playerWithPowerUp)
            {
                usePowerUps(player_to_give_powerUp, false);
                current_powerup = powerUps.Null;
                playerWithPowerUp = false;
            }
        }
        
        // Check if a powerUp can spawn
        if (current_powerup == powerUps.Null)
        {
            // can generate a powerUp

            if (Random.Range(0, 1000) == 1)
            {
                current_powerup = (powerUps)Random.Range(0, System.Enum.GetNames(typeof(powerUps)).Length - 1);
                spawnPowerUp = Time.time;

            }
        }

        // Check if we need to render the object
        GetComponent<MeshRenderer>().enabled = current_powerup != powerUps.Null && !playerWithPowerUp;
        GetComponent<Collider>().enabled = current_powerup != powerUps.Null && !playerWithPowerUp;

    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Ball")
        {
            player_to_give_powerUp = playerImpact.playerCollision;
            time_playerhadwithpowerup = Time.time;
            playerWithPowerUp = true;

            usePowerUps(player_to_give_powerUp, true);

            canvasText.SetActive(true);
            canvasText.GetComponent<Text>().text = current_powerup.ToString();
            shadow.SetActive(true);
            shadow.GetComponent<Text>().text = current_powerup.ToString();
            StartCoroutine(desactivateCanvas());
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
                        player2.GetComponentInChildren<ThirdPersonCharacter>().m_JumpPower = 5f;
                    else
                        player2.GetComponentInChildren<ThirdPersonCharacter>().m_JumpPower = 10f;
                }
                else
                {
                    if (activate)
                        player1.GetComponentInChildren<ThirdPersonCharacter>().m_JumpPower = 5f;
                    else
                        player1.GetComponentInChildren<ThirdPersonCharacter>().m_JumpPower = 10f;
                }
                break;
            case powerUps.NoDie:
                
                playerImpact.activePowerUp = activate;
                playerImpact.playerWithNoDie = player;
                break;
            case powerUps.lowSpeed:
                if (player == 1)
                {
                    if (activate)
                        player2.GetComponentInChildren<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 0.8f;
                    else
                        player2.GetComponentInChildren<ThirdPersonCharacter>().m_JumpPower = 1.2f;
                }
                else
                {
                    if (activate)
                        player1.GetComponentInChildren<ThirdPersonCharacter>().m_JumpPower = 0.8f;
                    else
                        player1.GetComponentInChildren<ThirdPersonCharacter>().m_JumpPower = 1.2f;
                }
                break;
            default:
                break;
        }
    }

    IEnumerator desactivateCanvas()
    {
        yield return new WaitForSeconds(2);
        canvasText.SetActive(false);
        shadow.SetActive(false);
    }
}
