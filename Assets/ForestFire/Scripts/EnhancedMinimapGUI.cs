using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class EnhancedMinimapGUI : MonoBehaviour
{
    public Camera MinimapCamera;



    private void Start()
    {


    }
   
    void CameraZoomIncrease()
    {
        MinimapCamera.orthographicSize += 2;
    }
    void CameraZoomDecrease()
    {
        MinimapCamera.orthographicSize -= 2;
    }



    // Update is called once per frame
    void Update()
    {
    }
}
