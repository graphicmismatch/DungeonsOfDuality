using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public RectTransform rt;
    private Transform th;
    // Start is called before the first frame update
    void Start()
    {
        th = rt.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rt.eulerAngles = new Vector3(rt.rotation.x, rt.rotation.y, PlayerMovement.rotation);
    }
}
