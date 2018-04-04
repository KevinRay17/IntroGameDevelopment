using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {
	public GameObject Player_1;
	public GameObject Player_2;
	public float P1RespawnTime = 3.0f;
	public float P2RespawnTime = 3.0f;


	bool P1Respawning = false;
	bool P2Respawning = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//P1 Respawn
		GameObject P1 = GameObject.Find("Player 1");
		if (P1 == null  && !P1Respawning) {
			P1Respawning = true;
			P1RespawnTime = 3.0f;
		}

		if (P1Respawning) {
			P1RespawnTime -= Time.deltaTime;
			//Animation calls on this line
			if (P1RespawnTime <= 0) {
				GameObject Player1Respawned = Instantiate (Player_1, new Vector3 (11.28f, 0f, 0f), Quaternion.identity);
				Player1Respawned.name = "Player 1";
				P1Respawning = false;
			}
		}
		//P2 Respawn
		GameObject P2 = GameObject.Find("Player 2");
		if (P2 == null  && !P2Respawning) {
			P2Respawning = true;
			P2RespawnTime = 3.0f;
		}
	
		if (P2Respawning) {
			P2RespawnTime -= Time.deltaTime;
			//Animation calls on this line
			if (P2RespawnTime <= 0) {
				GameObject Player2Respawned = Instantiate (Player_2, new Vector3 (11.28f, 0f, 0f), Quaternion.identity);
				Player2Respawned.name = "Player 2";
				P2Respawning = false;
			}
		}
}
}