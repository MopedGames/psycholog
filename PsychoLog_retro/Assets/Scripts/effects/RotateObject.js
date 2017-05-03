#pragma strict

var rotation : Vector3;
private var startRotation : Vector3;

function Start () {

	startRotation.x = transform.eulerAngles.x;
	startRotation.y = transform.eulerAngles.y;
	startRotation.z = transform.eulerAngles.z;

}

function Update () {

	transform.eulerAngles.x = startRotation.x + rotation.x*Time.time;
	transform.eulerAngles.y = startRotation.y + rotation.y*Time.time;
	transform.eulerAngles.z = startRotation.z + rotation.z*Time.time;

}