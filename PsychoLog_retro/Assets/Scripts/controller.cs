using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class controller : MonoBehaviour {
	
    public KeyCode controllerInput;

	public bool androidBuild;
	
	public float floodwidth = 2.0f;
	public float speedForward = 2.0f;
	public float sideAcceleration = 0.02f;
	public float androidFactor = 0.5f;
	
	public float maxSpeed = 50;
	public float normalSpeed = 10;
	public float fastSpeed = 20;
	public float acceleration = 5;
	[System.NonSerialized]
	public int speedUp = 0;
	
	
	private byte rotate = 0;
	private GameObject treeLog;
	private GameObject leftEmission;
	private GameObject rightEmission;
	private ParticleSystem leftPS;
	private ParticleSystem rightPS;
	public int bloodLevel = 0;
	public Texture2D[] psychoLogTextures;
	public GameObject visibleLog;
	
	
	private float speedSides = 0.0f;
	private Vector3 position_PsychoLog;
	
	private Vector3 currentGyroRotation;
	private int buttonWidth = Screen.width/4;

    public string[] levels; 

	void Start () {
		(
		transform.parent.GetComponent(typeof(PutOnVector)) as PutOnVector).speed = speedForward;
		buttonWidth = Screen.width/4;
		leftEmission = GameObject.FindGameObjectWithTag("TurnLeft");
		rightEmission = GameObject.FindGameObjectWithTag("TurnRight");
		leftPS = leftEmission.GetComponent(typeof (ParticleSystem)) as ParticleSystem;
		rightPS = rightEmission.GetComponent(typeof (ParticleSystem)) as ParticleSystem;
	}
	
	private float refVelocity;
	private bool speedingUp = false;
    bool bouncing = false;


    public void NewLevel(){



        int chosenLevel = Random.Range(0, levels.Length);
        speedSides = 0f;
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);

        LevelHandler.gameControllerGO.putOnVector.NewPath(levels[chosenLevel]);

    }

	// Update is called once per frame
	void Update () {
		
       

		if(speedUp > 0){
			if(!speedingUp){
				refVelocity = 0;
			}
			speedingUp = true;
			SetSpeed(Mathf.SmoothDamp(speedForward, fastSpeed,ref refVelocity, (fastSpeed - speedForward)/acceleration));
		}
        else if (speedUp == 0 && !bouncing) {
			if(speedingUp){
				refVelocity = 0;
			}
			speedingUp = false;
            SetSpeed(Mathf.SmoothDamp(speedForward, normalSpeed, ref refVelocity, (normalSpeed - speedForward) / acceleration));
        }
        else if (speedUp == 0 && bouncing) {
            if(speedingUp){
                refVelocity = 0;
            }
            speedingUp = false;
            SetSpeed(Mathf.SmoothDamp(speedForward, normalSpeed, ref refVelocity, (normalSpeed - speedForward) / acceleration*6));
        }

		else
        {
			
		}
	
	
	}
	
	void FixedUpdate () {
		if (!androidBuild) {
			speedSides = Input.GetAxis("Horizontal") * sideAcceleration;
		} else if (androidBuild) {
			currentGyroRotation.y = currentGyroRotation.y + Input.acceleration.y;
			if (currentGyroRotation.sqrMagnitude > 1)
            	currentGyroRotation.Normalize();
			speedSides = currentGyroRotation.y * androidFactor * sideAcceleration * (-1);
		}
		
		
		GetComponent<Rigidbody>().AddRelativeForce(new Vector3(speedSides, 0.0f, 0.0f));
		Vector3 localForward = transform.TransformDirection(Vector3.forward);
		float scalarProd = ((localForward.x * GetComponent<Rigidbody>().velocity.x) + (localForward.y * GetComponent<Rigidbody>().velocity.y) + (localForward.z * GetComponent<Rigidbody>().velocity.z));
		
		
		treeLog = GameObject.FindGameObjectWithTag("Psykologger");
		
		if (speedSides > 0)
		{
			treeLog.transform.Rotate(Vector3.back * Time.deltaTime * speedSides*30); 
			leftPS.enableEmission = false;
			rightPS.enableEmission = true;
		}
		else if (speedSides < 0)
		{
			treeLog.transform.Rotate(Vector3.forward * Time.deltaTime * Mathf.Abs(speedSides*30));
			leftPS.enableEmission = true;
			rightPS.enableEmission = false;
		}
		else
		{
			leftPS.enableEmission = false;
			rightPS.enableEmission = false;
		}
		
		

		
		Vector3 localForwardVelocity = (scalarProd / Mathf.Pow(localForward.magnitude,2)) * localForward;
		GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity - localForwardVelocity;
	}
	
    IEnumerator Bounce(){
        bouncing = true;
        acceleration = acceleration;

        while (speedForward < 0){
           

            //SetSpeed(speedForward+(Time.deltaTime*1));
            yield return new WaitForEndOfFrame();
            speedForward += acceleration * Time.deltaTime;
        }
        acceleration = acceleration;
        bouncing = false;
    }

    public void ChangeSpeed (float newSpeedFactor, bool bounce)
    {
        //Debug.Log("ChangeSpeed was called");
        if (bounce)
        {
            speedForward = -25f;
        }
        else
        {
            speedForward = Mathf.Clamp(speedForward * newSpeedFactor, -200, maxSpeed);
        }
        if(speedForward < 0.0f && !bouncing){
            StartCoroutine("Bounce");
        }
        //Debug.Log(LevelHandler.psychoLogProgress == null);
        (LevelHandler.psychoLogProgress.GetComponent(typeof(PutOnVector)) as PutOnVector).speed = speedForward;
        refVelocity = 0;
    }

    public void SetSpeed (float newSpeed) 
    {

        speedForward = Mathf.Clamp(newSpeed, -200, maxSpeed);
        if(speedForward < 0.0f && !bouncing){
            StartCoroutine("Bounce");
        }
        //(LevelHandler.psychoLogProgress.GetComponent(typeof(PutOnVector)) as PutOnVector).speed = speedForward;

        GetComponentInParent<PutOnVector>().speed = speedForward;
    }
		
	public void SetBloodLevel (int newBlood) {
		StopCoroutine("RemoveBlood");
		bloodLevel = Mathf.Clamp(newBlood, 0, psychoLogTextures.Length);
		visibleLog.GetComponent<Renderer>().material.SetTexture("_MainTex",psychoLogTextures[bloodLevel]);
		StartCoroutine("RemoveBlood");
	}
	
	public void IncrementBlood () {
		StopCoroutine("RemoveBlood");
		bloodLevel++;
		if(bloodLevel > psychoLogTextures.Length - 1){
			Debug.LogWarning("You fucked up the blood level");
			bloodLevel--;
			return;
		}
		
		Debug.Log("Bloodlevel " + bloodLevel);
		visibleLog.GetComponent<Renderer>().material.SetTexture("_MainTex",psychoLogTextures[bloodLevel]);
		StartCoroutine("RemoveBlood");
		
	}
	
	public void RefreshBlood () {
		StopCoroutine("RemoveBlood");
		StartCoroutine("RemoveBlood");
	} 
	
	IEnumerator RemoveBlood () {
		while(bloodLevel > 0){
			yield return new WaitForSeconds(5);
			
			bloodLevel--;
			
			visibleLog.GetComponent<Renderer>().material.SetTexture("_MainTex",psychoLogTextures[bloodLevel]);
			Debug.Log("Removing; Bloodlevel " + bloodLevel + " and the time is " + Time.time);
		}
		yield break;
	}
}
//turnparticles under prefab