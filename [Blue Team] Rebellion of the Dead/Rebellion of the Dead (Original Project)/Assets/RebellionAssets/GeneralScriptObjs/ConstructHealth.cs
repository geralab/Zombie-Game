using UnityEngine;
using System.Collections;

public class ConstructHealth : MonoBehaviour {

	public enum BuildingSize { Lite, Mid, Heavy, Mega }
	public BuildingSize size;
	public int currHealth, maxHealth;
	//private float timeSinceLastAttack;
	
	// Use this for initialization
	void Start () {
		switch (size)
		{
			case BuildingSize.Lite: maxHealth = 250; break;
			case BuildingSize.Mid: maxHealth = 500; break;
			case BuildingSize.Heavy: maxHealth = 750; break;
			case BuildingSize.Mega: maxHealth = 1000; break;
		}
		currHealth = maxHealth;
		//timeSinceLastAttack = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
