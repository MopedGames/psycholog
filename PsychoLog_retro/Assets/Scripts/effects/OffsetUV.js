#pragma strict

	var Speed : Vector2[];
	var TextureName : String[];

function Update () {
	
	var i : int;
	var getPos : Vector2;
	
	for (i = 0; i < Speed.length; i++){
		
		getPos = GetComponent.<Renderer>().material.GetTextureOffset (TextureName[i]);
		
		getPos.x = getPos.x+(Speed[i].x*Time.deltaTime);
		getPos.y = getPos.y+(Speed[i].y*Time.deltaTime);
		
	    GetComponent.<Renderer>().material.SetTextureOffset (TextureName[i], getPos);
	}
}