using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actions;
using Characters;

public class HitShooter : MonoBehaviour {

	private Characters.CharacterController caster;

	// Use this for initialization
	void Start () 
	{
		caster = this.gameObject.GetComponent<Characters.CharacterController> ();
		ActionManager.UseAction (ActionType.EXPLOSION, caster);		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
