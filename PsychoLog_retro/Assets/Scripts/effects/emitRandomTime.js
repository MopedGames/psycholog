#pragma strict

var timeRange : Vector2;
private var timer : float;

function Awake () {
	
	timer = Random.Range(timeRange.x, timeRange.y);
	
}


function Update () {
	timer = timer - Time.deltaTime;
	if(timer < 0){
		emitParticles();
	}
	
}

function emitParticles () {
	timer = Random.Range(timeRange.x, timeRange.y);
	
	GetComponent.<ParticleSystem>().Play();
	
}