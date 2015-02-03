using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HumanoidHandler : MonoBehaviour 
{
	public static List<Transform> ListOfHumans = new List<Transform>();
	public static List<Transform> ListOfZombies = new List<Transform>();
	
	public Transform objectBeingDamaged;
	public Transform emptyTarget;
	private int myListPos;
	
	/// <summary>
	/// Character attack using state--User defined, type of attack being used
	/// </summary>
	public enum StateCharacterAtkUsing { WeakAttack, MediumAttack, StrongAttack }
	
	
	/// <summary>
	/// Character attack behave state--User defined, stand until attacked or attack nearest
	/// </summary>
	public enum StateCharacterBehave { StandAndRun, Stand, AtkNearest }
	
	/// <summary>
	/// Character state--Zombie or Human.
	/// </summary>
	public enum StateCharacterType { Zombie, Human }
	
	/// <summary>
	/// Zombie type--The type the character is or will turn into if infected.
	/// </summary>
	public enum StateZombieType { Sneeky, Strong, Fast }
	/// <summary>
	/// Animation state Standing->standing, Walking->walking or shambling, 
	///           Running->running, Attacking-> hitting or shooting
	/// 
	/// NOTE: Standing, Walking, Running, and Attacking add HoldGun to respective animation
	/// </summary>
	public enum StateAnim { Standing, Walking, Running, Attacking }
		
	public static Texture2D FastTex;
	public static Texture2D SneakyTex;
	public static Texture2D ToughTex;
	public static Texture2D HumanTex;
	public static Texture2D MilitaryTex;
	
	/// State enum variables--The enum variables representing current states.
	
	public StateCharacterAtkUsing CurrUsingAttack;
	public StateCharacterBehave CurrBehavior;
	public StateCharacterType CurrCharacterType;
	public StateZombieType CurrZombType;
	public StateAnim CurrAnimation;
	
	public bool isSelected; //By the player, needed in AIPather.cs so all zombies aren't moved at once.
	public bool isFleeing; //For humans to run away to the nearest construct.
	public bool isInfecting;
	
	/// <summary>
	/// is targeting -> either targeting a walkToPoint or an enemy
	/// </summary>
	public bool isTargeting;
	public bool isArmed;
	public int maxHealth;
	public int currHealth;
	public int maxResistance;
	public int currResistance;
	public int baseAttackDamage;
	public float timeSinceLastAttack;
	public float speedPerSec;
	public Vector3 DirVecToTarget;
	
	public Vector3 Position
	{
		get
		{
			return transform.position;
		}
	}
	
	public Transform Target
	{
		get 
		{
			return transform.GetComponent<AIPath>().target;	
		}
		set
		{
			transform.GetComponent<AIPath>().target = value;	
		}
	}
	
	
	// Use this for initialization
	public void Start() 
	{
		FastTex = (Texture2D)Resources.Load ("ZombieFastTex");
		SneakyTex = (Texture2D)Resources.Load ("ZombieSneakyTex");
		ToughTex = (Texture2D)Resources.Load ("ZombieToughTex");
		HumanTex = (Texture2D)Resources.Load ("HumanTex");
		MilitaryTex = (Texture2D)Resources.Load ("MilitaryTex");
		
		
		CurrUsingAttack = StateCharacterAtkUsing.WeakAttack;
		
		CurrBehavior = StateCharacterBehave.AtkNearest;
		
		CurrAnimation = StateAnim.Standing;
		
		isArmed = false;
		
		maxResistance = 75;
		
		currResistance = maxResistance;
		
		maxHealth = 75;
		baseAttackDamage = 2;
		speedPerSec = 2.5f;
		
		timeSinceLastAttack = 0;
		
		
		if( CurrCharacterType == StateCharacterType.Human )// && HumanoidHandler.ListOfHumans.Count == 0 )
		{
			//Debug.Log ("Entered AddHuman");
			transform.tag = "Human";
			//CurrCharacterType = StateCharacterType.Human;
			transform.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = HumanoidHandler.HumanTex;
			myListPos = ListOfHumans.Count;
			HumanoidHandler.ListOfHumans.Add (transform);
		}
		else
		{
			//Debug.Log ("Entered AddZombie");
			transform.tag = "Zombie";
			//CurrCharacterType = StateCharacterType.Zombie;
			switch (CurrZombType)
			{
			case StateZombieType.Fast: //Just faster speed.
				speedPerSec *= 2;
				maxHealth = 100;
				//transform.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = HumanoidHandler.FastTex;
				break;
			case StateZombieType.Sneeky: //Will need to wait until detection is setup!
				//transform.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = HumanoidHandler.SneakyTex; 
				break;
			case StateZombieType.Strong: //Just more damage.
				baseAttackDamage *= 2;
				//transform.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = HumanoidHandler.ToughTex; 
				break;
			}
			
			
			myListPos = ListOfZombies.Count;
			HumanoidHandler.ListOfZombies.Add(transform);
		}
		emptyTarget = Target;
		currHealth = maxHealth;
		
		TestingFun ();
	}
	
	void TestingFun()
	{
		//isTargeting = true;
		isArmed = false;
			
		if( CurrCharacterType == StateCharacterType.Zombie )
		{
			CurrAnimation = StateAnim.Walking;
		}
		else
		{
			CurrAnimation = StateAnim.Standing;
		}
		
	}// TestingFun()
	
	// Update is called once per frame
	void Update () 
	{//Debug.Log ("Getting type check " + HumanoidHandler.ListOfHumans[0].GetComponent<HumanoidHandler>().CurrCharacterType);
	
		//if( true )//CurrCharacterType == StateCharacterType.Human)
		//{
			if( isTargeting )
			{//Debug.Log ("Entered AnimHuman");
				AnimateHuman();
			}
			else
			{// if the Character moves while standing
			 // isTargeting is not getting set somewhere
				animation.Play ("Standing");
			}
		//}
/*		else
		{Debug.Log ("Entered AnimZombie");
		
			if( isTargeting )
			{
				AnimateZombie();
			}
			else
			{// if the Character moves while standing
			 // isTargeting is not getting set somewhere
				animation.Play ("Standing");
			}
		}
*/		
	}// Update()
	
	void AnimateHuman()
	{//Debug.Log ("Entered animateHuman fun");
		float currSpeed = 0;
		
		if( CurrAnimation == StateAnim.Running )
		{Debug.Log ("Entered Run");
			if( !animation.IsPlaying ("Run2") )
			{
				animation.Play ("Run2");
			}
			transform.GetComponent<AIPath>().speed = 2 * speedPerSec;// set running speed
		}
		else if( CurrAnimation == StateAnim.Attacking )
		{
			if( !isArmed )
			{
				if( !animation.IsPlaying ("Hit") )
				{
					animation.Play ("Hit");
				}
				
			}// attack armed
			else
			{
				// always animate HoldGun when armed (see above)
			}// attack not armed
			
			
			// zero speed -> zero translation
		}
		else if( CurrAnimation == StateAnim.Walking )
		{//Debug.Log ("Entered Walk");
			if( !animation.IsPlaying ("Walk") )
			{
				animation.Play ("Walk");
			}
			transform.GetComponent<AIPath>().speed = speedPerSec;// set to walking speed
		}
		else
		{// if the Character moves while standing
		 // isTargeting is not getting set somewhere
			animation.Play ("Standing");
		}// body animation blocks
		
		if( isArmed )
		{
			animation.Play ("HoldGun");
		}
		
		if( CurrAnimation == StateAnim.Running 
			|| CurrAnimation == StateAnim.Walking )
		{
			TranslateAndRotate ( currSpeed );
		}
		else if( CurrAnimation == StateAnim.Attacking )
		{
			Attack ();
		}
	}// AnimateHuman()
	
	void AnimateZombie()
	{
		
	}// AnimateZombie()
	
	void TranslateAndRotate(float speed)
	{	
		if (this.Target != null)
			DirVecToTarget = this.Target.position -  transform.position;
		
		DirVecToTarget.y = 0;
		//Debug.Log (DirVecToTarget.magnitude);
		if(transform.GetComponent<AIPath>().TargetReached || Vector3.Magnitude ( DirVecToTarget ) < .5f )
		{
			if (this.Target.tag == "Human" || this.Target.tag == "Zombie" || this.Target.tag == "Construct") 
			{
				CurrAnimation = StateAnim.Attacking;
				objectBeingDamaged = this.Target;
				Attack ();
			}
			else CurrAnimation = StateAnim.Standing;
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.transform.tag == "Human" ||
			hit.transform.tag == "Construct") 
			{
			//Debug.Log("Hit");
			CurrAnimation = StateAnim.Attacking;
			objectBeingDamaged = this.Target;
			Attack ();
		}
		else /*if (hit.gameObject.layer == 8)*/ CurrAnimation = StateAnim.Walking;
	}
	
	
	
	void Attack()
	{
		//if( transform != null )
		//{
			timeSinceLastAttack += Time.deltaTime;
			if( timeSinceLastAttack > .5f )
			{
				float atkMult = 0f;
				switch(CurrUsingAttack) {
					case StateCharacterAtkUsing.WeakAttack: atkMult = 1/5f; break;
					case StateCharacterAtkUsing.MediumAttack: atkMult = 1/3.5f; currHealth -= 1; break;
					case StateCharacterAtkUsing.StrongAttack: atkMult = 1/2f; currHealth -= 2; break;	
				}
			
				//This is a really cheesy workaround that happens because we didn't divide the health scripts..
				if (objectBeingDamaged.GetComponent<HumanoidHandler>() != null) 
				{
					Camera.mainCamera.GetComponent<PriorityAudio>().PlayScratch();
					if (!isInfecting) objectBeingDamaged.GetComponent<HumanoidHandler>().currHealth -= (int) ((CurrZombType == StateZombieType.Strong ? 2 : 1) * atkMult * currHealth);
					else objectBeingDamaged.GetComponent<HumanoidHandler>().currResistance -= (int) ((CurrZombType == StateZombieType.Sneeky ? 2 : 1) * atkMult * currHealth);
				}
				if (objectBeingDamaged.GetComponent<ConstructHealth>() != null)
				{
					Camera.mainCamera.GetComponent<PriorityAudio>().PlayBuildingBreaking();
					objectBeingDamaged.GetComponent<ConstructHealth>().currHealth -= (int) ((CurrZombType == StateZombieType.Strong ? 2 : 1) * atkMult * currHealth);
				}
				timeSinceLastAttack = 0f;
					
				//Debug.Log ("Entered Atk" + " health " + objectBeingDamaged.currHealth);
					
			}
				
			if( objectBeingDamaged.GetComponent<HumanoidHandler>() != null)
			{	
				HumanoidHandler hh = objectBeingDamaged.GetComponent<HumanoidHandler>();
				if (hh.currHealth <= 0 )//currHealth <= 0 )
				{
					//Might just want to remove from the list first instead of just killing the renderer?
					if (CurrCharacterType == StateCharacterType.Human) 
					{
					    ListOfHumans.RemoveAt (hh.myListPos);
					}
					else if (CurrCharacterType == StateCharacterType.Zombie) 
					{
					    ListOfZombies.RemoveAt (hh.myListPos);
					}
					Destroy ( objectBeingDamaged.gameObject );
					//Might also want to shove the call to play a dying noise here.
					//transform.GetComponentInChildren <SkinnedMeshRenderer>().enabled = false;
				}
				else if (hh.currResistance <= 0)
				{
					//In case other zombies are on the same target.
					if (hh.CurrCharacterType != StateCharacterType.Human)
					{
						Camera.mainCamera.GetComponent<PriorityAudio>().PlayInfected();
						hh.CurrCharacterType = StateCharacterType.Zombie;	
						ListOfHumans.RemoveAt (hh.myListPos);
						hh.myListPos = ListOfZombies.Count;
						ListOfZombies.Add (objectBeingDamaged);
					}
				}
			//Debug.LogError ("Killed");
			}
			else if (objectBeingDamaged.GetComponent<ConstructHealth>() != null 
			&& objectBeingDamaged.GetComponent<ConstructHealth>().currHealth <= 0)
			{
				Destroy (objectBeingDamaged.gameObject);
			}
		
		//Preserve the pathfinding target.
		if (objectBeingDamaged == null)
		{
				emptyTarget.position = transform.position;
				Target = this.emptyTarget;	
		}
		
	} 
}










