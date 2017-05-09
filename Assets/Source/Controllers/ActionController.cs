using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace Actions
{	
	public class ActionController : MonoBehaviour 
	{

		// Use this for initialization
		void Start () 
		{
			
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}

		public GameObject UseAction(ActionType type, Characters.CharacterController caster)
		{
			GameObject returnedAction = null;
			switch (type) 
			{
				case ActionType.NORMAL_ATTACK:					
					returnedAction = ObjectManager.InstantiateObject ("Actions/Jab", caster.transform.position);
					returnedAction.transform.parent = caster.transform;
				break;

				case ActionType.HADOUKEN:					
					returnedAction = ObjectManager.InstantiateObject ("Actions/Hadouken", caster.transform.position);
					returnedAction.transform.parent = caster.transform;
				break;

				case ActionType.EXPLOSION:					
					returnedAction = ObjectManager.InstantiateObject ("Actions/Explosion", caster.transform.position);
					returnedAction.transform.parent = caster.transform;
				break;

				case ActionType.SHORYUKEN:					
					returnedAction = ObjectManager.InstantiateObject ("Actions/Shoryuken", caster.transform.position);
					returnedAction.transform.parent = caster.transform;
				break;
			}

			return null;
		}
	}
}