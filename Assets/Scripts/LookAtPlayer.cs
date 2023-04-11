using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject Player;
    public float clipping;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, Player.transform.position) < clipping)
        {
            var adjusted = Player.transform.position;
            adjusted = new Vector3(adjusted.x, this.transform.position.y, adjusted.z);
            transform.LookAt(adjusted);
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y - 180, this.transform.eulerAngles.z);
        }
    }
}
