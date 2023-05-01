using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + 45*Time.deltaTime, this.transform.rotation.eulerAngles.z);   
    }
}
