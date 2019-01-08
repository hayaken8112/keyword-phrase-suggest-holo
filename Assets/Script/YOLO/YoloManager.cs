using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.XR.WSA.WebCam;

public class YoloManager : MonoBehaviour
{
    public float uploadtime = 1.0f;
	public GameObject quad = null;
    Renderer quadRenderer = null;
    SendToYolo imageUploader = null;
    WebCamTexture webcam = null;

    //  this for initialization
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        Debug.Log(devices[0]);
        webcam = new WebCamTexture(devices[0].name, 896, 504, 15);

        quadRenderer = quad.GetComponent<Renderer>() as Renderer;
        quadRenderer.material.mainTexture = webcam;
        webcam.Play();
        imageUploader = this.GetComponent<SendToYolo>();
        StartCoroutine(UploadImage());
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

    IEnumerator UploadImage()
    {
        while (true)
        {
            imageUploader.SendImageToServer(ConvertTextoTex2D(webcam));
            yield return new WaitForSeconds(uploadtime);
        }
    }
}