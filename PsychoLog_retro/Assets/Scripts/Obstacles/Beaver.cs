using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Beaver : Obstacle {
	
    public string level;

	public float moveSpeed;
	public ParticleSystem blood;
	private bool hunting;
	public Animation beaverAni;
    public bool dead = false;
	public GameObject visibleBeaver;
	public string animationToPlay = ("Swimming");

    private EffectManager effect;



    public void Spawn () {

        if (level == GameObject.FindGameObjectWithTag("PsykoProgress").GetComponent<PutOnVector>().pathName)
        {

            dead = false;
            GetComponentInParent<PutOnVector>().speed = moveSpeed;
            GetComponentInParent<PutOnVector>().distance = GetComponentInParent<PutOnVector>().startPercent;
            visibleBeaver.GetComponent<Renderer>().enabled = true;
            GetComponentInParent<PutOnVector>().enabled = true;
        }
        else
        {
            GetComponentInParent<PutOnVector>().enabled = false;
        }
    }

    [@ContextMenu ("Set Level")]
    void setLevel () {
        level = GetComponentInParent<iTweenPath>().pathName;
    }

	void Start () {
        
		StartCoroutine(CheckPlayerState());
		beaverAni.Play(animationToPlay);

		blood.Stop ();
        
        Spawn();

        //effect = LevelHandler.psychoLog.GetComponent<EffectManager>();
	}
	
	private Vector3 refVelocity = Vector3.zero;
	private float cooldown = 0;
	private float _cooldown = 0;
	private float _targetX;
	
	void Update (){	//Beaver AI, makes beaver intercept a slow psycholog
        if(!dead && level == LevelHandler.psychoLogProgress.GetComponent<PutOnVector>().pathName){
			if(hunting){
				float distToPl = (transform.position - LevelHandler.psychoLog.transform.position).magnitude;
				if(LevelHandler.playerController.speedForward > 0.8 || distToPl > 10){
					hunting = false;
					refVelocity = Vector3.zero;
					cooldown = -1;
					StartCoroutine(CheckPlayerState());
					Vector3 target =  new Vector3(-LevelHandler.psychoLog.transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
					transform.localPosition = Vector3.SmoothDamp(transform.localPosition, target, ref refVelocity, 5f);
				}
				else{
					Vector3 target =  new Vector3(LevelHandler.psychoLog.transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
					transform.localPosition = Vector3.SmoothDamp(transform.localPosition, target, ref refVelocity, 3f);
				}
			}
			else {
				if(cooldown > 0){
					cooldown -= Time.deltaTime;
					Vector3 target =  new Vector3(_targetX, transform.localPosition.y, transform.localPosition.z);
					transform.localPosition = Vector3.SmoothDamp(transform.localPosition, target, ref refVelocity, _cooldown);
				}
				else{
					cooldown = Random.Range(1f,3f);
					_cooldown = cooldown;
					_targetX = Random.Range(-0.6f,0.6f);
					refVelocity = Vector3.zero;
					Vector3 target =  new Vector3(_targetX, transform.localPosition.y, transform.localPosition.z);
					transform.localPosition = Vector3.SmoothDamp(transform.localPosition, target, ref refVelocity, _cooldown);
				}
			}
		}
	}
	
    public void Jump(){
        if (beaverAni.clip.name != "jump")
        {
            StartCoroutine(Jumping());
        }
    }
        
    IEnumerator Jumping (){
        beaverAni.Play("jump");

        yield return new WaitForSeconds(2f);

        beaverAni.Play(animationToPlay);
    }

	public override void CollidedWithPlayer (Collider collision)
	{
		if(!dead){
			Debug.Log("hit a beaver");

			float plSpeed = LevelHandler.playerController.speedForward;
			if(plSpeed > 0.8){
				float score = (plSpeed - moveSpeed) * 2f;
				if(plSpeed > 399){
					LevelHandler.audioBank.PlayClip("hit_muchblood");
                    LevelHandler.psychoLog.GetComponent<EffectManager>().Bam(true);

                }
				else{
					LevelHandler.audioBank.PlayClip(new string[5] { "hit_1", "HIT_AWESOME", "hit_quick", "hit_smask", "hit_smask2" });
                    LevelHandler.psychoLog.GetComponent<EffectManager>().Bam(true);
                  
				}
				
				Die(score);
			}
			else {
				LevelHandler.gameControllerGO.LoseGame();
			}
		}
	}
	
	public void Die (float addScore){
		
		
		LevelHandler.gameControllerGO.score += Mathf.CeilToInt(addScore);
		LevelHandler.gameControllerGO.kills++;
		if(LevelHandler.playerController.bloodLevel == LevelHandler.playerController.psychoLogTextures.Length - 1)
			LevelHandler.playerController.RefreshBlood();
		else if(LevelHandler.playerController.bloodLevel < LevelHandler.playerController.psychoLogTextures.Length -1)
			LevelHandler.playerController.IncrementBlood();
		else
			Debug.LogWarning("The Blood has been corrupted. Please Check Blood Level");
			
		dead = true;
		StartCoroutine(DyingCoroutine());
		
	}
	
	IEnumerator DyingCoroutine (){
		Debug.Log("A Beaver is dying");
        int leftRight = Random.Range(-1, 3);
        beaverAni.Play("getHit");

		blood.Play();
        float t = 0f;

        GetComponentInParent<PutOnVector>().speed = LevelHandler.playerController.speedForward * 1.05f;
        while (t < 2)
        {

            transform.localPosition += ((new Vector3(1, 0, 0) * Time.deltaTime * 0.5f)-(new Vector3(1, 0, 0) * Time.deltaTime * leftRight))*50f;

            t += Time.deltaTime;
            yield return null;
		
        }
		visibleBeaver.GetComponent<Renderer>().enabled = false;
        GetComponentInParent<PutOnVector>().speed = 0;

		yield break;
	}
	
	IEnumerator CheckPlayerState () {
		yield return null;
		float plSpeed = LevelHandler.playerController.speedForward;
		while(!hunting && !dead){
			if(plSpeed < 0.8f){
				float distToPl = (transform.position - LevelHandler.psychoLog.transform.position).magnitude;
				if(distToPl < 10){
					hunting = true;
					yield break;
				}
			}
			yield return new WaitForSeconds(1);
		}
	}
	
}
