using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMousePoints : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 mousePos = Vector3.zero;

        if (mouseRay.direction.y != 0)
        {
            float dstToXZPlane = Mathf.Abs(mouseRay.origin.y / mouseRay.direction.y);
            mousePos = mouseRay.GetPoint(dstToXZPlane);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //generator.AddPoint(mousePos);
        }
    }
}
