
#region Declarations
using UnityEngine;
using System.Collections;

public class BeamPowerMeter : MonoBehaviour {

#endregion

	#region Initialization
	private ShotDirector shotDirector;
	private Vector3 startMeterScale;
	public Renderer myMeterRenderer;
	private bool materialIsSetToBeam;
	private bool materialIsSetToNone;

	void Awake(){
		startMeterScale = transform.localScale;
	}
	#endregion

	#region Scale the Beam Meter
	void Update(){
		transform.localScale = new Vector3 (startMeterScale.x,startMeterScale.y * (shotDirector.BeamPower/100f),startMeterScale.z);
		if (shotDirector.IsBeaming){
			if (!materialIsSetToBeam){
				materialIsSetToBeam = true;
				materialIsSetToNone = false;
			}
		}
		else{
			if (!materialIsSetToNone){
				materialIsSetToNone = true;
				materialIsSetToBeam = false;
			}
		}
	}
	#endregion

	#region Return Values
	public ShotDirector ShotDirector{
		get{
			return shotDirector;
		}
		set{
			shotDirector = value;
		}
	}
	#endregion

}
