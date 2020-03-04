using UnityEngine;

[System.Serializable]
public class Bottle {
	public GameObject bottle;
	public GameObject shadowBottle;
	[HideInInspector] public Transform cap;
	[HideInInspector] public Vector3 capStartPosition;
	[HideInInspector] public GameObject foam;
	public AudioClip audio;
	public int scoreToNext;
	public int estimatedCount;
}
