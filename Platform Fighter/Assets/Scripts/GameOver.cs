using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public void Main_Menu() {
		SceneManager.LoadScene(0);
	}

	public void Rematch() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
