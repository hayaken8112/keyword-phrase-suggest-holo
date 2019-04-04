using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using UnityEngine.XR.WSA.WebCam;

public class Capture : MonoBehaviour
{
    public int uploadtime = 3;
    public int captionTime = 5;
    // public GameObject quad;
    // public Texture2D targetTexture = null;
    // public Texture2D testtex = null;
    // Renderer quadRenderer = null;
    public RawImage displayImage;
    SendTexture imageUploader = null;
    WebCamTexture webcam = null;
    [SerializeField]
    SuggestionManager suggestionManager;
    public Text debugText;

    //  this for initialization
    void Start()
    {
        if(WebCamTexture.devices.Length == 0) {
            debugText.text = "no camera";
        }
        WebCamDevice[] devices = WebCamTexture.devices;
        webcam = new WebCamTexture(devices[0].name, 896, 504, 3);

        // quadRenderer = quad.GetComponent<Renderer>() as Renderer;
        // displayImage.texture = webcam;
        webcam.Play();
        imageUploader = this.GetComponent<SendTexture>();
        StartCoroutine(GetKeywords());
        StartCoroutine(GetCaptionWithKeywords());
    }


    Texture2D ConvertTextoTex2D(Texture tex)
    {
        Texture2D tex2D = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(tex.width, tex.height, 32);
        Graphics.Blit(tex, renderTexture);
        RenderTexture.active = renderTexture;
        tex2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height),0,0);
        tex2D.Apply();
        RenderTexture.active = currentRT;
        return tex2D;
    }

    IEnumerator GetKeywords()
    {
        while (true)
        {
            imageUploader.SendImageToYolo(ConvertTextoTex2D(webcam));
            yield return new WaitForSeconds(uploadtime);
        }
    }

    IEnumerator GetCaptionWithKeywords(){
        while (true) {
            Keywords keywords = new Keywords(suggestionManager.selectedWords);
            string formtext = JsonUtility.ToJson(keywords);
            Debug.Log(formtext);
            imageUploader.SendImageToServer(ConvertTextoTex2D(webcam), formtext);
            yield return new WaitForSeconds(captionTime);
        }
    }
}