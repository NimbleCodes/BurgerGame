using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurOnGamePaused : MonoBehaviour
{
    bool blur = false;
    public Shader blurShad;
    Material blurMat;
    [Range(0, 16)]
    public int blurIterations;
    [Range(0, 16)]
    public int blurDownRes;

    private void Start()
    {
        //EventManager.eventManager.GamePausedEvent += OnGamePausedEvent;
        if (blurShad == null)
        {
            Debug.Log("blur shader not initialized");
            gameObject.GetComponent<BlurOnGamePaused>().enabled = false;
        }
        else
        {
            blurMat = new Material(blurShad);
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture rt = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
        if (blur)
        {
            blurFunc(source, rt);
        }
        else
        {
            Graphics.Blit(source, rt);
        }
        Graphics.Blit(rt, destination);
        RenderTexture.ReleaseTemporary(rt);
    }
    private void blurFunc(RenderTexture source, RenderTexture destination)
    {
        int width = source.width, height = source.height;
        RenderTextureFormat format = source.format;
        RenderTexture rt = RenderTexture.GetTemporary(width, height, 0, format);
        Graphics.Blit(source, rt);

        for (int i = 0; i < blurDownRes; i++)
        {
            if (height / 2 <= 1)
                break;
            RenderTexture rt2 = RenderTexture.GetTemporary(width / 2, height / 2, 0, format);
            Graphics.Blit(rt, rt2);
            Graphics.Blit(rt2, rt);
            RenderTexture.ReleaseTemporary(rt2);
        }

        for (int i = 0; i < blurIterations; i++)
        {
            RenderTexture rt2 = RenderTexture.GetTemporary(width, height, 0, format);
            Graphics.Blit(rt, rt2, blurMat);
            RenderTexture.ReleaseTemporary(rt);
            rt = rt2;
        }

        Graphics.Blit(rt, destination);
        RenderTexture.ReleaseTemporary(rt);
    }
    public void OnGamePausedEvent(string str)
    {
        if(str.Equals("Pause"))
            blur = true;
        else
            blur = false;
    } 
}
