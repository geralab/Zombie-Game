using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {
	
	public int chaptersUnlocked;
	public TitleMenuGUI.Menu menuToShow;
	
	// Use this for initialization
	void Start () {
		chaptersUnlocked = 1;
		menuToShow = TitleMenuGUI.Menu.Title;
	}
	
	// Update is called once per frame
	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
	}
	
	void Update() {
		if (Input.GetAxis ("Escape") != 0f) {
			menuToShow = TitleMenuGUI.Menu.Title;
			Application.LoadLevel ("Title Menu");	
		}
	}
}
