using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour {
	
	private controller charController;
	
	public GameObject trailObject;
	
	public Color minColor;
	public Color maxColor;
	
	public GameObject[] firstWaves;
	
	public Vector2 waveHeight;
	
	public GameObject[] extraWaves;
	
	public ParticleSystem[] foam;

    public Bam[] bam;

    public ParticleSystem topSpeedParticles;

	// Use this for initialization
	void Start () {
		
		charController = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(controller))as controller;
		
	}

    public void Bam (bool big){
        if (big)
        {
            foreach (Bam b in bam)
            {
                b.Hit();
            }
        }
        else
        {
            bam[0].Hit();
        }
    }

	// Update is called once per frame
	void Update () {
		//Setting Trail Opacity
		Color newColor = Color.Lerp (minColor,maxColor, (charController.speedForward*2/charController.normalSpeed));
		trailObject.GetComponent<Renderer>().material.SetColor("_TintColor", newColor);
		
		
		float camTarget = Mathf.Lerp(50,70, (charController.speedForward/charController.normalSpeed));
		
		float smooth = Camera.main.fieldOfView/camTarget;	
		Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, camTarget,0.1f);
		//Checking First Waves
		int i;
		
		for (i= 0; i<firstWaves.Length; i++){
			
			float yVector;
			yVector = Mathf.Lerp(waveHeight.x,waveHeight.y,((charController.speedForward*2)/charController.normalSpeed));
			if (yVector > 1)
				yVector =1;
			
			firstWaves[i].transform.position = new Vector3(firstWaves[i].transform.position.x,yVector,firstWaves[i].transform.position.z);
			
		}

        ParticleSystem.EmissionModule em = topSpeedParticles.emission;
        if(charController.speedForward >= charController.normalSpeed-0.1f){
            
            em.enabled = true;
        } else {
            em.enabled = false;
        }

		//Checking extra Waves
		for (i= 0; i<extraWaves.Length; i++){
			
			float yVector;
			yVector = Mathf.Lerp(waveHeight.x,waveHeight.y,(((charController.speedForward*2)-(charController.normalSpeed))/charController.normalSpeed));
			if (yVector > 1)
				yVector =1;
			
			extraWaves[i].transform.position = new Vector3(extraWaves[i].transform.position.x,yVector,extraWaves[i].transform.position.z);
			
		}
		
		if (charController.speedForward/charController.normalSpeed >= 0.5){
			
			for(i=0; i<foam.Length; i++){
			
				foam[i].enableEmission = true;
			
			}
			
		} else {
			
			for(i=0; i<foam.Length; i++){
				
				foam[i].enableEmission = false;
				
			}
			
		}
	}
}
