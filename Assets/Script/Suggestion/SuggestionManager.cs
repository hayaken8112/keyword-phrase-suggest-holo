using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuggestionManager : MonoBehaviour {

	List<string> testWords = new List<string> {"sweater", "mouse", "book", "bottle", "person"};
	public GameObject suggestionPrefab;
	List<SuggestedWord> suggestedWords;
	public List<string> selectedWords {
		get;
		private set;
	}
	public int numOfSuggestion = 5;
	public InputField inputField;
	public Button clearButton;

	// Use this for initialization
	void Start () {
		selectedWords = new List<string>();
		suggestedWords = new List<SuggestedWord>();
		for(int i = 0; i < numOfSuggestion; i++) {
			GameObject obj = Instantiate(suggestionPrefab, this.transform);
			SuggestedWord suggestedWord = obj.GetComponent<SuggestedWord>();
			suggestedWords.Add(suggestedWord);
			suggestedWord.SetWord(testWords[i]);
			obj.GetComponent<Button>().onClick.AddListener(delegate {
				Debug.Log("testtest");
				selectedWords.Add(suggestedWord.GetWord());
				UpdateSelectedWords();
			});
		}
		clearButton.onClick.AddListener(delegate {
			ClearSelectedWords();
		});
		
	}
	public void UpdateSuggestedWords(List<string> words) {
		int updateNum = Mathf.Min(numOfSuggestion, words.Count);
		for (int i = 0; i < updateNum; i++) {
			suggestedWords[i].SetWord(words[i]);
		}
	}
	void UpdateSelectedWords(){
		inputField.text = "";
		foreach(string word in selectedWords) {
			inputField.text += word + ' ';
		}
	}

	public void ClearSelectedWords(){
		inputField.text = "";
		selectedWords.Clear();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
