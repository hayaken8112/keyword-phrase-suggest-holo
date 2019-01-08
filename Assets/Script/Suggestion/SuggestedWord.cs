using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuggestedWord : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// ObservableEventTrigger trigger = gameObject.AddComponent<ObservableEventTrigger>();
		// trigger.OnPointerEnterAsObservable().Subscribe(_ => {
		// 	RectTransform rect = this.GetComponent<RectTransform>();
		// 	rect.transform.localScale = new Vector3(1.2f,1.2f,1);
		// });
		// trigger.OnPointerExitAsObservable().Subscribe(_ => {
		// 	RectTransform rect = this.GetComponent<RectTransform>();
		// 	rect.transform.localScale = new Vector3(1,1,1);
		// });
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SetWord(string txt) {
		this.GetComponentInChildren<Text>().text = txt;
	}
	public string GetWord() {
		return this.GetComponentInChildren<Text>().text;
	}
}
