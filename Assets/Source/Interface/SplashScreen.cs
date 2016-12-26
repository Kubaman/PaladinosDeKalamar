using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour 
{
	public float TransitionTime;
	public Menu NextMenu;

	private Menu currentMenu;
	private bool started;

	// Use this for initialization
	void Start () 
	{
		currentMenu = GetComponent<Menu>();
	}

	void Update ()
	{
		if (currentMenu.IsOpen && !started) 
		{
			started = true;
			StartCoroutine (LoadNextMenu ());
		}
	}

	IEnumerator LoadNextMenu()
	{
		yield return new WaitForSeconds (TransitionTime);

		if (currentMenu != null) 
		{
			currentMenu.IsOpen = false;
		}	

		NextMenu.IsOpen = true;
	}
}
