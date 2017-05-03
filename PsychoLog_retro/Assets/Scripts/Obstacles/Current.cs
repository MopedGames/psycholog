using UnityEngine;
using System.Collections;

public class Current : Obstacle {
	
	private string[] speedBoost = new string[5] { "speedboost", "speedboost2", "speedboost3", "speedboost4", "speedboost6" };

	public override void CollidedWithPlayer (Collider collision)
	{
		Debug.Log("Speedy log!");
		LevelHandler.audioBank.PlayClip(speedBoost);
		LevelHandler.playerController.speedUp++;
	}
	
	void OnTriggerExit (Collider collision){
		if(collision.tag == "Player"){
			LevelHandler.playerController.speedUp--;
		}
		
	}
}
