using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("MainPlayer"))
		{
			Characters.CharacterController controller = Characters.CharacterManager.GetController(other.gameObject);
			controller.LandCharacter ();
		}
	}
}
