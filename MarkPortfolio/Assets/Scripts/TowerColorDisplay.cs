using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]

public class TowerColorDisplay : MonoBehaviour
{
    private float internalTimer = 0;
    [SerializeField] private Material myMat;
    [SerializeField] private float TowerColorSpeed;
    private double lastTimeSinceStartup = 0;

    private float hue;
    private float sat;
    private float bri;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        internalTimer += (float)(EditorApplication.timeSinceStartup - lastTimeSinceStartup);
        lastTimeSinceStartup = EditorApplication.timeSinceStartup;
        if(internalTimer > 1000f)
        {
            internalTimer = 0f;
        }
        //Convert original color to HSV, edit hue based on the set speed
        Color.RGBToHSV(myMat.GetColor("_PalCol1"), out hue, out sat, out bri);
        hue += internalTimer * (TowerColorSpeed / 10000);
        if(hue >= 1f)
        {
            hue = 0f;
        }

        myMat.SetColor("_PalCol1", Color.HSVToRGB(hue,sat,bri));


        Color.RGBToHSV(myMat.GetColor("_PalCol2"), out hue, out sat, out bri);
        hue += internalTimer * (TowerColorSpeed / 10000);
        if(hue >= 1f)
        {
            hue = 0f;
        }

        myMat.SetColor("_PalCol2", Color.HSVToRGB(hue,sat,bri));
    }



    void OnDrawGizmos() {

    #if UNITY_EDITOR
        // Ensure continuous Update calls.
        if (!Application.isPlaying)
        {
            UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
            UnityEditor.SceneView.RepaintAll();
        }
    #endif
    }
}
