#region Declaration;
using UnityEngine;
using System.Collections;

namespace GenericFunctions{
#endregion

	public static class Constants{

		#region ships
		public static string uniterShip = "CubeShipUnite/Prefabs/Ships/TheUniter";
		#endregion

		#region Input Strings
		public static string keys_Horizontal = "Horizontal";
		public static string keys_Vertical = "Vertical";
		public static string keys_FireBullets = "FireBullets";
		public static string keys_None = "None";
		public static string keys_EngageInvitationBeam = "EngageInvitationBeam";
		public static string keys_AimShips = "AimShips";

		public static string xbox_Horizontal = "XBOX_Horizontal";
		public static string xbox_Vertical = "XBOX_Vertical";
		public static string xbox_FireBullets = "XBOX_FireBullets";
		public static string xbox_None = "XBOX_None";
		public static string xbox_EngageInvitationBeam = "XBOX_EngageInvitationBeam";
		public static string xbox_AimShips = "XBOX_AimShips";
		#endregion

		#region Layer Integers
		public static int meLayer = 9;
		public static int threatLayer = 10;
		public static int comradLayer = 11;
		public static int bulletLayer = 12;
		public static int invitationBeam = 13;

		#endregion
		public static float ySpawnSpot = 0f;
	}

}
