using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Attack : MonoBehaviour {
	private Collider attack;
	private MeshRenderer attackeffect;

	public AudioClip swordswing;
	private AudioSource source { get { return GetComponent<AudioSource> (); } }

	public bool attackbutton = false;
	public bool damageBoxActive;

	public int frameCounter;
	public int frameActive;
	public int attackcd = 0;

	public int damageOfMove;

	Animator anim;



	private void Start()
	{
		attack = GetComponent<Collider>();
		attackeffect = GetComponent<MeshRenderer> ();
		attack.enabled = false;
		attackeffect.enabled = false;

		anim = GameObject.Find("Player 1").GetComponent<Animator> ();
		gameObject.AddComponent<AudioSource> ();
		source.clip = swordswing;
		source.playOnAwake = false;

	}

	public void initialize_attack (int frames)
	{
		damageBoxActive = true;
		frameCounter = 0;
		frameActive = frames;
	}

	public void FixedUpdate()
	{
		attackeffect.enabled = attack.enabled;
		anim.SetBool ("attack", attack.enabled);
		if (attack.enabled) {
			source.PlayOneShot (swordswing);
		}

		if (Input.GetButton ("Con1_Attack") && !attackbutton) {
			attackbutton = true;
			attackcd = 0;

			initialize_attack (5);
		}
		attackcd++;
		if (!Input.GetButton ("Con1_Attack") && attackbutton) {
			if (attackcd > 40) {
				attackbutton = false;
			}
		}

		if (damageBoxActive) {
			frameCounter++;
			if (frameCounter <= frameActive) {
				attack.enabled = true;
			} else {
				attack.enabled = false;
				damageBoxActive = false;
			}
		}
	}

	private void OnTriggerEnter(Collider col)
	{

		if (col.gameObject.layer == 10) {
			Player2Controller P2 = col.gameObject.GetComponent<Player2Controller> ();
			P2.hp -= 25;
			Color temp = col.gameObject.GetComponentInChildren<Light> ().color;
			temp.g -= .2f;
			col.gameObject.GetComponentInChildren<Light> ().color = temp;
			Light tempL = col.gameObject.GetComponentInChildren<Light>();
			tempL.intensity -= .2f;
			CameraShake CamShake = GameObject.Find ("Main Camera").GetComponent<CameraShake> ();
			CamShake.PlayerShake (.5f);

			// new minion stats
			if (P2.hp <= 0) {
				GameObject Cam = GameObject.Find ("Main Camera");
				AgentRespawn NewStat = Cam.gameObject.GetComponent<AgentRespawn> ();
				NewStat.Agent1InHP += 5;
				NewStat.Agent1dmg += 2;
			}
		}
		if (col.gameObject.tag == "Agent2") {
			Agent2Controller A2 = col.gameObject.GetComponent<Agent2Controller> ();
			A2.HP -= 25;
		}
 
	}
}

