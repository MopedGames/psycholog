using UnityEngine;
using System.Collections;

public class GameControllerGO : MonoBehaviour {
	
	public bool menu = false;
	public GUIStyle style;
	public GUIStyle style25;
	public int score;
	public int kills;
	public float timah = 0.0f;
	public bool gameFinished = false;
	
	public Texture2D beaverImg;
	
	//gamecontroller
	private GameObject oneObject;
    public controller oneController;
	public PutOnVector putOnVector;
	public string[] levelsegments;

	// Use this for initialization
	void Start () {
	
		oneObject = GameObject.FindWithTag("Player");
		oneController = oneObject.GetComponent<controller>();
        putOnVector = oneObject.GetComponentInParent<PutOnVector>();
		LevelHandler.Init();
		StartCoroutine(PlayRandomAmbience());
		
	
	}
	
	void Update(){
		
		//Debug.Log ("FPS: " + (1/Time.deltaTime));
		
		if (gameFinished == false){
		timah = timah+Time.deltaTime;
		
		} else {
			
			//oneController.acceleration = 0.0f;
			//oneController.speedForward = 0.0f;	
		}
		
	}
		
	
	void OnGUI (){
		
		int scaleTime = (int)(timah*100);
		float showTime = (float)scaleTime/100;
		
		if(gameFinished){
			
			
			GUI.color =  Color.black;
			GUI.Label(new Rect(12f,12f,Screen.width,30f), ("Score: " + score.ToString()),style25);
			GUI.Label(new Rect(12f,32f,Screen.width,30f), ("Time: " + showTime.ToString()),style25); 
			
			//GUI.Label(new Rect(30f,40f,Screen.width,25f), LevelHandler.playerController.speedForward.ToString());
			
			GUI.color = Color.white;
			GUI.Label(new Rect(10f,10f,Screen.width,30f), ("Score: " + score.ToString()),style25);
			GUI.Label(new Rect(10f,30f,Screen.width,30f), ("Time: " + showTime.ToString()),style25);
		}else{
			GUI.color =  Color.black;
			GUI.Label(new Rect(12f,12f,Screen.width,55f), ("Score: " + score.ToString()),style);
			GUI.Label(new Rect(12f,62f,Screen.width,55f), ("Time: " + showTime.ToString()),style); 
			
			//GUI.Label(new Rect(30f,40f,Screen.width,25f), LevelHandler.playerController.speedForward.ToString());
			
			GUI.color = Color.white;
			GUI.Label(new Rect(10f,10f,Screen.width,55f), ("Score: " + score.ToString()),style);
			GUI.Label(new Rect(10f,60f,Screen.width,55f), ("Time: " + showTime.ToString()),style);
			
			//Draw beaverkills
			//int i = 0;
			/*for (i= 0; i<kills; i++){
				Debug.Log ("drew " + (i+1) + " beaver");
				GUI.DrawTexture(new Rect(Screen.width-(i*50)-55,Screen.height-65,50,50),beaverImg,ScaleMode.ScaleToFit,true,1.0f);
				
			}*/
		}
	}
	
	public void LoseGame (){
		
		
	}
	
	private string[] ambientSound = new string[6] { "birds", "crow", "frog", "bees", "frog_2", "insects" };
	
	IEnumerator PlayRandomAmbience () {
		while(true){
			int i;
			yield return new WaitForSeconds(Random.Range(0f,8f));
			LevelHandler.audioBank.PlayClip(ambientSound, out i);
			yield return new WaitForSeconds(LevelHandler.audioBank.audioClips[i].length);
		}
	}
	
}
