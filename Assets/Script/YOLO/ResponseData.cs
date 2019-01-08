using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResponseData {
	public List<string> data;

    public static ResponseData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<ResponseData>(jsonString);
    }
}

[Serializable]
public class ObjectData {
	public string label;
	public float x;
	public float y;
}
