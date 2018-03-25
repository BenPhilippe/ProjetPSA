using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// SCENE CONFIGURATION
	public int numberOfTestsDone = 0;
	public List<TestData> testDataList;

	private void Awake() {
		DontDestroyOnLoad(gameObject);
		// Load second scene in Build Setup
		SceneManager.LoadScene(1);
	}

	public void LoadScene(string s){
		SceneManager.LoadScene(s);
	}
}
