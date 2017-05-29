using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This script was used in our game to make all the cutscene effects without any problems
// I used a spline frameworkd called "Curvy".
// In our game, we needed to do some cutscenes, so I gave a tool to my GameDesigners, and it helped them shape a behavior for the camera
// At a precise moment, they could launch a cutscene, and decide what the camera will look, when, how much time, what text it'll display, what sound it'll play ....
public class CutScenesParameters : MonoBehaviour {

	public List<CutSceneStep> cutSceneSteps = new List<CutSceneStep>();
	public GameObject cutsceneCamera;
	public GameObject gameCamera;

    // Used as debug.
	public bool skyppingCutscene;
    public bool playCutSceneAtStart = false;

	public GameObject mainSound;
	private GameObject textToErase;

	private CameraMovementOnPath _cameraMovement;

    public void Start() {
        if(playCutSceneAtStart)
			LaunchCutScene();

		if(Camera.main.GetComponent<CameraMovementOnPath>() != null) {
			_cameraMovement = Camera.main.GetComponent<CameraMovementOnPath>();
		}
    }

    public void LaunchCutScene() {
		if(_cameraMovement != null) {
			_cameraMovement.canMoveDuringCutscene = false;
		}
    }

    IEnumerator CutSceneSteps() {
		yield return null;

		gameCamera.SetActive(false);
		cutsceneCamera.SetActive(true);

		if(!skyppingCutscene) {
			for(int i = 0; i < cutSceneSteps.Count; i++) {
				CutSceneStep step = cutSceneSteps[i];

				switch(step.type)
				{
					case CutSceneStepType.CAMERA_MOVEMENT:
	                    yield return StartCoroutine(CameraFollowPathCoroutine(step.cameraPath, step.duration, step.cameraEvolutionOnPathCurve, step.lookAtObjectStart, step.lookAtObjectEnd, step.objectToDisplayText, step.localisationKey, step.objectAnimationToPlay, step.nameAnimationToPlay));
					break;

					case CutSceneStepType.WAIT: 
					    yield return new WaitForSeconds(step.duration);
					break;

					case CutSceneStepType.END: 
                        // Fade
					yield return null;
					break;
				}
			}
		}
	}

	IEnumerator CameraFollowPathCoroutine (CurvySpline path, float duration, AnimationCurve cameraEvolutionOnPathCurve, Transform lookAtObjectStart, Transform lookAtObjectEnd, GameObject objectToDisplayText, string localisationKey, GameObject animationToPlay, string nameAnimationToPlay) {

		    float elapsedTime = 0;
            if (textToErase != null && textToErase.GetComponent<CutScenesText3D>() != null) {
                textToErase.GetComponent<CutScenesText3D>().MessageToErase();
            }
            textToErase = objectToDisplayText;

            if (objectToDisplayText != null && objectToDisplayText.GetComponent<CutScenesText3D>() != null) {
                objectToDisplayText.GetComponent<CutScenesText3D>().MessageToDisplay(localisationKey);
            }

			if(animationToPlay != null && animationToPlay.GetComponent<Animation>()) {
				animationToPlay.GetComponent<Animation>().Play(nameAnimationToPlay);
			}

            while (elapsedTime < duration) {

                float k = elapsedTime / duration;
                float abstractK = Mathf.Clamp01(cameraEvolutionOnPathCurve.Evaluate(k));

                cutsceneCamera.transform.position = path.Interpolate(abstractK);
                cutsceneCamera.transform.LookAt(Vector3.Lerp(lookAtObjectStart.position, lookAtObjectEnd.position, abstractK));

                
                /*if (GameManager.instance.currentState != GameManager.STATE.PAUSE) {
                    elapsedTime += Time.deltaTime;
                }*
                 */
 
                yield return null;
            }

            cutsceneCamera.transform.position = path.Interpolate(1);
            cutsceneCamera.transform.LookAt(lookAtObjectEnd.position);
	}
}

public enum CutSceneStepType {
	CAMERA_MOVEMENT,
	WAIT,
	END
}

[System.Serializable]
public class CutSceneStep {

	public CutSceneStepType type;
	public CurvySpline cameraPath;
	public AnimationCurve cameraEvolutionOnPathCurve;
	public float duration;
	public Transform lookAtObjectStart;
	public Transform lookAtObjectEnd;

	public GameObject objectToDisplayText;
	public string localisationKey;

	public GameObject objectAnimationToPlay;
	public string nameAnimationToPlay;
}