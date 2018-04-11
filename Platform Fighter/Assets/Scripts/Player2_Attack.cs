using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_Attack : MonoBehaviour {
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

		anim = GameObject.Find("Player 2").GetComponent<Animator> ();
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
		if (Input.GetButton ("Con2_Attack") && !attackbutton) {
			attackbutton = true;
			attackcd = 0;

			initialize_attack (10);
		}
		attackcd++;
		if (!Input.GetButton ("Con2_Attack") && attackbutton) {
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

		if (col.gameObject.layer == 9) {
			Player1Controller P1 = col.gameObject.GetComponent<Player1Controller> ();
			P1.hp -= 25;
			//Changes color based on attack
			Color temp = col.gameObject.GetComponentInChildren<Light> ().color;
			temp.r -= .2f;

			//Changes intensity based on attack
			Light tempL = col.gameObject.GetComponentInChildren<Light>();
			tempL.intensity -= .2f;

			CameraShake CamShake = GameObject.Find ("Main Camera").GetComponent<CameraShake> ();
			CamShake.PlayerShake (.5f);
			if (P1.hp <= 0) {
				GameObject Cam = GameObject.Find ("Main Camera");
				AgentRespawn NewStat = Cam.gameObject.GetComponent<AgentRespawn> ();
				NewStat.Agent2InHP += 5;
				NewStat.Agent2dmg += 2;
			}


		}
		if (col.gameObject.tag == "Agent1") {
			Agent1Controller A1 = col.gameObject.GetComponent<Agent1Controller> ();
			A1.HP -= 25;
		}

	}
}