using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
	public class CharacterModel 
	{
		#region Atributos Internos	
		[SerializeField] private string characterName;
		[SerializeField] private int actorNumber;
		//[SerializeField] private ClassType characterClass;

		[SerializeField] private bool isMainPlayer;
		[SerializeField] private int modelIndex;

		[SerializeField] private int strength;
	    [SerializeField] private int vitality;
		[SerializeField] private int inteligence;
	    [SerializeField] private int wisdom;
	    [SerializeField] private int dextery;
	    [SerializeField] private int agility;
		
		[SerializeField] private float strengthExperience;
	    [SerializeField] private float vitalityExperience;
	    [SerializeField] private float inteligenceExperience;
	    [SerializeField] private float wisdomExperience;
	    [SerializeField] private float dexteryExperience;
	    [SerializeField] private float agilityExperience;

		[SerializeField] private int hitPoints;
		[SerializeField] private int magicPoints;	
		
		[SerializeField] private int maxHitPoints;
		[SerializeField] private int maxMagicPoints;
		
		[SerializeField] private int currentCoolDown;
		[SerializeField] private int firstActionCoolDown;
		[SerializeField] private int secondActionCoolDown;
		[SerializeField] private int thirdActionCoolDown;
		[SerializeField] private int specialActionCoolDown;	

		
		[SerializeField] private float attackRange;
		[SerializeField] private int attackCoolDown;	
		[SerializeField] private int attackDuration;
		[SerializeField] private string weaponPrefab;
		//[SerializeField] private WeaponType weapon;
		
		[SerializeField] private int attackDamage;
		[SerializeField] private int magicDamage;
		[SerializeField] private int armor;
		[SerializeField] private int magicResist;
		[SerializeField] private float attackSpeed;
		[SerializeField] private int coolDownReduction;
		[SerializeField] private float movimentSpeed;

		[SerializeField] private float criticalChance;
		[SerializeField] private float criticalDamage;
		#endregion	
		
		#region Atributos de Interface			
		//[SerializeField] private DisplayController display;
		[SerializeField] private GameObject lifeBar;	
		#endregion	

		#region Properties
		public string CharacterName { get{return characterName;} set{characterName = value;}} 
		//public ClassType CharacterClass { get{return characterClass;} set{characterClass = value;}} 
		public int ActorNumber { get{return actorNumber;} set{actorNumber = value;}} 
		public bool IsMainPlayer { get{return isMainPlayer;} set{isMainPlayer = value;}} 
		public int ModelIndex { get{return modelIndex;} set{modelIndex = value;}}

		public int Strength { get{return strength;} set{strength = value;}}
	    public int Vitality { get { return vitality; } set { vitality = value; } }
	    public int Inteligence { get { return inteligence; } set { inteligence = value; }}
	    public int Wisdom { get { return wisdom; } set { wisdom = value; }}
	    public int Dextery { get { return dextery; } set { dextery = value; }}
	    public int Agility { get { return agility; } set { agility = value; }} 

		public float StrengthExperience { get{return strengthExperience;} set{strengthExperience = value;}}
	    public float VitalityExperience { get { return vitalityExperience; } set { vitalityExperience = value; }}
	    public float InteligenceExperience { get { return inteligenceExperience; } set { inteligenceExperience = value; }}
	    public float WisdomExperience { get { return wisdomExperience; } set { wisdomExperience = value; }}
	    public float DexteryExperience { get { return dexteryExperience; } set { dexteryExperience = value; }}
	    public float AgilityExperience { get { return agilityExperience; } set { agilityExperience = value; }}

	    public int HitPoints { get { return hitPoints; } set { hitPoints = value; }}
	    public int MagicPoints { get { return magicPoints; } set { magicPoints = value; }}
	    public int MaxHitPoints { get { return maxHitPoints; } set { maxHitPoints = value; }}
	    public int MaxMagicPoints { get { return maxMagicPoints; } set { maxMagicPoints = value; }}

		public float LifePercent { get { return (float)((float)hitPoints / (float)maxHitPoints); } }
		public float ManaPercent { get { return (float)((float)magicPoints / (float)maxMagicPoints); } }
		
		public int CurrentCoolDown { get { return currentCoolDown; } set { currentCoolDown = value; }}
	    public int FirstActionCoolDown { get { return firstActionCoolDown; } set { firstActionCoolDown = value; }}
	    public int SecondActionCoolDown { get { return secondActionCoolDown; } set { secondActionCoolDown = value; }}
	    public int ThirdActionCoolDown { get { return thirdActionCoolDown; } set { thirdActionCoolDown = value; }}
	    public int SpecialActionCoolDown { get { return specialActionCoolDown; } set { specialActionCoolDown = value; }}	
		    
	    public float AttackRange { get { return attackRange; } set { attackRange = value; }}
		public int AttackCoolDown { get { return attackCoolDown; } set { attackCoolDown = value; }}
		public int AttackDuration { get { return attackDuration; } set { attackDuration = value; }}
		public string WeaponPrefab { get { return weaponPrefab; } set { weaponPrefab = value; }}	
		//public WeaponType Weapon { get { return weapon; } set { weapon = value; }}	
		
		public int AttackDamage { get { return attackDamage; } set { attackDamage = value; }}
		public int MagicDamage { get { return magicDamage; } set { magicDamage = value; }}
		public int Armor { get { return armor; } set { armor = value; }}
		public int MagicResist { get { return magicResist; } set { magicResist = value; }}
		public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; }}
		public int CoolDownReduction { get { return coolDownReduction; } set { coolDownReduction = value; }}
		public float MovimentSpeed { get { return movimentSpeed; } set { movimentSpeed = value; }}

		public float CriticalChance { get { return criticalChance; } set { criticalChance = value; }}
		public float CriticalDamage { get { return criticalDamage; } set { criticalDamage = value; }}
		#endregion
		
		public CharacterModel(string charName)//, ClassType classType)
		{
			characterName = charName;
			//characterClass = classType;

			currentCoolDown = 2000;
			firstActionCoolDown = 2000;
			secondActionCoolDown = 2000;
			thirdActionCoolDown = 2000;
			specialActionCoolDown = 2000;
	    }
		
		public void UpdateCoolDown()
		{
			//if(IsMainPlayer)
			//		Debug.Log ("CoolDown " + firstActionCoolDown + " | " + secondActionCoolDown + " | " + thirdActionCoolDown);
		
			currentCoolDown++;
			firstActionCoolDown++;
			secondActionCoolDown++;
			thirdActionCoolDown++;
			specialActionCoolDown++;
		}
		
		public int CalculateDamage(DamageType damageType, int amount)
		{
			/* TODO calc damage */
			return amount;
		}
		
		public int CalculateEffectDuration(EffectType effectType, int amount, int duration)
		{
			/* TODO calc duration */
			return duration;
		}
	}
}