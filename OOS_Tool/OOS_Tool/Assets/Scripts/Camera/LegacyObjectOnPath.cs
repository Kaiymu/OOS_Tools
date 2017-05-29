using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;
using Holoville.HOTween.Path;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Plugins.Core;

public abstract class LegacyObjectOnPath : MonoBehaviour {

    [Header("Name of the successive gameobjects that contains the path", order = 0)]
    public string pathName;

	[Range(0.002f, 100f)]
	[Header ("Position of the object at the start of the game on the path", order = 0)]
	public float percentage;
	
	protected float _realPercentage;

	public float realPercentage
	{
		get{ return _realPercentage;}
		set{_realPercentage = value;}
	}
}
