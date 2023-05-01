using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBobbing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, ((Mathf.PerlinNoise1D(Time.time)*0.1f) +0.6f) ,this.transform.position.z);
    }
}
