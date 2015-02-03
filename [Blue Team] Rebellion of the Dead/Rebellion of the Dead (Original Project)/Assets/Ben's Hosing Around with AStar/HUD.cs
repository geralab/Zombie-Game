using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour {
	
	//Initially set to a starting Transform object, empty or otherwise, in the Inspector.
	private Transform target; 
	public List<Transform> ListOfZombies;
	public Vector3 anchorPosition; //For selection rect.
	public Rect selectionBox;
	public float rectMinSize; //If dist from start of a click to current mouse position tops this, we draw a rect.
	public GUIStyle selectionBoxStyle;
	public Transform marker; //An marker that is used to target areas on the ground/obstacle sides.
	private bool drawBox; //Because Unity is tableflip. Drawing box in other logic requires the method to be OnGui. 
	//But if this happens, then GetMouseButtonUp is fired twice every click. RAAAAGE.
	private bool multiSelect; //Is Shift/Ctrl down?
	private Vector3 cameraPos;
	public GUIStyle logoStyle;
	
	// Use this for initialization
	void Start () {
		this.ListOfZombies = HumanoidHandler.ListOfZombies;
		marker.renderer.enabled = false;
	}
	
	//Might come back to add some GUI elements here that essentially work as the camera keys in Update().
	//Also, three buttons that will select all of a type of zombie, also corresponds to Update()'s code.
	//Finally, three buttons to set attack mode / infect of selected zombies, also given keys in Update().
	void OnGUI () {
		if (drawBox) GUI.Box(selectionBox, "");	
		
		GUI.Button (GetScrnRect (0, .8, .1, .1), "Infect (Q)");
		GUI.Button (GetScrnRect (.1, .8, .1, .1), "Lite Atk (W)");
		GUI.Button (GetScrnRect (.2, .8, .1, .1), "Mid Atk (E)");
		GUI.Button (GetScrnRect (.3, .8, .1, .1), "Hvy Atk (R)");
		
		GUI.Button (GetScrnRect (0, .9, .1, .1), "Tilt View (A)");
		if (GUI.Button (GetScrnRect (.1, .9, .1, .1), "Strong (S)")) {
			foreach (Transform zombie in ListOfZombies) 
			if (zombie.GetComponent<HumanoidHandler>().CurrZombType == HumanoidHandler.StateZombieType.Strong) 
				zombie.GetComponent<HumanoidHandler>().isSelected = true;
		}
		if (GUI.Button (GetScrnRect (.2, .9, .1, .1), "Sneaky (D)")){
			foreach (Transform zombie in ListOfZombies) 
			if (zombie.GetComponent<HumanoidHandler>().CurrZombType == HumanoidHandler.StateZombieType.Sneeky) 
				zombie.GetComponent<HumanoidHandler>().isSelected = true;
			}
		if (GUI.Button (GetScrnRect (.3, .9, .1, .1), "Fast (F)")){
			foreach (Transform zombie in ListOfZombies) 
			if (zombie.GetComponent<HumanoidHandler>().CurrZombType == HumanoidHandler.StateZombieType.Fast) 
				zombie.GetComponent<HumanoidHandler>().isSelected = true;
		}
		
		GUI.Button (GetScrnRect (.87, .85, .1, .13), "RotD", logoStyle);
	}
	
	//Takes fraction values representing portions of the screen (e.g. a button is half the screen = 1/2 for width).
	Rect GetScrnRect(double x, double y, double w, double h) {
		return new Rect((float)(Screen.width*x), (float)(Screen.height*y), (float)(Screen.width*w), (float)(Screen.height*h));	
	}
	
	//In essence, sets isSelected on zombies under various mouse click related conditions.
	//Also moves the camera according to virtual axes Horizontal and Vertical.
	void Update() {
		//Set infect.
		if (Input.GetAxis ("Infect") != 0f) foreach (Transform zombie in ListOfZombies)
			if (zombie.GetComponent<HumanoidHandler>().isSelected) 
				zombie.GetComponent<HumanoidHandler>().isInfecting = true;
		
		if (Input.GetAxis ("Lite Atk") != 0f) foreach (Transform zombie in ListOfZombies) 
		{
			if (zombie.GetComponent<HumanoidHandler>().isSelected) 
				zombie.GetComponent<HumanoidHandler>().CurrUsingAttack = HumanoidHandler.StateCharacterAtkUsing.WeakAttack;
		}
		else if (Input.GetAxis ("Mid Atk") != 0f) foreach (Transform zombie in ListOfZombies) 
		{
			if (zombie.GetComponent<HumanoidHandler>().isSelected) 
				zombie.GetComponent<HumanoidHandler>().CurrUsingAttack = HumanoidHandler.StateCharacterAtkUsing.MediumAttack;
		}
		else if (Input.GetAxis ("Hvy Atk") != 0f) foreach (Transform zombie in ListOfZombies) 
		{
			if (zombie.GetComponent<HumanoidHandler>().isSelected) 
				zombie.GetComponent<HumanoidHandler>().CurrUsingAttack = HumanoidHandler.StateCharacterAtkUsing.StrongAttack;
		}
		//Deselect.
		if (Input.GetMouseButton (1)) 
			foreach (Transform zombie in ListOfZombies)
				zombie.GetComponent<HumanoidHandler>().isSelected = false;
		
		#region Zombie Type Select and Deselect
		if (Input.GetAxis ("Strong") != 0f) foreach (Transform zombie in ListOfZombies) 
			if (zombie.GetComponent<HumanoidHandler>().CurrZombType == HumanoidHandler.StateZombieType.Strong) 
			 zombie.GetComponent<HumanoidHandler>().isSelected = true;
		
		if (Input.GetAxis ("Sneaky") != 0f) foreach (Transform zombie in ListOfZombies) 
			if (zombie.GetComponent<HumanoidHandler>().CurrZombType == HumanoidHandler.StateZombieType.Sneeky) 
			 zombie.GetComponent<HumanoidHandler>().isSelected = true;
		
		if (Input.GetAxis ("Fast") != 0f) foreach (Transform zombie in ListOfZombies) 
			if (zombie.GetComponent<HumanoidHandler>().CurrZombType == HumanoidHandler.StateZombieType.Fast) 
			 zombie.GetComponent<HumanoidHandler>().isSelected = true;
		#endregion
		
		
		#region Camera Controls
		//Translation: Dolly forward and back for the Up and down keys.
		if (Input.GetAxis ("Vertical") < 0) {
			if (Input.GetAxis ("Look Up/Down") == 0f || Input.GetAxis ("Mouse ScrollWheel") < 0f) 
				Camera.mainCamera.transform.Translate(Vector3.back, Space.World);
			else 
				Camera.mainCamera.transform.Rotate (Vector3.left);
			
			if (Camera.mainCamera.transform.rotation.x > 90) 
				Camera.mainCamera.transform.Rotate(Camera.mainCamera.transform.rotation.x - 90, 0, 0);
		}
		else if (Input.GetAxis ("Vertical") > 0) {
			if (Input.GetAxis ("Look Up/Down") == 0f || Input.GetAxis ("Mouse ScrollWheel") > 0f) 
				Camera.mainCamera.transform.Translate (Vector3.forward, Space.World);
			else 
				Camera.mainCamera.transform.Rotate (Vector3.right);
			
			if (Camera.mainCamera.transform.rotation.x < 0) 
				Camera.mainCamera.transform.Rotate (-Camera.mainCamera.transform.rotation.x, 0, 0);
		}
		#endregion
		
		#region Mouse-Related Selection
		//Left or Right Shift or Ctrl make this true.
		if (Input.GetAxis("Multiselect") == 0f) multiSelect = false;
		else multiSelect = true;
		
		if (Input.GetMouseButtonDown(0)) //Click start.
		{
			//Debug.Log ("In click start.");
			anchorPosition = Input.mousePosition;
		}
		else if (Input.GetMouseButton(0)) //Dragging.
		{
			#region Dragging box selection.
			//Draw a rectangle from the anchorPosition to the current mousePosition.
			//But only if the distance between them is over the rectMinSize amount.
			if (Vector3.Distance (anchorPosition, Input.mousePosition) > rectMinSize)
			{
				//Probably will need to make four of these depending on where Input.mousePosition is relative to anchorpos.
				//i.e. Do a difference of the positions and check signs of x and y components. (0,0) is bottom left corner.
				//Debug.Log ("In Dragging, Also Rect Draw");
				if (anchorPosition.x < Input.mousePosition.x) //If startclick is left of currMousePoint.
				{
					if (anchorPosition.y < Input.mousePosition.y) //If startclick is below currMousePoint. i.e. startclick BL, currMousePoint TR.
						selectionBox = Rect.MinMaxRect(anchorPosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.x, Screen.height - anchorPosition.y);		
					else //If startclick is above currMousePoint, i.e. startclick TL, currMousePoint BR.
						selectionBox = Rect.MinMaxRect(anchorPosition.x, Screen.height - anchorPosition.y, Input.mousePosition.x, Screen.height - Input.mousePosition.y);		
						
				}
				else //If startclick is right of currMousePoint.
				{
					if (anchorPosition.y < Input.mousePosition.y) //If startclick is below currMousePoint, i.e. startclick BR, currMousePoint TL.
						selectionBox = Rect.MinMaxRect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, anchorPosition.x, Screen.height - anchorPosition.y);		
					else //If startclick is above currMousePoint, i.e. startclick TR, currMousePoint BL.
						selectionBox = Rect.MinMaxRect(Input.mousePosition.x, Screen.height - anchorPosition.y, anchorPosition.x, Screen.height - Input.mousePosition.y);		
				}	
				
				drawBox = true;
			}
			#endregion
		}
		else if (Input.GetMouseButtonUp(0)) //Click release.
		{
			//Debug.Log (selectionBox);
			if (selectionBox == new Rect(-1, -1, -1, -1)) //Just because you can't set Rect to null.
			{
				//Debug.Log ("Normal Click");
				#region Normal click logic goes here, i.e. no drag select.
				marker.renderer.enabled = false;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Cast the click as a ray into the scene...
				RaycastHit hit; 
				if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
				{
					//Debug.Log ("The tag of the transform that the click collided with is: " + hit.transform.tag);
					//Debug.Log ("The layer of the transform that the click collided with is: " + hit.transform.gameObject.layer);
					if (hit.transform.tag == "Human" || hit.transform.tag == "Construct") 
					{ 
						foreach (Transform zombie in ListOfZombies) 
							if (zombie.GetComponent<HumanoidHandler>().isSelected)
								zombie.GetComponent<AIPath>().target = hit.transform;
					}
					else if (hit.transform.tag == "Zombie") 
					{  
						//See Input Manager, it means Left/Right Ctrl/Shift is NOT down. Then we deselect all zombies.
						if (!multiSelect) 
						{
							foreach (Transform zombie in ListOfZombies) 
								zombie.GetComponent<HumanoidHandler>().isSelected = false;
						}
						//If selected, deselect; if not selected, select!
						//Debug.Log ("1: " + hit.transform.GetComponent<HumanoidHandler>().isSelected);
						hit.transform.GetComponent<HumanoidHandler>().isSelected = !hit.transform.GetComponent<HumanoidHandler>().isSelected;
						//Debug.Log ("2: " + hit.transform.GetComponent<HumanoidHandler>().isSelected);
					}
					//Indices of Ground&Obstacles layers are 8&9, so this means if we have clicked nothing else but the ground/obstacles.
					//Note that Constructs have already been taken of, so these are non-destrucible obstructions like trees and whatnot.
					else if (hit.transform.gameObject.layer == 8 ||  hit.transform.gameObject.layer == 9) 
					{
						marker.renderer.enabled = true;
						marker.position = hit.point;
						foreach (Transform zombie in ListOfZombies) 
						{
							HumanoidHandler hh = zombie.GetComponent<HumanoidHandler>();
							if (hh.isSelected) 
							{
								hh.emptyTarget.position = hit.point;
								hh.Target = hh.emptyTarget;
								//Keeps zombie from being distracted while walking to target.
								hh.isTargeting = true; 
							}
						}
					}
				}
				else 
				{
					foreach (Transform zombie in ListOfZombies) 
					zombie.GetComponent<HumanoidHandler>().isSelected = false;
				}
				#endregion
			}
			else 
			{
				//Translate the positions of every zombie in the scene into a screen point.
				//If the point is inside the rectangle, then set the current loop iteration's zombie's isSelected = true.
				foreach (Transform zombie in HumanoidHandler.ListOfZombies) 
				{
					//Debug.Log (Camera.main.WorldToScreenPoint(zombie.position));
					Vector3 derp = Camera.main.WorldToScreenPoint(zombie.position);
					derp.y = Screen.height - derp.y;
					if (selectionBox.Contains(derp)) 
					{
						zombie.GetComponent<HumanoidHandler>().isSelected = true;
					}
					else
					{
						if (!multiSelect) zombie.GetComponent<HumanoidHandler>().isSelected = false;
					}
				}
				selectionBox = new Rect(-1, -1, -1, -1); //At the end.
			}
		}
		#endregion
	}
}
