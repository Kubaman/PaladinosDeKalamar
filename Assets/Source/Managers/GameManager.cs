using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Skills;
using Actions;
using Commands;
using Characters;

public class GameManager
{
	private static GameObject mainPlayerGO;
	private static Characters.CharacterController mainPlayer;

	public static float GameTime {get { return 0; } }
	
	public static GameObject MainPlayerGO
	{
		get
		{
			if (mainPlayerGO == null)
				mainPlayerGO = GameObject.FindWithTag("MainPlayer");
			
			return mainPlayerGO;
		}
	}
	
	public static Characters.CharacterController MainPlayer
	{
		get
		{
			if (mainPlayer == null)
				mainPlayer = mainPlayerGO.GetComponent<Characters.CharacterController>();
			
			return mainPlayer;
		}
	}	
	
	public static void ApplyAction(ActionType actionType)
	{
		MainPlayer.UseAction(actionType, MainPlayerGO.transform.position);
	}
	
	public static void ApplyCommand(CommandChainType commandChain)
	{			
		MainPlayer.UseSkill(commandChain, MainPlayerGO.transform.position);
	}
	
	public static void ApplyAction(ActionType actionType, Vector3 characterPosition)
	{
		MainPlayer.UseAction(actionType, characterPosition);
	}
	
	public static void ApplyCommand(CommandChainType commandChain, Vector3 characterPosition)
	{
		MainPlayer.UseSkill(commandChain, characterPosition);
	}
}