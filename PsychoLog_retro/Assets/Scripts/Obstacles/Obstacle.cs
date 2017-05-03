using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	
	public float regainAmount = 100; //How fast the player will regain speed after the collision is done
	
	public float slowAmount; //How much to slow the player in percent
	
	void OnTriggerEnter (Collider collision){
		if(collision.gameObject.tag == "Player"){
			LevelHandler.playerController.ChangeSpeed(1 - (slowAmount/100), false);
			CollidedWithPlayer(collision);
			
		} else if(collision.gameObject.GetType() == typeof(Obstacle)){
			CollidedWithObstacle(collision);
        } else if(collision.gameObject.tag == "Beaver") {
            CollidedWithBeaver(collision);

        }
	}
	
		
	void OnTriggerExit (Collider collision)
	{
		if(collision.gameObject.tag == "Player" && LevelHandler.playerController.speedForward < regainAmount){
			LevelHandler.playerController.SetSpeed(regainAmount);
			
		}
	}
	
	public virtual void CollidedWithPlayer (Collider collision) {}
	
	public virtual void CollidedWithObstacle (Collider collision) {}

    public virtual void CollidedWithBeaver (Collider collision) {}
}
