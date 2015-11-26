using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {
	
	public AudioSource[] sounds;
	// Use this for initialization
	void Awake () {
		sounds [Random.Range (0, sounds.Length)].Play ();
		Destroy (gameObject, 3f);
	}
}
