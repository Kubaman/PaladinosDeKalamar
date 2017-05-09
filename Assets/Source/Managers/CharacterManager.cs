using UnityEngine;
using System.Collections;

namespace Characters
{	
	public class CharacterManager 
	{
		public static CharacterController GetController(GameObject characterGO)
		{
			CharacterController returnedController = null;

			if (characterGO != null)
			{
				returnedController = characterGO.GetComponent<CharacterController> ();
			}

			return returnedController;
		}
	}
}
