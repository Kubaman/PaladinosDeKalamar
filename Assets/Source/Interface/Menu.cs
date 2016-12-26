using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public bool StartOpen;

	private Animator animator;
	private CanvasGroup canvasGroup;

	public bool IsOpen
	{
		get 
		{
			return animator.GetBool("IsOpen");
		}

		set 
		{
			if(value)				
				canvasGroup.blocksRaycasts = canvasGroup.interactable = true;
			else				
				canvasGroup.blocksRaycasts = canvasGroup.interactable = false;

			animator.SetBool("IsOpen", value);
		}
	}

	public void Awake()
	{
		animator = GetComponent<Animator>();
		canvasGroup = GetComponent<CanvasGroup>();

		IsOpen = StartOpen;
	}
}