using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

public class SendToYolo : MonoBehaviour
{

    public string serverAddress = "http://10.13.255.14:8080/yolo";
    public string filename = "test.png";
    public ObjectTagGenerater objectTagGenerater;
    // public Text text;
    // public InputField inputField;
    void Start(){
        objectTagGenerater = GameObject.Find("Canvas").GetComponent<ObjectTagGenerater>();
    }


    public void SendImageToServer(Texture2D tex)
    {
        StartCoroutine(UploadImage(tex));
    }

    IEnumerator UploadImage(Texture2D tex)
    {
        //Texture2D tex_to_send = DeCompress(tex);
        var bytes = tex.EncodeToJPG();
        //File.WriteAllBytes(Application.dataPath + "/../SavedScreen.jpg", bytes);
        Debug.Log(bytes.Length);
        string encodedText = Convert.ToBase64String(bytes);
        WWWForm form = new WWWForm();
        form.AddField("file", encodedText);
        // form.AddField("text", inputField.text);
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
            Debug.Log(request.downloadHandler.text);
            string responseText = request.downloadHandler.text;
            var resData = ResponseData.CreateFromJSON(responseText);
            foreach(var data in resData.data){
                // Debug.Log(data.label);
            }
            // objectTagGenerater.UpdateTag(resData.data);
            // text.text = resList.message;
        }

    }
}