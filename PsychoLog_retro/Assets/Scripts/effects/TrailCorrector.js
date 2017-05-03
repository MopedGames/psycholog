#pragma strict

var particles : ParticleSystem[];
var originRotation : float[];
private var i : int;

function Update () {

	for (i = 0; i <particles.length; i++){
		
		//Debug.Log ("parent: " + transform.rotation.y + "; origin: " + originRotation[i] + "; final: " + (originRotation[i] + transform.rotation.y));
		var floatHolder : float;
		floatHolder = originRotation[i] + (transform.rotation.y);
		
		particles[i].startRotation = (transform.rotation.y)*3.25;
		Debug.Log (floatHolder + "; " + particles[i].startRotation);
		
	}

}