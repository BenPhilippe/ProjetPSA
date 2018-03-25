using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public GameManager GM;
	public TestData currentTestData;
	public bool hasAlreadyAddedData = false;

	public Text GM_Test_Text;

	public GameObject[] configurationPanels;
	int configPanelIndex = 0;
	public Button previousConfigStepButton, nextConfigStepButton;

	private void Awake() {
		GM = Object.FindObjectOfType<GameManager>();
		ChangeConfigPanel();
		ChangeButtonsState();

		currentTestData.ID = GM.numberOfTestsDone +1;
		currentTestData.distanceToCarCrash = Random.Range(15f, 100f);
		currentTestData.testDuration = Random.Range(2f, 60f);
		currentTestData.s = "blah";

		hasAlreadyAddedData = false;
	}

	private void Update() {
		//GM_Test_Text.text = GM.numberOfTestsDone.ToString();
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

	public void AddCurrentTestData(){
		if(GM != null){
			if(!GM.testDataList.Contains(currentTestData) && !hasAlreadyAddedData){
				GM.testDataList.Add(currentTestData);
				hasAlreadyAddedData = true;
			}
			GM.numberOfTestsDone ++;
		}
		
	}

	public void PreviousConfigPanel(){
	//	if(configPanelIndex > 0){
			configPanelIndex--;
			ChangeConfigPanel();
			ChangeButtonsState();
		//}
	}

	public void NextConfigPanel(){
		//if(configPanelIndex < configurationPanels.Length -1){
			configPanelIndex++;
			ChangeConfigPanel();
			ChangeButtonsState();
		//}
	}

	public void ReloadScene(){
		GM.LoadScene(SceneManager.GetActiveScene().name);
	}
}
