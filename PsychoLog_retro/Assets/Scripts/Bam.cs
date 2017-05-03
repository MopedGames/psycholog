using UnityEngine;
using System.Collections;

public class Bam : MonoBehaviour {

    public float maxAngles = 180f;

    public float duration = 0.8f;

    public AnimationCurve growAnimation;

	
    public void Hit (){
        StartCoroutine(Hits());
        GetComponentInParent<ParticleSystem>().Play();
    }

    IEnumerator Hits(){
        float t = 0.0f;
        float targetAngle = Random.Range(maxAngles * -1, maxAngles);
        float angle = transform.localEulerAngles.z;
        while (t < duration)
        {
            t += Time.deltaTime;
            float size = growAnimation.Evaluate(t / duration);
            transform.localScale = new Vector3(size,size,size);
            angle = Mathf.Lerp(angle, targetAngle, t / duration);
            transform.localEulerAngles = new Vector3(0, 0, angle);

            yield return null;
        }

        transform.localEulerAngles = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
