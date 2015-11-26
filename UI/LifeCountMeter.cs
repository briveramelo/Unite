#region Declarations
using UnityEngine;
using System.Collections;

public class LifeCountMeter : MonoBehaviour {

#endregion
	public UniterRespawner uniterRespawner;

	#region GUI Display Number Lives
//	void OnGUI(){
//		//GUI.Label(new Rect(10, 100, 100, 20), "x"+uniterRespawner.CurrentNumberOfLives.ToString());
//
//		GUI.Label(new Rect(10, 100, 100, 20), "x1");
//		GUI.Label(new Rect(110, 100, 100, 20), "x2");
//		GUI.Label(new Rect(10, 200, 100, 20), "x3");
//		GUI.Label(new Rect(110, 200, 100, 20), "x4");
//	}
	#endregion

	#region Return Values
	public UniterRespawner UniterRespawner{
		get{
			return uniterRespawner;
		}
		set{
			uniterRespawner = value;
		}
	}
	#endregion
}
