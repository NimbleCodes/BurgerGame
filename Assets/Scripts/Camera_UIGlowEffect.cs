using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_UIGlowEffect : MonoBehaviour
{
    public Texture2D blurredTex;
    public Shader uiGlowEffect;
    Material uiGlowEffectMat;
    [Range(0,10)]
    public int intensity = 0;

    //MONOBEHAVIOUR
    private void Awake()
    {
        uiGlowEffectMat = new Material(uiGlowEffect);
        uiGlowEffectMat.EnableKeyword("_BlurredTex");
        uiGlowEffectMat.SetTexture("_BlurredTex", blurredTex);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture temp = RenderTexture.GetTemporary(Camera.main.pixelWidth,Camera.main.pixelHeight);
        Graphics.Blit(source, temp);
        for (int i = 0; i < intensity; i++)
        {
            RenderTexture temp2 = RenderTexture.GetTemporary(temp.width, temp.height);
            Graphics.Blit(temp, temp2, uiGlowEffectMat);
            RenderTexture.ReleaseTemporary(temp);
            temp = temp2;
        }
        Graphics.Blit(temp, destination);
        RenderTexture.ReleaseTemporary(temp);
    }
}
