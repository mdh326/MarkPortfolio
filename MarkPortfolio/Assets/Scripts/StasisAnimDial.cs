using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]

//SCRIPT WAS EDITED TO WORK IN THIS PROJECT AND IN EDITOR

public class StasisAnimDial : MonoBehaviour
{
    [SerializeField] private int TexCount;
    [SerializeField] private Texture2D[] MyShadTextures;
    [SerializeField] private Texture2D[] MyEmisTextures;
    private int RGBplace = 0;
    private int TexIndex = 0;

    [SerializeField] private float TexChangeTime;
    private float myTimer;
    private double lastTimeSinceStartup = 0;



    [SerializeField] Material myStasisMat;

    // Start is called before the first frame update
    void Start()
    {
        StartTexture();

    }
    // Update is called once per frame
    void Update()
    {
        //timer set up for finding delta time even in editor
        myTimer += (float)(EditorApplication.timeSinceStartup - lastTimeSinceStartup);
        lastTimeSinceStartup = EditorApplication.timeSinceStartup;

        if (myTimer >= TexChangeTime)
        {
            NextTexture();
            myTimer = 0f;
        }


    }

    void OnDrawGizmos()
    {

#if UNITY_EDITOR
        // Ensure continuous Update calls.
        if (!Application.isPlaying)
        {
            UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
            UnityEditor.SceneView.RepaintAll();
        }
#endif
    }


    void StartTexture()
    {
        myStasisMat.SetFloat("_RGBInd", 0f);
        myStasisMat.SetTexture("_ShadMap", MyShadTextures[0]);
        myStasisMat.SetTexture("_EmisMap", MyEmisTextures[0]);
    }

    void NextTexture()
    {
        RGBplace++;

        if (RGBplace > 2)
        {
            RGBplace = 0;
            TexIndex++;
        }

        if (TexIndex * 3 + RGBplace > TexCount - 1)
        {
            RGBplace = 0;
            TexIndex = 0;
        }

        myStasisMat.SetFloat("_RGBInd", RGBplace);
        //Debug.Log(RGBplace);
        myStasisMat.SetTexture("_ShadMap", MyShadTextures[TexIndex]);
        myStasisMat.SetTexture("_EmisMap", MyEmisTextures[TexIndex]);

    }
}
