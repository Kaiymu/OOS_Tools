using UnityEngine;
using System.Collections;

public class CutsceneManager : SingleBehaviour<CutsceneManager> {

	[System.Serializable]
	public class Cutscene
	{
        public CutScenesParameters cutScenesParameters;

		[Header ("Le nom de la cutscene")]
		public string name;

		[Header ("Le texte à afficher")]
		public string text;

		[Header ("Le temps durant lequel la cutscene reste afficher (secondes)")]
		public float time;

		public enum FONT {Mother, Shrink};

		public enum ROOM {NULL, ROOM_01, LIVING_01, KITCHEN, BATHROOM, CHILDROOM, LIVING_02, ROOM_02, FINAL}
		public ROOM roomToLoad;

		[Header ("La font à utiliser pendant la cutscene")]
		public FONT font;

		[Header ("La question posée par le psychologue à la fin de la cutscene")]
		public string questionToCall;

		[HideInInspector]
		public bool hasBeenValidate = false;
	}
	
	[Header ("Les différentes cutscenes disponibles")]
	public Cutscene[] cutscenes;
}
