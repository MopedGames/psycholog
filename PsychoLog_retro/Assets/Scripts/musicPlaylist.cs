using UnityEngine;
using System.Collections;

public class musicPlaylist : MonoBehaviour {

    public AudioSource[] musicList;
    private float timer = 0f;


	// Use this for initialization
	void StartNextSong () {
        int newSongNumber = Random.Range(0, musicList.Length);
        AudioSource asource = musicList[newSongNumber];
        asource.Play();
        timer = asource.clip.length;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            StartNextSong();

        }
	}
}
