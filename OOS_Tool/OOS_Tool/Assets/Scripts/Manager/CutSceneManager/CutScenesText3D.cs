using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(TextMesh))]
public class CutScenesText3D : MonoBehaviour {
 
	public float letterPause = 0.2f;
	public AudioClip sound;
 
	private string message;
    private bool fadeInOut = false;

    private TextMesh _sceneTextMessage;
    private Color _color;
    private bool canFade;

	private void Start() {
		_sceneTextMessage = GetComponent<TextMesh>();
	}

	public void MessageToDisplay (string localisationKey) {
        canFade = true;
        //message = Localization.Get(localisationKey);
        _sceneTextMessage.text = "";
        StopAllCoroutines();
		StartCoroutine(TypeText ());
	}

   // GetComponent<MeshRenderer>().material.color = new Color(TextColor.r, TextColor.g, TextColor.b, alpha);

    private float FadeTexteMesh(float speed) {

        float alpha = _sceneTextMessage.color.a;
        Color finalColor = _sceneTextMessage.color;

        alpha += speed * Time.deltaTime;
        alpha = Mathf.Clamp(alpha, 0f, 1f);

        finalColor.a = alpha;

        _sceneTextMessage.color = finalColor;
        return finalColor.a;
    }

    public void Update() {
        if (canFade)
            FadeTexteMesh(0.7f);
        else {
            if (FadeTexteMesh(-1f) == 0f) {
                _sceneTextMessage.text = "";
            } 
        }
    }

	public void MessageToErase() {
		StopAllCoroutines();
        canFade = false;
	}
 
	IEnumerator TypeText () {
		foreach (char letter in message.ToCharArray()) {
            _sceneTextMessage.text += letter;
			if (sound)
				GetComponent<AudioSource>().PlayOneShot (sound);

			yield return new WaitForSeconds (letterPause);
		}      
	}
}
