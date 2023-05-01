using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{

    public static int endState = 0;

    public static Vector3 pos = Vector3.zero;
    public static float rot = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        pos = Vector3.zero;
        rot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
