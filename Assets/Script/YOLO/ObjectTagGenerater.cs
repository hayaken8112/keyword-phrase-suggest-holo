using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ObjectTagGenerater : MonoBehaviour {
	public int maxtag = 5;
	public GameObject tagPrefab;
	List<GameObject> tagList;
	const int camera_width = 896;
	const int camera_height = 504;

	// Use this for initialization
	void Start () {
		tagList = new List<GameObject>();
		for (int i = 0; i < maxtag; i++) {
			GameObject obj = Instantiate(tagPrefab, this.transform);
			tagList.Add(obj);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateTag(List<ObjectData> data_list) {
		// Create new tags
		for (int i = 0; i < maxtag; i++){
			if (i < data_list.Count) {
				SetTag(data_list[i], tagList[i]);
			} else {
				SetTagOff(tagList[i]);
			}
		}

	}
	void SetTag(ObjectData data, GameObject tagObj) {
		tagObj.GetComponent<Image>().color = Color.white;
		tagObj.GetComponentInChildren<Text>().color = Color.black;
		Vector2 screen_pos = CameraPositionToScreenPosition(data.x, data.y);
		Debug.Log(Screen.width);
		Debug.Log(Screen.height);
		// convert screen postion to world postion
		RectTransform rect = this.GetComponent<RectTransform>();
		Vector3 tag_position = new Vector3();
		RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screen_pos, Camera.main, out tag_position);

		// set a tag object to that postion
		tagObj.transform.position = tag_position;
		tagObj.GetComponentInChildren<Text>().text = data.label;

	}
	void SetTagOff(GameObject tagObj) {
		tagObj.GetComponent<Image>().color = new Color(0,0,0,0);
		tagObj.GetComponentInChildren<Text>().color = new Color(0,0,0,0);
	}
	Vector2 CameraPositionToScreenPosition(float camera_pos_x, float camera_pos_y) {
		int screen_x = (int)(camera_pos_x * ((float)Screen.width / (float)camera_width));
		int screen_y = (int)((camera_height - camera_pos_y) * ((float)Screen.height / (float)camera_height));
		return new Vector2(screen_x, screen_y);

	}
}
