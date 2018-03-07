using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// SCENE CONFIGURATION
	public int test_int = 0;

	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}
}
