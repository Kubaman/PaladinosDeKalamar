using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Skills
{
	public class Skill : MonoBehaviour 
	{	
		private int level;
		private float experience;
		private SkillType type;
		private GameObject skillGO;		
			
		private List<float> coolDown = new List<float>();
		private List<float> rangeMultiplier = new List<float>();
		private List<float> physicalDamageMultiplier = new List<float>();
		private List<float> magicalDamageMultiplier = new List<float>();
		
		public Skill (SkillType type)
		{
			this.type = type;
		}

		/**
		 * Ponto de entrada para a execução da Skill
		 */ 
		public void PlaySkill(CharacterController caster)
		{
			/*
			// TODO Pegar modelo do Character
			CharacterModel model = caster.Model;
			
			int damage = (int)((model.MagicDamage * magicalDamageMultiplier) + (model.Damage * physicalDamageMultiplier));
			int range = ranges[level-1];
			float coolDown = coolDowns[level-1];
			float pushSpeed = pushSpeeds[level-1];
			float pushDistance = pushDistances[level-1];
			int maxHit = maxHits[level-1];
			Vector3 speed = speeds[level-1];
		
			this.player = player;
			playing = true;
			
			// Calcula posição em função do player.
			Vector3 position = Vector3.zero;			
			if (facingRight)
				position = new Vector3(Player.transform.position.x + positionOffSet, Player.transform.position.y, Player.transform.position.z);
			else
				position = new Vector3(Player.transform.position.x - positionOffSet, Player.transform.position.y, Player.transform.position.z);
			
			// Instancia o target da skill.	
			hitGameObject = null;
			if (facingRight)
				hitGameObject = (GameObject)GameObject.Instantiate(rightTargetObject);
			else
				hitGameObject = (GameObject)GameObject.Instantiate(leftTargetObject);
			
			// Posiciona o target da skill.
			hitGameObject.transform.position = position;
			
			HitBehaviour hitBehaviour = hitGameObject.GetComponent("HitBehaviour") as HitBehaviour;
			hitBehaviour.Shot(player, damage, range, pushSpeed, pushDistance, speed, maxHit, facingRight);
			*/
		}	
	}

	public enum SkillType
	{
		NONE, 
		
		// Special Attacks (3 command chains)
		HADOUKEN,	
		
		// Super Attacks (4 command chains)
		SHORYUKEN,
		
		// Ultra Attacks (5 command chains)
		SHINKU_HADOUKEN
	}
}