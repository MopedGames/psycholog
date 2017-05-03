#pragma strict
var androidBuild : boolean;
var androidButton : Collider;
var highScore : int;
var scoreText : GUIText;
var shadowText : GUIText;
function Awake () {
	
	if(PlayerPrefs.HasKey("PsyLogAcadeScore")){
		// new score is higher than the stored score
		highScore = PlayerPrefs.GetInt("PsyLogAcadeScore");
	} else {
		highScore = 0;
	}
	if(scoreText != null){
	    scoreText.text = ("Highscore: " + highScore.ToString());
	    shadowText.text = scoreText.text;
	}
}

function Update () {
	
	if (SystemInfo.deviceType == DeviceType.Handheld && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
		
		var ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
		var hit : RaycastHit;
		if (Physics.Raycast (ray,hit)) {
			if(hit.collider == androidButton){
				Application.LoadLevel("THELEVEL");
			}
			
		}

		
	} else if (SystemInfo.deviceType == DeviceType.Desktop && Input.anyKeyDown) {
		Application.LoadLevel("THELEVEL");
	}
}

