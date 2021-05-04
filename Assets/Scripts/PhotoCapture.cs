using SO.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [SerializeField] GameObject selfieCM;
    [SerializeField] Camera renderCam;
    [SerializeField] EventSO onOnScreenshotIsCaptured;
    [SerializeField] RenderTexture captureRT;
    [SerializeField] RawImage selfieImage;
    Coroutine captureCO;
    private void Awake()
    {
        renderCam.gameObject.SetActive(false);
    }
    public void CapturePhoto()
    {
        renderCam.transform.position = selfieCM.transform.position;
        renderCam.transform.rotation = selfieCM.transform.rotation;

        renderCam.gameObject.SetActive(true);

        captureCO = StartCoroutine(Capture());
    }
    IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture2D = new Texture2D(captureRT.width, captureRT.height);
        renderCam.Render();

        RenderTexture.active = captureRT;
        texture2D.name = "captured_selfie";
        texture2D.ReadPixels(new Rect(0, 0, captureRT.width, captureRT.height), 0, 0);
        texture2D.Apply();

        selfieImage.texture = texture2D;

        RenderTexture.active = null;

        onOnScreenshotIsCaptured.Raise();

        StopCoroutine(captureCO);
    }
    //private void OnPostRender()
    //{
    //    RenderTexture.active = captureRT;

    //    Texture2D texture2D = new Texture2D(captureRT.width, captureRT.height);
    //    texture2D.name = "captured_selfie";
    //    texture2D.ReadPixels(new Rect(0, 0, captureRT.width, captureRT.height), 0, 0);
    //    texture2D.Apply();
    //    texture2D.EncodeToPNG();

    //    selfieImage.texture = texture2D;
    //}
}
