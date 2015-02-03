using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter () {
		GameObject.Find ("PersistentData").GetComponent<PersistentData>().menuToShow = TitleMenuGUI.Menu.Credits;
		Application.LoadLevel ("Title Menu");
	}
}
