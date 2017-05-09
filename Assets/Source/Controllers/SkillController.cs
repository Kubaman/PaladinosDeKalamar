using UnityEngine;
using System.Collections;
using Actions;

namespace Skills
{
	public class SkillController : MonoBehaviour 
	{
		public void UseSkill(SkillType type, Characters.CharacterController caster, Vector3 origin, Vector3 direction)
		{
			Debug.Log ("Usou: " + type);

			switch (type) 
			{
				case SkillType.HADOUKEN:					
					ActionManager.UseAction (ActionType.HADOUKEN, caster);
				break;

				case SkillType.SHORYUKEN:					
					ActionManager.UseAction (ActionType.SHORYUKEN, caster);
				break;

				case SkillType.ATTACK_DAS_CORUJA:					
					ActionManager.UseAction (ActionType.ATTACK_DAS_CORUJA, caster);
				break;

				case SkillType.SHINKU_HADOUKEN:					
					ActionManager.UseAction (ActionType.SHINKU_HADOUKEN, caster);
				break;
			}
		}
	}
}
