using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	//related to UI updates
	public Transform winningMessage;
	public Transform player1;
	public Transform player2;
	public GameObject ballFallPositionHelper;

	public Text [] playersScores;
	public Text [] playersNames;
	public Transform [] pointToMessages;

	private Rigidbody rb;
	private int [] pointCounters = {0,0};
	private float sleepingDuration = 1.0f;

	float sleepingBetweenTurns = -1;
	int gameStartedBy = 1;

	void Start(){
		rb = this.GetComponent<Rigidbody> ();
		RestartGame ();
		sleepingBetweenTurns = sleepingDuration;  // start immediately
	}

	private void setWinner(string winnerMessage){
		foreach (Transform msg in winningMessage) {
			Text txt = msg.gameObject.GetComponent<Text> ();
			if(txt) txt.text = winnerMessage;
		}
		winningMessage.gameObject.SetActive(true);
		ballFallPositionHelper.SetActive (false);
	}


	public void pointTo(int id){
		if (++pointCounters [id] == 5) {
			setWinner (playersNames [id].text + " wins!");
			positionBallForPlayer (id == 0 ? 1 : 0);
			sleepingBetweenTurns = -1;  // stop ball indefinitely

		} else {
			positionBallForPlayer (gameStartedBy == 0 ? 1 : 0);
			pointToMessages [id].gameObject.SetActive (true);
			ballFallPositionHelper.SetActive (false);
		}
		playersScores[id].text = pointCounters[id].ToString();
	}

	private void positionBallForPlayer(int id){
		this.transform.position = new Vector3 (4.5f, 12f, id*9f + 4.5f);
		rb.velocity = new Vector3(0,0,0);
		rb.angularVelocity = new Vector3(0,0,0);
		rb.isKinematic = true;
		sleepingBetweenTurns = 0;
	}

	private void resume(){
		rb.isKinematic = false;
		pointToMessages [0].gameObject.SetActive (false);
		pointToMessages [1].gameObject.SetActive (false);
		player1.position = new Vector3 (4.5f, 1f, 4.5f);
		player2.position = new Vector3 (4.5f, 1f, 13.5f);
		ballFallPositionHelper.SetActive (true);


	}

	void Update(){
		if (sleepingBetweenTurns >= 0) {
			sleepingBetweenTurns += Time.deltaTime;
			if (sleepingBetweenTurns > sleepingDuration) {
				resume ();
				sleepingBetweenTurns = -1;
			}
		}
	}

	public void RestartGame(){
		pointCounters[0] = 0;
		pointCounters[1] = 0;
		winningMessage.gameObject.SetActive(false);
		resume ();
	}
}
