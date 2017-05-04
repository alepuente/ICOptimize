using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMobile : IInput{

    Ray ray;
    RaycastHit hit;

    public Vector3 getSelection()
    {
        if (Input.touchCount > 0)
        {
            ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Water")
                {
                    return hit.point;
                }
            }
        }
        return Vector3.zero;
    }
}
