using UnityEngine;
using System.Collections;

public class PutOnVector : MonoBehaviour {

	public string pathName;	
	public float speed = 1.0f;
    public float startSpeed = 5.0f;
	public float startPercent = 0.0f; // 0.0f = 0%, 0.5f = 50%, 1.0f = 100%;
    public float distance = 0.0f;
    private Vector3[] thePath;
    private float pathLength;
	private int currentPos = 0;
	private float prevLength = 0.0f;
	
	// Use this for initialization
	void Start () {
        speed = startSpeed;
	    thePath = iTweenPath.GetPath(pathName);
        pathLength = iTween.PathLength(thePath);
		
	}

    public void NewPath (string newPath){
        distance = 0f;
        speed = startSpeed;
        thePath = iTweenPath.GetPath(newPath);
        pathName = newPath;
        pathLength = iTween.PathLength(thePath);
        GetComponentInChildren<controller>().speedForward = startSpeed;
    }
	
	// Update is called once per frame
	void Update () {
		
	distance += speed * Time.deltaTime;
		
	
	/*float pointLength = iTween.PointLength(thePath[currentPos],thePath[currentPos+1]);
	if(distance > prevLength+pointLength){
		currentPos++;
		prevLength = prevLength + pointLength;
	}*/
		
	float percent = startPercent + (distance / pathLength);
    iTween.PutOnPath(gameObject, thePath, percent);
	gameObject.transform.LookAt(iTween.PointOnPath(thePath, percent+0.01f));
	
		
		
		
	}
}
