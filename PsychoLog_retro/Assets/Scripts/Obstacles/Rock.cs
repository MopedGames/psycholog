using UnityEngine;
using System.Collections;

public class Rock : Obstacle {
	
	private bool hit = false;
	
   void OnTriggerEnter (Collider collision){
        if(collision.gameObject.tag == "Player"){
            LevelHandler.playerController.ChangeSpeed(1 - (slowAmount/100), true);
            //LevelHandler.playerController.BounceBack();//(1 - (slowAmount/100));
            CollidedWithPlayer(collision);

        } else if(collision.gameObject.GetType() == typeof(Obstacle)){
            CollidedWithObstacle(collision);
        } else if(collision.gameObject.tag == "Beaver") {
            Beaver beaver = collision.GetComponent<Beaver>();
            if (!beaver.dead)
            {
                beaver.Jump();
            }
        }
    }
	
	public override void CollidedWithPlayer (Collider collision)
	{
		if (!hit){
			LevelHandler.audioBank.PlayClip(new string[2] { "hit_quick", "hit_stone" }); 
		}
		
		hit = true;
	}
	
    public virtual void CollidedWithBeaver (Collider collision) {

        collision.GetComponent<Beaver>().Jump();

    }

	public void OnTriggerStay (Collider collision)
	{
		if(collision.gameObject.tag == "Player")
			LevelHandler.playerController.ChangeSpeed(1 - (slowAmount/100), true);
		
	}
	


}
