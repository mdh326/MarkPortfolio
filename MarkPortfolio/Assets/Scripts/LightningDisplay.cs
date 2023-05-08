using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]

//SCRIPT JUST FOR DISPLAYING THE LIGHTNING IN SCENE

public class LightningDisplay : MonoBehaviour
{
    private float internalTimer = 0;
    [SerializeField] private Material myMat;
    [SerializeField] private float flipSpeedMult;
    [SerializeField] private float waitTime;
    private double lastTimeSinceStartup = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Flipbook shader goes from 0 to 1.5; add to internal timer based on speed multiplier
        if (internalTimer < 1.5) {
            internalTimer += (float)(EditorApplication.timeSinceStartup - lastTimeSinceStartup) * flipSpeedMult;
            lastTimeSinceStartup = EditorApplication.timeSinceStartup;
            myMat.SetFloat("_FlipValue", internalTimer);
        } else if (internalTimer < 1.5 + waitTime && internalTimer >= 1.5) { //Wait before Firing Again
            internalTimer += (float)(EditorApplication.timeSinceStartup - lastTimeSinceStartup);
            lastTimeSinceStartup = EditorApplication.timeSinceStartup;
        } else {
            internalTimer = 0f;
        }
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
