using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameManager GM;

	public GameObject[] configurationPanels;
	int configPanelIndex = 0;
	public Button previousConfigStepButton, nextConfigStepButton;

	private void Awake() {
		ChangeConfigPanel();
		ChangeButtonsState();
	}

	private void Update() {
		
	}

	public void ChangeConfigPanel(){
		foreach(GameObject g in configurationPanels){
			bool b = false;
			if(g == configurationPanels[configPanelIndex]){
				b = true;
			}
			g.SetActive(b);
		}
	}

	private void ChangeButtonsState(){
		if(configPanelIndex < configurationPanels.Length -1){
			nextConfigStepButton.interactable = true;
		} else{
			nextConfigStepButton.interactable = false;
		}
		if (configPanelIndex > 0){
			previousConfigStepButton.interactable = true;
		} else {
			previousConfigStepButton.interactable = false;
		}
	}

	public void PreviousConfigPanel(){
		if(configPanelIndex > 0){
			configPanelIndex--;
			ChangeConfigPanel();
			ChangeButtonsState();
		}
	}

	public void NextConfigPanel(){
		if(configPanelIndex < configurationPanels.Length -1){
			configPanelIndex++;
			ChangeConfigPanel();
			ChangeButtonsState();
		}
	}
}
