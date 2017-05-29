using UnityEngine;
using System.Collections;

public class InterativObjectAnimation : MonoBehaviour {
	
	[Header("The object to animate.", order = 0)]
	public GameObject animationObject;
	
	private Animator _animator;
	
	[Header("The parameter to play the right animation")]
	public int animationName = 1;

	public GameObject[] pathToDesactivate;
	public GameObject[] pathToActivate;
	
	public void Start()
	{
		if(animationObject.GetComponent<Animator>() != null)
			_animator = animationObject.GetComponent<Animator>();
		else 
			Debug.LogError("The object" + _animator.name + " doesn't have an animator on it");
	}
	
	public void PlayAnimation()
	{
		_animator.SetInteger("STATE", animationName);
		ActivateNewPath();
	}

	protected void ActivateNewPath()
	{
		if(pathToDesactivate.Length != 0) {
			for(int i = 0; i < pathToDesactivate.Length; i++)
			{
				pathToDesactivate[i].SetActive(false);
			}
		}

		if(pathToActivate.Length != 0) {
			for(int i = 0; i < pathToActivate.Length; i++)
			{
				pathToActivate[i].SetActive(true);
			}
		}
	}		
}
