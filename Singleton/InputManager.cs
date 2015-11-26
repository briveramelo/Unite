using UnityEngine;
using System.Collections;
using GenericFunctions;

public class InputManager : MonoBehaviour {

	#region Initialize Values
	private string horizontal;
	private string vertical;
	private string fireBullets;
	private string none;
	private string engageInvitationBeam;
	private string aimShips;
	private Controller controller;
	private Controller lastController;



	void Awake(){
		ChooseControls();
	}
	#endregion

	public enum Controller{
		Keyboard,
		Xbox360Controller
	}

	#region Return Input Strings
	public string Horizontal{
		get{
			return horizontal;
		}
	}

	public string Vertical{
		get{
			return vertical;
		}
	}

	public string FireBullets{
		get{
			return fireBullets;
		}
	}

	public string None{
		get{
			return none;
		}
	}

	public string EngageInvitationBeam{
		get{
			return engageInvitationBeam;
		}
	}

	public string AimShips{
		get{
			return aimShips;
		}
	}
	#endregion
	

	void Update(){
		if (controller != lastController) {
			ChooseControls();
		}
		lastController = controller;
		#region View Inputs
		ViewInputs ();
	}

	public float hor;
	public float ver;
	public bool fired;
	public bool beamEngaged;
	public bool aimedShips;

	void ViewInputs(){
		hor = Input.GetAxisRaw (horizontal);
		ver = Input.GetAxisRaw (vertical);
		fired = Input.GetButtonDown (fireBullets);
		beamEngaged = Input.GetButtonDown (engageInvitationBeam);
		aimedShips = Input.GetButtonDown (aimShips);
	}
	#endregion

	#region ChooseControls
	void ChooseControls(){
		if (controller == Controller.Keyboard) {
			UseKeyBoardControls();
		}
		else if (controller == Controller.Xbox360Controller) {
			UseXboxController();
		}
	}

	void UseKeyBoardControls(){
		controller = Controller.Keyboard;
		horizontal = Constants.keys_Horizontal;
		vertical = Constants.keys_Vertical;
		fireBullets = Constants.keys_FireBullets;
		none = Constants.keys_None;
		engageInvitationBeam = Constants.keys_EngageInvitationBeam;
		aimShips = Constants.keys_AimShips;
	}

	void UseXboxController(){
		controller = Controller.Xbox360Controller;
		horizontal = Constants.xbox_Horizontal;
		vertical = Constants.xbox_Horizontal;
		fireBullets = Constants.xbox_FireBullets;
		none = Constants.xbox_None;
		engageInvitationBeam = Constants.xbox_EngageInvitationBeam;
		aimShips = Constants.xbox_AimShips;
	}
	#endregion

}
