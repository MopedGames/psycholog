using UnityEngine;
using System.Collections;

public static class LevelHandler{

	public static GameControllerGO gameControllerGO;
	public static GameObject psychoLog;
	public static GameObject psychoLogProgress;
	public static controller playerController;
	public static GameObject mainCamera;
	public static AudioBank audioBank;

	public static void Init (){


		gameControllerGO = GameObject.FindObjectOfType(typeof(GameControllerGO)) as GameControllerGO;
        if (gameControllerGO != null)
        {
            gameControllerGO.score = 0;
        }
		/*if(!gameControllerGO && Application.loadedLevel !=0){
			Debug.LogError("Add a gamecontroller to the level pls");
			return;
		}*/
		//if(Application.loadedLevel !=0){
		psychoLog = GameObject.FindGameObjectWithTag("Player");
        psychoLogProgress = GameObject.FindGameObjectWithTag("PsykoProgress");
        if(psychoLog != null){
			
			//if(!psychoLog || !psychoLogProgress)
			//	Debug.LogError("Could not locate player or playerprogress");
			playerController = psychoLog.GetComponent(typeof(controller)) as controller;
			
			
			
		}
		
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		audioBank = mainCamera.GetComponentInChildren<AudioBank>();
	}
}
