using UnityEngine;
using System.Collections;

public class ObjectFinder : MonoBehaviour {

    public GameObject[] respawns;

    // Use this for initialization
    void Start () {
         if (respawns == null)
            respawns = GameObject.FindGameObjectsWithTag("Respawn");

        foreach (GameObject respawn in respawns) {

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
