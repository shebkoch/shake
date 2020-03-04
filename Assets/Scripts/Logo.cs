using System;
using UnityEngine;
using UnityEngine.UI;

public class Logo : UnityEngine.MonoBehaviour
{
	public float showTime;
	public float fadeSpeed;
	private bool startFading = false;
	public void Start()
	{
		Invoke(nameof(StartFading),showTime);
	}

	public void StartFading()
	{
		startFading = true;
	}
	

	public void Update()
	{
		if (startFading)
		{
			Color color = GetComponent<Image>().color;
			color.a -= fadeSpeed;
			if(color.a < 0) Destroy(gameObject);
			GetComponent<Image>().color = color;
		}
	}
}
