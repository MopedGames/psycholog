#pragma strict
var player : boolean = false;
var range : float;
var style : GUIStyle;

var bubble : Texture2D;
var bubbleSize : Vector2[];
var textString : String[];

private var selected : int;
private var showBubble : boolean;
private var wasIn : boolean = false;

function OnGUI () {

	if(showBubble){
		
		var drawbase : Vector2 = Camera.main.WorldToScreenPoint(transform.position);
		
			if (drawbase.y > 0 && drawbase.x > 0 && drawbase.x < Screen.width){
				
				drawbase.y = (Screen.height - drawbase.y) - bubbleSize[selected].y;
				drawbase.x = drawbase.x - bubbleSize[selected].x;
				
				GUI.DrawTexture(Rect(drawbase.x,drawbase.y,bubbleSize[selected].x,bubbleSize[selected].y),bubble,ScaleMode.StretchToFill,true);
				GUI.Label(Rect(drawbase.x+50,drawbase.y,bubbleSize[selected].x-100,(bubbleSize[selected].y/2)),textString[selected],style);
			}
	}

}

function Update () {
	if (!player){
		
		var playerTalker : GameObject = GameObject.FindWithTag("Talker");
		var dist : float = Vector3.Distance(playerTalker.transform.position,transform.position);
		
			if(dist < range && !wasIn){
				
				wasIn = true;
				selected = Random.Range(0,textString.Length);
				showBubble = true;
				playerTalker.GetComponent(TalkScript).showBubble = true;
				playerTalker.GetComponent(TalkScript).selected = selected;
				
			} else if (dist > range && wasIn){
				
				wasIn = false;
				showBubble = false;
				playerTalker.GetComponent(TalkScript).showBubble = false;
								
			}
		
	}
}