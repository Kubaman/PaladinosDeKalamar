using UnityEngine;
using System.Collections;

namespace Actions
{
	public class Action : MonoBehaviour 
	{	

	}

	public enum ActionType
	{
		NONE, 
		
		MOVE_UP,
		MOVE_UP_FORWARD,
		MOVE_FORWARD,
		MOVE_FORWARD_DOWN,
		MOVE_DOWN,
		MOVE_DOWN_BACKWARD,
		MOVE_BACKWARD,	
		MOVE_BACKWARD_UP,
		
		RUN_UP,
		RUN_UP_FORWARD,
		RUN_FORWARD,
		RUN_FORWARD_DOWN,
		RUN_DOWN,
		RUN_DOWN_BACKWARD,
		RUN_BACKWARD,	
		RUN_BACKWARD_UP,
		
		// Basic Attacks (Basic and 2 command chains)
		NORMAL_ATTACK, 
		FORCE_ATTACK,
		DIRECTIONAL_ATTACK,
		RUNNING_ATTACK
	}
}	