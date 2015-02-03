using UnityEngine;
using System.Collections;

public class PriorityAudio : MonoBehaviour {

	public AudioClip buildingBreakingClip;
	private AudioClip[] infectClips;
	private AudioClip[] scratchClips;
	public AudioClip y1, y2, y3, y4;
	public AudioClip s1, s2, s3, s4, s5, s6;
	public AudioClip winClip, loseClip;
	public AudioSource[] allsrc;
	
	// Use this for initialization
	void Start () {
		scratchClips = new AudioClip[]{s1, s2, s3, s4, s5, s6};
		infectClips = new AudioClip[]{y1, y2, y3, y4};
		allsrc = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
	}
	
	public void PlayInfected() {
		int num = (int) Random.value*infectClips.Length;
		if (infectClips[num].isReadyToPlay)
		{
			foreach (AudioSource src in allsrc) src.Stop();
			AudioSource.PlayClipAtPoint(infectClips[num], transform.position);
		}
	}
	
	
	public void PlayScratch() {
		int num = (int) Random.value*scratchClips.Length;
		if (scratchClips[num].isReadyToPlay)
		{
			foreach (AudioSource src in allsrc) src.Stop();
			AudioSource.PlayClipAtPoint(scratchClips[num], transform.position);
		}
	}
	
	public void PlayBuildingBreaking () {
		if (buildingBreakingClip.isReadyToPlay) 
		{
			foreach (AudioSource src in allsrc) src.Stop();
			AudioSource.PlayClipAtPoint(buildingBreakingClip, transform.position);
		}
	}
	public void PlayWin () {
		if (winClip.isReadyToPlay) 
		{
			foreach (AudioSource src in allsrc) src.Stop();
			AudioSource.PlayClipAtPoint(winClip, transform.position);
		}
	}
	public void PlayLose () {
		if (loseClip.isReadyToPlay) 
		{
			foreach (AudioSource src in allsrc) src.Stop();
			AudioSource.PlayClipAtPoint(loseClip, transform.position);
		}
	}
}
