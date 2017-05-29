using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;
using Holoville.HOTween.Path;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Plugins.Core;

public class GivePathPositions : SingleBehaviour<GivePathPositions> {

	private Tweener _tweenAnimation;

	/// <summary>
	/// Called into editor mode.
	/// </summary>

	#if UNITY_EDITOR
	private void OnDrawGizmos() {
		if (_tweenAnimation == null) {
			DeleteHotWeen();
			CreateHotTween();
		}
        GetPathLength();
	}
	#endif

	/// <summary>
	/// Destroy the previous tween.
	/// </summary>
	private void DeleteHotWeen() {
		GameObject _hotweenInScene = GameObject.Find("HOTween : " + PathManager.instance.CountPath());
		GameObject _hotweenInEditor = GameObject.Find("HOTween");

		
		if(_hotweenInScene != null) 
			DestroyImmediate(_hotweenInScene.gameObject);
		
		if(_hotweenInEditor != null)
			DestroyImmediate(_hotweenInEditor.gameObject);
	}

	/// <summary>
	/// Create a new tween
	/// </summary>
	private void CreateHotTween() {
		_tweenAnimation = HOTween.To(gameObject.transform, 0.00001f, new TweenParms()
		                             .Prop( "position", gameObject.GetComponent<HOPath>().MakePlugVector3Path())
		                             .Pause());
	}

	public Tweener tweenAnimation {
		get {return _tweenAnimation;}
	}
	

	private void Awake () {
		DeleteHotWeen();
		CreateHotTween();
	}

    public float GetPathLength() {
        if (_tweenAnimation != null) {
            return _tweenAnimation.GetPathLength();
        }
        else
            return 0f;
    }

	public Vector3 GetPathPosition(float percentage){
        
		if(_tweenAnimation != null)
			return _tweenAnimation.GetPointOnPath(percentage);
		else return new Vector3(999.999f, 999.999f, 999.999f);
	}

	public bool IsClosed(){
		return gameObject.GetComponent<HOPath>().IsClosed;
	}
}
