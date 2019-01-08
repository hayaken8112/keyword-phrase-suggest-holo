using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

public class SendTexture : MonoBehaviour
{

    public string serverAddress = "http://10.13.255.14:8080/get_cap";
    public string yoloAddress = "http://10.13.255.14:8888/yolo";
    public string filename = "test.png";
    public InputField inputField;
    // Use this for initialization
    public PhraseManager phraseManager;
    public SuggestionManager suggestionManager;

    public void SendImageToServer(Texture2D tex, string formtext="")
    {
        StartCoroutine(UploadImage(tex, formtext));
    }
    public void SendImageToYolo(Texture2D tex)
    {
        StartCoroutine(UploadImageToYolo(tex));
    }

    IEnumerator UploadImage(Texture2D tex, string formtext)
    {
        //Texture2D tex_to_send = DeCompress(tex);
        var bytes = tex.EncodeToJPG();
        //File.WriteAllBytes(Application.dataPath + "/../SavedScreen.jpg", bytes);
        Debug.Log(bytes.Length);
        string encodedText = Convert.ToBase64String(bytes);
        WWWForm form = new WWWForm();
        form.AddField("file", encodedText);
        form.AddField("text", formtext);
        // HTTPリクエストを送る
        UnityWebRequest request = UnityWebRequest.Post(serverAddress, form);
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
            // Debug.Log(request.downloadHandler.text);
            string responseText = request.downloadHandler.text;
            Captions captions = JsonUtility.FromJson<Captions>(responseText);
            phraseManager.UpdatePhrase(captions.caption);
            foreach(string cap in captions.caption) {
                Debug.Log(cap);
            }
            // var resList = Serialization.CreateFromJSON(responseText);
            // text.text = resList.message;
        }

    }
    IEnumerator UploadImageToYolo(Texture2D tex)
    {
        //Texture2D tex_to_send = DeCompress(tex);
        var bytes = tex.EncodeToJPG();
        //File.WriteAllBytes(Application.dataPath + "/../SavedScreen.jpg", bytes);
        Debug.Log(bytes.Length);
        string encodedText = Convert.ToBase64String(bytes);
        WWWForm form = new WWWForm();
        form.AddField("file", encodedText);
        // HTTPリクエストを送る
        UnityWebRequest request = UnityWebRequest.Post(yoloAddress, form);
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
            // Debug.Log(request.downloadHandler.text);
            string responseText = request.downloadHandler.text;
            Debug.Log(responseText);
            var resData = ResponseData.CreateFromJSON(responseText);
            List<string> keywords = new List<string>();
            foreach(var data in resData.data){
                keywords.Add(data);
            }
            suggestionManager.UpdateSuggestedWords(keywords);
            // var resList = Serialization.CreateFromJSON(responseText);
            // text.text = resList.message;
        }

    }
}