using System;
using HoloToolkit.UI.Keyboard;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.UI;
public class KeyboardManager: MonoBehaviour, IInputClickHandler
{
    // キー入力先のTextMesh
    InputField inputField;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        inputField = this.GetComponent<InputField>();
        // すでにキーボードを開いていれば閉じる
        Keyboard.Instance.Close();
        // キーボードを表示する
        Keyboard.Instance.PresentKeyboard();
        // キーボードの位置をオブジェクトの近くに調整する
        Keyboard.Instance.RepositionKeyboard(transform, null, 0.1f);
        // キー入力値更新時のイベントを設定する
        Keyboard.Instance.OnTextUpdated += KeyboardOnTextUpdated;
        // キーボードを閉じたときのイベントを設定する
        Keyboard.Instance.OnClosed += KeyboardOnClosed;
    }
    private void KeyboardOnTextUpdated(string s)
    {
        // 入力された文字列が渡されるので、
        inputField.text = s;
    }
    private void KeyboardOnClosed(object sender, EventArgs eventArgs)
    {
        // イベントを解除する
        Keyboard.Instance.OnTextUpdated -= KeyboardOnTextUpdated;
        Keyboard.Instance.OnClosed -= KeyboardOnClosed;
    }
}