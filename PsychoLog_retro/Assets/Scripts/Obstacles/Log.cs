using UnityEngine;
using System.Collections;

public class Log : Obstacle {
	
	public float moveSpeed;
	private string[] woodSounds = new string[5] { "hit_wood", "hit_wood_slight", "wood_clock", "wood_hardhit", "wood_slighthit"};
	private string[] reedSounds = new string[3] { "bush_1", "bush_2", "bush_3" };

	void Start () {
		(transform.parent.GetComponent(typeof(PutOnVector)) as PutOnVector).speed = moveSpeed;
		
	}
	
	public override void CollidedWithObstacle (Collider collision)
	{
		moveSpeed *= 1 - (collision.gameObject.GetComponent<Obstacle>().slowAmount / 100f);
		(transform.parent.GetComponent(typeof(PutOnVector)) as PutOnVector).speed = moveSpeed;
		
		float distToPl = (transform.position - LevelHandler.psychoLog.transform.position).magnitude;
		if(distToPl < 5){
			if(collision.gameObject.GetType() == typeof(Log)){
				LevelHandler.audioBank.PlayClip(woodSounds);
				
			}
			else if(collision.gameObject.GetType() == typeof(Rock)) {
				LevelHandler.audioBank.PlayClip("hit_quick");
			}
			else if(collision.gameObject.GetType() == typeof(Reed)) {
				LevelHandler.audioBank.PlayClip(reedSounds);
			}
		}
	}
	
	public override void CollidedWithPlayer (Collider collision)
	{
		Debug.Log("Psycho-log has turned on his brothers! (hit another log)");
		
		LevelHandler.audioBank.PlayClip(woodSounds);
		GetComponent<Animation>().Play(GetComponent<Animation>().clip.name);
		//yield return new WaitForSeconds(animation.clip.length);
	}
}
