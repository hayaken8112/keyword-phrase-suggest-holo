using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhraseManager : MonoBehaviour {

	public int phraseNum = 3;
	public GameObject phraseObj;
	public InputField inputField;
	List<Text> phraseTexts;

	// Use this for initialization
	void Start () {
		phraseTexts = new List<Text>();
		for (int i = 0; i < phraseNum; i++) {
			GameObject obj = Instantiate(phraseObj, this.transform);
			Text buttonText = obj.GetComponentInChildren<Text>();
			phraseTexts.Add(buttonText);

			obj.GetComponent<Button>().onClick.AddListener(delegate {
				inputField.text = buttonText.text;
			});
		}
		
	}
	
	// Update is called once per frame
	public void UpdatePhrase (List<string> phrases) {
		for (int i = 0; i< phraseNum; i++) {
			phraseTexts[i].text = phrases[i];
		}
		
	}
}
