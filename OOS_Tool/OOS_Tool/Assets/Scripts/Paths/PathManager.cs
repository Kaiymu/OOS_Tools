using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathManager : SingleBehaviour<PathManager> {

	private List<GameObject> _listPath = new List<GameObject>();
	private GameObject[] _containerPaths;

	private int _countContainerPaths = 0;
		
	void OnDrawGizmos() {
		_containerPaths = GameObject.FindGameObjectsWithTag("HOPath");
		if (Application.isEditor && _countContainerPaths != _containerPaths.Length) {
			FillListpath();
		}

		_countContainerPaths = _containerPaths.Length;
	}

	void Awake(){
		_containerPaths = GameObject.FindGameObjectsWithTag("HOPath");
		FillListpath();
	}

    void FillListpath() {
		_listPath.Clear();
		for(int i = 0; i < _containerPaths.Length; i++)
		{
			_listPath.Add(_containerPaths[i]);
		}
	}

	public GameObject GetPathByName(string namePath){
		for(int i = 0; i < _listPath.Count; i++)
		{
			if(namePath == _listPath[i].name)
				return _listPath[i];
		}
		return null;
	}

	public int CountPath() {
		return _listPath.Count;
	}


}
