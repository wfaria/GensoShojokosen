using UnityEngine;
using System.Collections;
using UnityUtilLib;
using TeamUtility.IO;

public class MultiplayerInput : StaticGameObject<MultiplayerInput> {

	/// <summary>
	/// Number of players reading input from.
	/// </summary>
	[SerializeField]
	private int playerCount = 2;
	public static int PlayerCount {
		get {
			return Instance.playerCount;
		}
	}

	[SerializeField]
	private string prefix = "Player";
	public static string Prefix {
		get {
			return Instance.prefix;
		}
	}

	[SerializeField]
	private bool invertNames = false;
	public static bool InvertNames {
		get {
			return Instance.invertNames;
		}
	}

	[SerializeField]
	private bool spaceNames = true;
	public static bool SpaceNames {
		get {
			return Instance.spaceNames;
		}
	}

	private static PlayerInput[] playerInputs;

	public static PlayerInput GetPlayerControl(int player) {
		if(player < 1 || player > PlayerCount) {
			throw new System.IndexOutOfRangeException("Player Number has to be between 1 and " + PlayerCount);
		} else {
			return playerInputs[player - 1];
		}
	}

	public class PlayerInput {

		private string axisAddend;
		private int playerNumber;

		public PlayerInput(int playerNum) {
			playerNumber = playerNum;
			axisAddend = playerNum.ToString();
			if(SpaceNames)
				axisAddend = " " + axisAddend + " ";
			axisAddend = (InvertNames) ? axisAddend + Prefix : Prefix + axisAddend;
		}

		private string AxisString(string a) {
			return (InvertNames) ? a + axisAddend : axisAddend + a;
		}

		public float GetAxis(string axis) {
			return InputManager.GetAxis (AxisString(axis));
		}

		public float GetAxisRaw(string axis) {
			return InputManager.GetAxisRaw (AxisString(axis));
		}

		public bool GetButton(string axis) {
			return InputManager.GetButton (AxisString(axis));
		}

		public bool GetButtonDown(string axis) {
			return InputManager.GetButtonDown (AxisString(axis));
		}

		public bool GetButtonUp(string axis) {
			return InputManager.GetButtonUp (AxisString(axis));
		}
	}

	/// <summary>
	/// Called upon construciton. Used to determine whether this object is the current instance or not.
	/// Used to initialize the 
	/// </summary>
	public override void Awake () {
		base.Awake ();
		playerInputs = new PlayerInput[playerCount];
		for(int i = 0; i < playerCount; i++) {
			playerInputs[i] = new PlayerInput(i + 1);
		}
	}
}
