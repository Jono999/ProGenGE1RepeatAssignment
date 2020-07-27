using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MousePositionOnClick : MonoBehaviour
{
    // The current position of the mouse in pixel coordinates
    public Vector3 firstMousePos;
    //Whether or not the mouse has been clicked
    private bool mouseClicked;
 
    // Update is called once per frame
    void Update ()
    {
        // If left click is pressed & we have not already clicked the button previously
        if (Input.GetMouseButtonDown(0) && mouseClicked == false)
        {
            mouseClicked = true;
            firstMousePos = Input.mousePosition;
        }
    }
}
