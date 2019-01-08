using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemKeyboard : MonoBehaviour
{
    UnityEngine.TouchScreenKeyboard keyboard;
    public static string keyboardText = "";
    public InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "multi-line");
    }

    // Update is called once per frame
    void Update()
    {
        inputField.text = keyboardText;
        if (keyboard != null)
        {
            keyboardText = keyboard.text;
        } 
    }

    public void ResetKeyboard(){
        if (keyboard != null){
            keyboard.text = "";
        }
    }
}
