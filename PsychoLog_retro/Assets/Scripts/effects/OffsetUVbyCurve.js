#pragma strict

	var Speed : float;
	var TextureName : String;
	var curveXEnabled : boolean;
	var curveX : AnimationCurve;
	var curveYEnabled : boolean;
	var curveY : AnimationCurve;
	
function Update () {

	var Coords : Vector2;
	Coords = GetComponent.<Renderer>().sharedMaterial.GetTextureOffset(TextureName);
	if(curveXEnabled){
		Coords.x = curveX.Evaluate(Time.time*Speed);
	}
	if(curveYEnabled){
		Coords.y = curveY.Evaluate(Time.time*Speed);
	}
	

	GetComponent.<Renderer>().sharedMaterial.SetTextureOffset (TextureName, Coords);
	
}