using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{

    public float xrotmax;
    public float yrotmax;
    private Vector2 mouse;
    public GameObject pivot;
    
    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(1))
        {

            pivot.transform.localRotation = vector2quaternion(new Vector3(0, 0, 0));
        }
            if (Input.GetMouseButtonDown(1))
        {
            mouse = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector2 tmouse = new Vector2(mouse.x - Input.mousePosition.x, mouse.y - Input.mousePosition.y);
            tmouse = clamp(tmouse.y, tmouse.x);
            pivot.transform.localRotation = vector2quaternion(new Vector3(Mathf.Clamp(tmouse.x,-xrotmax, xrotmax), Mathf.Clamp(-tmouse.y, -yrotmax, yrotmax), 0));

        }

        if (Input.GetMouseButtonUp(1))
        {
            pivot.transform.localRotation = vector2quaternion(new Vector3(0, 0, 0));
        }
    }

    Vector2 clamp(float d, float f)
    {
        float xt = 0;
        float yt = 0;

        xt = (yrotmax) * d / (Screen.currentResolution.height / 5f);
        yt = (xrotmax) * f / (Screen.currentResolution.width / 7f);

        return new Vector2(xt, yt);
    }

    Quaternion vector2quaternion(Vector3 v)
    {
        Quaternion q = new Quaternion();
        q.eulerAngles = v;
        return q;
    }
}
