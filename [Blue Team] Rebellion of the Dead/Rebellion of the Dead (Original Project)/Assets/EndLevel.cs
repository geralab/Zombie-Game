using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndLevel : MonoBehaviour {
	
	public List<Transform> ListOfZombies;
	public List<Transform> ListOfHumans;
	
	// Use this for initialization
	void Start () {
		ListOfZombies = HumanoidHandler.ListOfZombies;
		ListOfHumans = HumanoidHandler.ListOfHumans;
	}
	
	// Update is called once per frame
	void Update () {
		if (ListOfHumans.Count == 0) {
			GUI.Box (new Rect(0, 0, Screen.width, Screen.height), "You win!");
			Camera.mainCamera.GetComponent<PriorityAudio>().PlayWin();
			GameObject.Find ("PersistentData").GetComponent<PersistentData>().chaptersUnlocked++;
			GameObject.Find ("PersistentData").GetComponent<PersistentData>().menuToShow = TitleMenuGUI.Menu.Campaign;
			Application.LoadLevel ("Title Menu");
		}
		else if (ListOfZombies.Count == 0) {
			GUI.Box (new Rect(0, 0, Screen.width, Screen.height), "You lose!");
			Camera.mainCamera.GetComponent<PriorityAudio>().PlayLose();
			GameObject.Find ("PersistentData").GetComponent<PersistentData>().menuToShow = TitleMenuGUI.Menu.Credits;
			Application.LoadLevel ("Title Menu");
		}
	}
}
