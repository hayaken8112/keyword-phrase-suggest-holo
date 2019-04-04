using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResponseData {
	public List<string> data;

    public static ResponseData CreateFromJSON(string jsonString)
    {
        ResponseData resData = null;
        try {
            resData = JsonUtility.FromJson<ResponseData>(jsonString);
        } catch (Exception e) {
            Debug.Log(e);
        }
        return resData;
    }
}

[Serializable]
public class ObjectData {
	public string label;
	public float x;
	public float y;
}
