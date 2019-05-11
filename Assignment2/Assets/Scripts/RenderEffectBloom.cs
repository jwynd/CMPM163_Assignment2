﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RenderEffectBloom : MonoBehaviour
{

    public Shader BloomShader;
    [Range(0.0f, 100.0f)]
    public float BloomFactor;
    private Material screenMat;

    Material ScreenMat
    {
        get
        {
            if (screenMat == null)
            {
                screenMat = new Material(BloomShader);
                screenMat.hideFlags = HideFlags.HideAndDontSave;
            }
            return screenMat;
        }
    }


    void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        if (!BloomShader && !BloomShader.isSupported)
        {
            enabled = false;
        }
       
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (BloomShader != null)
        {

            // Create two temp rendertextures to hold bright pass and blur pass result
            RenderTexture rt_bright = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height);
            RenderTexture rt_blur = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height);
            // Blit using bloom shader pass 0 for bright pass ( Graphics.Blit(SOURCE, DESTINATION, MATERIAL, PASS INDEX);)
            Graphics.Blit(sourceTexture, rt_bright, ScreenMat, 0);
            // Set BloomFactor to _Steps in the shader
            screenMat.SetFloat("_Steps", BloomFactor);
            // Blit using bloom shader pass 1 for blur pass ( Graphics.Blit(SOURCE, DESTINATION, MATERIAL, PASS INDEX);)
            Graphics.Blit(rt_bright, rt_blur, ScreenMat, 1);
            // Set sourceTexture to _BaseTex in the shader
            screenMat.SetTexture("_BaseTex", sourceTexture);
            // Blit using bloom shader pass 2 for combine pass ( Graphics.Blit(SOURCE, DESTINATION, MATERIAL, PASS INDEX);)
            Graphics.Blit(rt_blur, destTexture, ScreenMat, 2);
            // Release both temp rendertextures
            RenderTexture.ReleaseTemporary(rt_blur);
            RenderTexture.ReleaseTemporary(rt_bright);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        if (screenMat)
        {
            DestroyImmediate(screenMat);
        }
    }
}
