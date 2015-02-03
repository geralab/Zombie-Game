using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleMenuGUI : MonoBehaviour {
	public enum Menu { Title, Controls, AV, Campaign, Credits }
	Menu menuType; 
	Texture2D currImg;
	
	public int timer;
	public int chaptersUnlocked;
	public Texture2D logo;
	public Texture2D titleImg;
	public Texture2D campaignImg; 
	public Texture2D avImg; 
	public Texture2D ctrlsImg; 
	public Texture2D creditsImg;
	public GUIStyle bttnStyle, bttnStyle2, bttnStyle3; 
	public AudioClip bgm;
	public List<string> credits;
	public string[,] controls;
	Vector2 scrollViewVector = Vector2.zero;  
	
	// Use this for initialization	
	void Start () {
		this.menuType = GameObject.Find ("PersistentData").GetComponent<PersistentData>().menuToShow;
		this.chaptersUnlocked = GameObject.Find ("PersistentData").GetComponent<PersistentData>().chaptersUnlocked;
		//bttnStyle.
		currImg = titleImg;
		AudioSource.PlayClipAtPoint(bgm, transform.position);
		controls = new string[,]	{		
		{ "LMB", "Click and/or drag to select/target" },
		{ "RMB", "Deselect all" },
		{ "Up/Down", "Dolly view" },
		{ "A", "Hold while dollying to tilt view" },
		{ "S", "Select all Strong zombies" },
		{ "D", "Select all Sneaky zombies" },
		{ "F", "Select all Fast zombies" },
		{ "Q", "Use 'Infect'" },
		{ "W", "Use Light Attack" },
		{ "E", "Use Medium Attack" },
		{ "R", "Use Heavy Attack" },
		{"Esc", "Return to Title" }
		};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		//Aim is to make the background images of the buttons transparent!
		//The "hover" can just be on the text.
		//That way, the player can see the + axes and Label in the background!
		
		//Draw the image on the outside of the switch statement because it's a constant.
		//GUI.Label(new Rect(0, 0, logo.width, logo.height), logo); //Menu title.
		GUI.Label (GetScrnRect(1/2.0, .02, 1/2.0, 1/4.0), ( menuType == Menu.Title ? "Rebellion\n of the Dead" : menuType.ToString ()), bttnStyle2);
		GUI.Box (GetScrnRect(1/2.0 + .06, 1/4.0 + .02, 3/8.0, 1/2.0), currImg, bttnStyle);
		
		if (menuType == Menu.Title) { if (GUI.Button (GetScrnRect(0, 4/5.0, 1/3.0, .20), "Exit", bttnStyle)) Application.Quit(); }  //Quit game button.
		else { if (GUI.Button (GetScrnRect(0, 4/5.0, 1/3.0, .20), "Back", bttnStyle)) { menuType = Menu.Title; currImg = titleImg; } } //Back to title button.
		
	switch (menuType)
		{
		case Menu.Title:
			//GUI.Label(new Rect(Screen.width-logo.width/2+22, Screen.height-logo.height/2+6, logo.width, logo.height), logo);
			GUI.color = Color.white;
			GUI.backgroundColor = Color.white;
			GUI.contentColor = Color.white;
			if (GUI.Button (GetScrnRect(0, 0, .5, .5), "Campaign", bttnStyle)) { menuType = Menu.Campaign; currImg = campaignImg; }
			//if (GUI.Button (GetScrnRect(.5, 0, .5, .5), "Controls", bttnStyle)) { menuType = Menu.Controls; currImg = ctrlsImg; }
			if (GUI.Button (GetScrnRect(1/3.0, 4/5.0, 1/3.0, .20), "Options", bttnStyle)) { menuType = Menu.AV; currImg = avImg; }
			if (GUI.Button (GetScrnRect(2/3.0, 4/5.0, 1/3.0, .20), "Credits", bttnStyle)) { menuType = Menu.Credits; currImg = creditsImg; }
			break;
			
		case Menu.Campaign:
			//GUI.Label(new Rect(Screen.width-logo.width/2+22, Screen.height-logo.height/2+6, logo.width, logo.height), logo);
			GUI.color = Color.white;
			GUI.backgroundColor = Color.white;
			GUI.contentColor = Color.white;
			bttnStyle.alignment = TextAnchor.MiddleLeft;
			if (++timer > 100 && Input.GetKeyDown(KeyCode.Tab) && chaptersUnlocked <= 6) { chaptersUnlocked++; timer = 0; }
			for (int i = 0; i < 6; i++)
			{
				GUI.enabled = chaptersUnlocked > i ? true : false; //Disables the button if the section is not yet unlocked.
				if (GUI.Button (GetScrnRect(.05	, i*(1/9.0) + .05, 1/2.0, 1/9.0), "Level " + (i+1), bttnStyle))
				{
					//print ("Load Level " + (i+1));
					Application.LoadLevel("Level " + (i+1));
				}
			}
			bttnStyle.alignment = TextAnchor.MiddleCenter;
			break;
		
		case Menu.AV:
			GUI.color = Color.white;
			GUI.backgroundColor = Color.white;
			GUI.contentColor = Color.white;
			if (GUI.Button (GetScrnRect(1/3.0, 4/5.0, 1/3.0, .20), "Controls", bttnStyle)) { menuType = Menu.Controls; currImg = ctrlsImg; }
			scrollViewVector = GUI.BeginScrollView (GetScrnRect(.05, .02, 1/2.0, 4/5.0 - .02), scrollViewVector, new Rect(0, 0, .8f*Screen.width, 510));
			GUI.Label (new Rect(0, 0, 400, 200), "Game Volume", bttnStyle3);
			AudioListener.volume = GUI.HorizontalSlider (new Rect(0, 200, 400, 200), AudioListener.volume, 0.0f, 1.0f);
			GUI.EndScrollView();
			break;
			
		case Menu.Controls:
			//GUI.Label(new Rect(Screen.width-logo.width/2+22, Screen.height-logo.height/2+6, logo.width, logo.height), logo);
			GUI.color = Color.white;
			GUI.backgroundColor = Color.white;
			GUI.contentColor = Color.white;
			if (GUI.Button (GetScrnRect(1/3.0, 4/5.0, 1/3.0, .20), "AV", bttnStyle)) { menuType = Menu.AV; currImg = avImg; }
			scrollViewVector = GUI.BeginScrollView (GetScrnRect(.05, .02, 1/2.0, 4/5.0 - .02), scrollViewVector, new Rect(0, 0, .8f*Screen.width, controls.GetLength(0)*51));
			// Anything between Begin and End calls is inserted into the ScrollView relative to the SV's Begin call's first argument rect's XY.
     		
			for (int i = 0; i < controls.GetLength (0); i++)
					GUI.Label (new Rect(10, 10 + i*50, Screen.width/2, 50), controls[i,0] + " : " + controls[i,1], bttnStyle3);
    		// End the ScrollView	
    		GUI.EndScrollView(); 
			break;
			
		case Menu.Credits:
			//GUI.Label(new Rect(Screen.width-logo.width/2+22, Screen.height-logo.height/2+6, logo.width, logo.height), logo);
			GUI.color = Color.white;
			GUI.backgroundColor = Color.white;
			GUI.contentColor = Color.white;
			for (int i = 0; i < credits.Count; i++) 
			{
				if (i > 0 && i < 4) bttnStyle3.normal.textColor = Color.red;
				else if (i > 4) bttnStyle3.normal.textColor = Color.blue;
				GUI.Label (new Rect(15, i*50 + 0, Screen.width/2, 50), credits[i], bttnStyle3);
				bttnStyle3.normal.textColor = Color.black;
			}
			break;
			//Designers: Joonas Niemi, Vincent Stanto, Chris Comstock
			//Developers: Gerald Blake, Justin Cavin, Ben Gibson, Titus Thompson
		}
		
	}
	
	//Takes fraction values representing portions of the screen (e.g. a button is half the screen = 1/2 for width).
	Rect GetScrnRect(double x, double y, double w, double h) {
		return new Rect((float)(Screen.width*x), (float)(Screen.height*y), (float)(Screen.width*w), (float)(Screen.height*h));	
	}
}
