using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRespawn : MonoBehaviour {
	public int Agent1InHP = 10;
	public int Agent2InHP = 10;
	public int Agent1dmg = 2;
	public int Agent2dmg = 2;

	public int CastleL = 10;
	public int CastleR = 10;
 
	public float spawnCD = 3;
	public float timeUntilSpawn = 3;
	public GameObject Agent1;
	public GameObject Agent2;
	public GameObject ButtonPanel;

	private List<GameObject> Agents1;
	private List<GameObject> Agents2;

	int Count1 = 0;
	int Count2 = 0;
	// Use this for initialization
	void Start () {
		ButtonPanel.SetActive(false);
		Agents1 = new List<GameObject> ();
		Agents2 = new List<GameObject> ();
		
	}

	public int CastleLHealth()
	{
		return CastleL;
	}
	public int CastleRHealth()
	{
		return CastleR;
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		timeUntilSpawn -= Time.deltaTime;
		if (timeUntilSpawn <= 0 && CastleL >=0 && CastleR >=0) {
			GameObject Agent1Clone = Instantiate(Agent1, new Vector3 (-38.0f, -13.89f, 0f), Quaternion.identity);
			Agent1Controller AI1 = Agent1Clone.gameObject.GetComponent<Agent1Controller> ();
			AI1.HP = Agent1InHP;
			AI1.DMG = Agent1dmg;
			Agent1Clone.name = "1_" + Count1.ToString ();
			Agents1.Add (Agent1Clone);
			Count1++;

			GameObject Agent2Clone = Instantiate(Agent2, new Vector3 (38.0f, -13.89f, 0f), Quaternion.identity);
			Agent2Controller AI2 = Agent2Clone.gameObject.GetComponent<Agent2Controller> ();
			AI2.HP = Agent2InHP;
			AI2.DMG = Agent2dmg;
			Agent2Clone.name = "2_" + Count2.ToString ();
			Agents2.Add (Agent2Clone);
			Count2++;
			timeUntilSpawn = spawnCD;
		}

		//OnGameOver
		if (CastleL <= 0) {
			foreach(GameObject A in Agents1) {
				if (A != null)
					Destroy (A);
			}
			Agents1.Clear ();
		}
		if (CastleR <= 0) {
			foreach(GameObject A in Agents2) {
				if (A != null)
					Destroy (A);
			}
			Agents2.Clear();
		}

		if (CastleL <= 0 || CastleR <= 0) {
			ButtonPanel.SetActive (true);
			CameraShake CamShake = GameObject.Find ("Main Camera").GetComponent<CameraShake> ();
			CamShake.LongShake (.5f);
			}

	}
	public void DeleteAgent(GameObject A) {
		A.GetComponentInChildren<ParticleSystem> ().Play ();
		int loc = Agents1.FindIndex (x => x.name == A.name);
		if (loc >= 0) {
			Agents1.RemoveAt (loc);
		}
		loc = Agents2.FindIndex (x => x.name ==A.name);
		if (loc >= 0) {
			Agents2.RemoveAt (loc);
		}
		Destroy (A);
	}
}
