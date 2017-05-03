using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TheDAM : Obstacle {
	public bool androidBuild = false;
	public AudioSource musicSource;
	
	//gamecontroller
	private GameObject oneObject;
	private GameControllerGO oneController;
	
	//Psykolog
	private GameObject psykolog;
	
	//
	public ParticleSystem damExplosion;
	public Transform Dam;
	
	public Texture2D black;
	
	public Texture2D WinScreen;
	
	public Texture2D FailScreen;
	
	private float Opaq = 0.0f;
	private Texture2D currentScreen;
	
	public float winSpeed;
	public float registeredSpeed;
	
	private bool endScreen = false;

    private Vector3 startPos;



    [ContextMenu ("Find Beavers")]
    void findBeavers () {
       

    }


	// Use this for initialization
	void Start () {
	


	//	GameControllerGO oneObject = GetComponent<GameControllerGO>();
		
        startPos = transform.localPosition;

		oneObject = GameObject.Find("GameController");
		oneController = oneObject.GetComponent<GameControllerGO>();
		
	}
	

    void LoadRandomLevel () {
        endScreen = false;
        oneController.oneController.NewLevel();
          
    }

	void Update () {

		
	}
	
	
	public override void CollidedWithPlayer (Collider collision)
	{
		
		registeredSpeed = GameObject.FindWithTag("Player").GetComponent<controller>().speedForward;
		oneController.gameFinished = true;
		StartCoroutine(EndScene ());
		
		
	}
	
	IEnumerator EndScene ()
	{
		

        //Application.(Application.loadedLevelName);
		
		if(registeredSpeed > winSpeed){
			
			Dam.position = new Vector3(0,-50,0);
			damExplosion.enableEmission = true;
			yield return new WaitForSeconds(1.5f);
			damExplosion.enableEmission = false;
		}

        if (registeredSpeed > winSpeed){
            currentScreen = WinScreen;
        } else {
            currentScreen = FailScreen;
        }
		
		while(Opaq<1){
			
			Opaq=Opaq+(Time.deltaTime);
			musicSource.volume=1f-Opaq;
			yield return new WaitForEndOfFrame();
		}

		
		endScreen = true;
		
        while (!Input.anyKeyDown)
        {
            yield return null;
        }
        if (currentScreen == FailScreen)
        {

            Opaq = 0f;
            musicSource.volume = 1f;
            GameObject.FindWithTag("menuCam").GetComponent<Camera>().enabled = true;

            yield return null;
            while (!Input.anyKeyDown)
            {
                yield return null;// WaitForSeconds(15.0f);
            }
            currentScreen = null;
            GameObject.FindWithTag("menuCam").GetComponent<Camera>().enabled = false;

        }

        currentScreen = null;
        Opaq = 0f;

        Beaver[] beavers = transform.parent.GetComponentsInChildren<Beaver>();
       
        foreach (Beaver b in beavers)
        {
           
            b.Spawn();
        }

        LoadRandomLevel();


        transform.localPosition = startPos;

		//Application.LoadLevel("THEOPENING");
		
	}
	
	// Update is called once per frame
	void OnGUI () {
		
		
		Color col = GUI.color;
		col. a =  Opaq;
		
		GUI.color = col;
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), black, ScaleMode.StretchToFill, true);
		if(currentScreen != null)
			GUI.DrawTexture(new Rect(0,65,Screen.width,Screen.height-130), currentScreen, ScaleMode.ScaleToFit, false, 1.33f);
		
		//frakobl kamera fra player (How tf?)
		
		//start LOGparticle
		//fade to black
		//fjern DAM
		
		
		
	}
}
