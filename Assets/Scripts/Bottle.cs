using UnityEngine;

[System.Serializable]
public class Bottle {
	public GameObject bottle;
	public bool isOpen = false;
	public int cost;
	public Transform cap;
	public Vector3 capStartPosition;
}
