using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//klasse til afspilning af alle lyde

public class AudioBank : MonoBehaviour {
	
	public List<AudioClip> audioClips = new List<AudioClip>();
	
	public void PlayClip(string clipName) {
		AudioClip clip = null;
		
		foreach (AudioClip ac in audioClips) {
			if (ac.name == clipName) {
				clip = ac;
				break;
			}
		}
		
		if (clip != null) {
            GetComponent<AudioSource>().PlayOneShot(clip);

		} else {
			Debug.LogWarning("Could not find AudioClip " + clipName + " on " + gameObject);
		}
	}
	
	public void PlayClip(string[] clipname){
		AudioClip clip = null;
		if(clipname == null)
			Debug.LogError("Clipnames array not found");
		
		int clipNr;
		if(clipname.Length > 0){
			clipNr = Random.Range(0,clipname.Length);
		}
		else{
			Debug.LogWarning("No clipnames sent to PlayClip");
			return;
		}
		
		foreach (AudioClip ac in audioClips) {
			if (ac.name == clipname[clipNr]) {
				clip = ac;
				break;
			}
		}
		
		if (clip != null) {
			GetComponent<AudioSource>().PlayOneShot(clip);
		} else {
			Debug.LogWarning("Could not find AudioClip " + clipname + " on " + gameObject);
		}
		
	}
	
	public void PlayClip(string[] clipname, out int randomClipNr){
		AudioClip clip = null;
		if(clipname == null)
			Debug.LogError("Clipnames array not found");
		
		int clipNr;
		if(clipname.Length > 0){
			clipNr = Random.Range(0,clipname.Length);
		}
		else{
			Debug.LogWarning("No clipnames sent to PlayClip");
			randomClipNr = -1;
			return;
		}
		
		foreach (AudioClip ac in audioClips) {
			if (ac.name == clipname[clipNr]) {
				clip = ac;
				break;
			}
		}
		
		if (clip != null) {
			GetComponent<AudioSource>().PlayOneShot(clip);
			randomClipNr = clipNr;
		} else {
			randomClipNr = -1;
			Debug.LogWarning("Could not find AudioClip " + clipname + " on " + gameObject);
		}
		
	}
}
