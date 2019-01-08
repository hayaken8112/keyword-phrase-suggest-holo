using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class JsonDataForSave
{
    public int mode;
    public string sentence;
}

public class test : MonoBehaviour {
	public string address = "http://10.13.255.14:8181/save";
    public InputField inputField;
    public SuggestionManager suggestionManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	IEnumerator SendTestRequest(string jsonData){
        WWWForm form = new WWWForm();
        form.AddField("text", jsonData);
        // HTTPリクエストを送る
        UnityWebRequest request = UnityWebRequest.Post(address, form);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            // POSTに失敗した場合，エラーログを出力
            Debug.Log(request.error);
        }
        else
        {
            // POSTに成功した場合，レスポンスコードを出力
            Debug.Log(request.responseCode);
        }
	}

    public void OnButton2Clicked(){
		string messasge = inputField.text;
        if (messasge == "") return;
        JsonDataForSave beforeJson = new JsonDataForSave();
        beforeJson.mode = 2;
        beforeJson.sentence = messasge;
        string jsonData = JsonUtility.ToJson(beforeJson);
		Debug.Log(jsonData);
		StartCoroutine(SendTestRequest(jsonData));
        inputField.text = "";
        suggestionManager.ClearSelectedWords();
    }
}
