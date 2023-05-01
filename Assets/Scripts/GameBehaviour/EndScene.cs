using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public List<GameObject> endings;
    // Start is called before the first frame update
    void Start()
    {
        if (PersistentData.endState != -1)
        {
            endings[PersistentData.endState].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
