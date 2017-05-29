using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;
using Holoville.HOTween.Path;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Plugins.Core;

public class HotWeenInit : MonoBehaviour {
	
	void Awake () {
		HOTween.Init(true, true, true);
	}
}
