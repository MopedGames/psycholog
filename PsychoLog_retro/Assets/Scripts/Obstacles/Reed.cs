using UnityEngine;
using System.Collections;

public class Reed : Obstacle {
	private string[] reedSounds = new string[3] { "bush_1", "bush_2", "bush_3" };
	
    public virtual void CollidedWithBeaver (Collider collision) {

    }

	public override void CollidedWithPlayer (Collider collision)
	{
		LevelHandler.audioBank.PlayClip(reedSounds);
	}

}
