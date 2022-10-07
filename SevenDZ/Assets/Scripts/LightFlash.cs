using UnityEngine;
using System.Collections;

public class LightFlash : MonoBehaviour {

	[SerializeField] private Light anylight;
	[SerializeField] private float constantIntens=0.0f;
	private float inten = 0.0f;
	private float TimeDown;
	void Start () 
	{
		TimeDown = 1.0f;
	}
	void Update () {

		if (anylight.intensity != constantIntens) 
		{
			anylight.intensity = constantIntens; 
		}
		if (TimeDown > 0) 
		{ 
			TimeDown -= Time.deltaTime;
		}
		if (TimeDown < 0)
		{
			TimeDown = 0;
		}
		if(TimeDown == 0) {
			inten = Random.Range(0.2f, 4.0f);
			anylight.intensity = inten;
			TimeDown = Random.Range(0.2f, 0.6f);
		}
	}
}
