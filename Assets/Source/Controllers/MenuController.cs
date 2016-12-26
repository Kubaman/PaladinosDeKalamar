using UnityEngine;
using System.Collections;

namespace Menus
{
	public class MenuController : MonoBehaviour 
	{
		public Menu MainMenu;
		public Menu CurrentMenu;

		// Use this for initialization
		void Start () 
		{
		
		}
		
		// Update is called once per frame
		void Update () 
		{
		
		}

		public void ChangeMenu(Menu next)
		{
			if (CurrentMenu != null) {
				CurrentMenu.IsOpen = false;
			}

			next.IsOpen = true;
		}
	}
}