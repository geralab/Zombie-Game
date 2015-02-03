using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
    public AudioClip[] theSongs;
	public int songNumber;
	private double timeToNextSong;
	public float songVolume;
	// Use this for initialization
	void Start () {
	
	
		
      
		
	}
	
	// Update is called once per frame
	void Update () {
	    
		if(songNumber < theSongs.Length){
			
			if(!audio.isPlaying){
				audio.clip = theSongs[songNumber];
				audio.volume= songVolume;
				audio.Play();
				songNumber++;
				
			}
			
		}else{
			songNumber = 0;
		}
	}
	}

