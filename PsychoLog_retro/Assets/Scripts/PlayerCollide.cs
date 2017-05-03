using UnityEngine;
using System.Collections;

public class PlayerCollide : MonoBehaviour {
	public float sidesPush = 2;
	
	void OnTriggerEnter (Collider collider){
		if(collider.tag == "World"){
//			Vector3 otherForward = LevelHandler.psychoLogProgress.transform.TransformDirection(Vector3.forward);
//			Vector3 lp = transform.localPosition;
//			float sp = otherForward.x * lp.x + otherForward.y * lp.y + otherForward.z * lp.z;
//			float angle = Mathf.Rad2Deg * (sp/(Mathf.Pow(otherForward.magnitude, 2) * Mathf.Pow(lp.magnitude, 2)));
//			Debug.Log(angle + " " + lp + " compared to " + otherForward);
			
			LevelHandler.playerController.ChangeSpeed(0.8f, false);
			LevelHandler.audioBank.PlayClip("hit_quick");
			if(LevelHandler.playerController.speedForward < 0.5f)
				LevelHandler.gameControllerGO.LoseGame();
			
			Vector3 localRight = transform.TransformDirection(Vector3.right);
				
			if (transform.localPosition.x < 0){//if we hit the left side
//				rigidbody.AddRelativeForce(Vector3.right * 10,ForceMode.Force);
//				Debug.Log(transform.localPosition.x + " left");
				GetComponent<Rigidbody>().velocity = localRight * sidesPush;
			}
			else if (transform.localPosition.x > 0){//if we hit the right side
//				rigidbody.AddRelativeForce(-Vector3.right * 10,ForceMode.Force);
//				Debug.Log(transform.localPosition.x + " right");
				GetComponent<Rigidbody>().velocity = -localRight * sidesPush;
			}
			else {
				Debug.LogWarning("We hit the world while being in the middle of the river");
			}
		}
	}
}
