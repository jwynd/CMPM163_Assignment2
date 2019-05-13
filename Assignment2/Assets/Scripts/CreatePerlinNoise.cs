using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePerlinNoise : MonoBehaviour
{
    [SerializeField] private int pixWidth;
    [SerializeField] private int pixHeight;
    [SerializeField] private float minScale = 3.0f;
    [SerializeField] private float maxScale = 8.0f;
    private float xOrg;
    private float yOrg;
    private float scale;
    private Texture2D noiseTex;
    private Color[] pix;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        print(mat);
        xOrg = Random.Range(0.0f, 100.0f);
        yOrg = Random.Range(0.0f, 100.0f);
        scale = Random.Range(minScale, maxScale);
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        CalcNoise();
        mat.SetTexture("_HeightMapTex", noiseTex);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.N)){
            xOrg = Random.Range(0.0f, 100.0f);
            yOrg = Random.Range(0.0f, 100.0f);
            scale = Random.Range(minScale, maxScale);
            CalcNoise();
            mat.SetTexture("_HeightMapTex", noiseTex);
        }
    }

    private void CalcNoise()
    {
        // For each pixel in the texture...
        float y = 0.0F;

        while (y < noiseTex.height)
        {
            float x = 0.0F;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }

        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }

    /*void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture){
        mat.SetTexture("_HeightMapTex", noiseTex);
        Graphics.Blit(sourceTexture, destTexture, mat);
    }*/
}
