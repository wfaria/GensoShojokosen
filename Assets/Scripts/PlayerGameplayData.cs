using UnityEngine;
using System.Collections;

public class PlayerGameplayData : MonoBehaviour {
	[SerializeField]
	private Character character;
	
	[SerializeField]
	private Transform spawnLocation;
	public Transform SpawnLocation {
		get {
			return spawnLocation;
		}
	}
	
	[SerializeField]
	private Transform respawnLocation;
	public Transform RespawnLocation {
		get {
			return respawnLocation;
		}
	}
}
