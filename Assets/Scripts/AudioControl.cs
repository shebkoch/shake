using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {

	public static AudioControl Instance { get; private set; }
	public List<AudioClip> aplous = new List<AudioClip>();
	AudioSource audioSource;
	public void Awake()
	{
		Instance = this;
		audioSource = GetComponent<AudioSource>();
	}
	public void PlayRandomAplous()
	{
		int randomAudioIndex = Random.Range(0, aplous.Count - 1);
		audioSource.PlayOneShot(aplous[randomAudioIndex]);
	}
}