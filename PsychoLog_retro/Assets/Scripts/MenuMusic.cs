using UnityEngine;
using System.Collections;

public class MenuMusic : MonoBehaviour {
	
	public AudioBank audioBank;
	
	// Use this for initialization
	void Start () {
		
		LevelHandler.Init();
		StartCoroutine(PlayRandomAmbience());
		
	
	}

	
	private string[] ambientSound = new string[6] { "birds", "crow", "frog", "bees", "frog_2", "insects" };
	
	IEnumerator PlayRandomAmbience () {
		while(true){
			int i;
			yield return new WaitForSeconds(Random.Range(0f,8f));
			audioBank.PlayClip(ambientSound, out i);
			yield return new WaitForSeconds(audioBank.audioClips[i].length);
		}
	}
	
}
