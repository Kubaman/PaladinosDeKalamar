using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace Actions
{
	public class ActionManager
	{
		private static ActionController _controller;

		public static ActionController Controller
		{
			get
			{
				if (_controller == null)
					_controller = GameObject.Find("ActionController").GetComponent<ActionController>();

				return _controller;
			}
		}

		public static GameObject UseAction(ActionType type, Characters.CharacterController caster) { return Controller.UseAction (type, caster); }
	}

	public enum ActionType
	{
		NONE = 1, 

		MOVE_UP = 2,  
		MOVE_UP_FORWARD = 3,
		MOVE_FORWARD = 4,
		MOVE_FORWARD_DOWN = 5,
		MOVE_DOWN = 6,
		MOVE_DOWN_BACKWARD = 7,
		MOVE_BACKWARD = 8,	
		MOVE_BACKWARD_UP = 9,

		RUN_UP = 10,
		RUN_UP_FORWARD = 11,
		RUN_FORWARD = 12,
		RUN_FORWARD_DOWN = 13,
		RUN_DOWN = 14,
		RUN_DOWN_BACKWARD = 15,
		RUN_BACKWARD = 16,	
		RUN_BACKWARD_UP = 17,

		// Basic Attacks (Basic and 2 command chains)
		NORMAL_ATTACK = 18, 
		FORCE_ATTACK = 19,
		DIRECTIONAL_ATTACK = 20,
		RUNNING_ATTACK = 21,

		HADOUKEN = 22,

		SHORYUKEN = 23,

		ATTACK_DAS_CORUJA = 24,

		SHINKU_HADOUKEN = 25,

		EXPLOSION = 26
	}
}