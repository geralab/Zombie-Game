using UnityEngine;
using System.Collections;

public class HighlightOnSelect : MonoBehaviour {
	
	public Material selectedMaterial;
	public Material infectMaterial;
	private Material normalMaterial;
	private bool selectedHighlight, infectHighlight;
	
	// Use this for initialization
	void Start () 
	{
		selectedHighlight = false;
		infectHighlight = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.parent.GetComponent<HumanoidHandler>().isSelected && transform.parent.GetComponent<HumanoidHandler>().isInfecting) return;
		
		if (!selectedHighlight && transform.parent.GetComponent<HumanoidHandler>().isSelected) 
		{
			normalMaterial = renderer.material;
			renderer.material = selectedMaterial;
			selectedHighlight = true;
		}
		else if (selectedHighlight && !transform.parent.GetComponent<HumanoidHandler>().isSelected) 
		{
			renderer.material = normalMaterial;	
			selectedHighlight = false;
		}
		else if (!infectHighlight && transform.parent.GetComponent<HumanoidHandler>().isInfecting) 
		{
			//If already selected, normalMaterial is already set, else we need to set it ourselves.
			if (renderer.material != selectedMaterial) normalMaterial = renderer.material;
			
			renderer.material = infectMaterial;
			infectHighlight = true;
		}
		else if (infectHighlight && !transform.parent.GetComponent<HumanoidHandler>().isInfecting)
		{
			renderer.material = normalMaterial;	
			infectHighlight = false;
		}
	}
}

//I want it to be switching only if the isSelected is set. 
//I want it to revert not when isSelected is false, but when it switches to off.