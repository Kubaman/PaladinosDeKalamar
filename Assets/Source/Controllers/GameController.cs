using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	private float gameTime;

	public float GameTime { get{return gameTime;} }

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameTime = UnityEngine.Time.fixedTime;
	}
}
