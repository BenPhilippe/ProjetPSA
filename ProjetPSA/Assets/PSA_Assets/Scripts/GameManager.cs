using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// SCENE CONFIGURATION

	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}
}
