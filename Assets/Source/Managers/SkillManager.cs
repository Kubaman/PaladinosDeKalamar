using UnityEngine;
using System.Collections;
namespace Skills
{
	public class SkillManager
	{
		private static SkillController _controller;

		public static SkillController Controller
		{
			get
			{
				if (_controller == null)
					_controller = GameObject.Find("SkillController").GetComponent<SkillController>();

				return _controller;
			}
		}

		public static void UseSkill(SkillType type, Characters.CharacterController caster, Vector3 origin, Vector3 direction){Controller.UseSkill(type, caster, origin, direction);}

	}
}