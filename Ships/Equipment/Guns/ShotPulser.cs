using UnityEngine;
using System.Collections;

public class ShotPulser : Gun {

	// Use this for initialization
	void Awake () {
		FireSpeed = 30f;
		StartCoroutine (PulseFire ());
	}

	IEnumerator PulseFire(){
		Fire (-Vector3.forward);
		yield return new WaitForSeconds(2f);
		StartCoroutine (PulseFire ());
	}


}
