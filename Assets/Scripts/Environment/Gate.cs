using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject open;
    public GameObject closed;
    public AudioClip closing;
    public GameObject player;
    public Vector3 checkpos;
    public bool check = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            if (player.transform.position == this.transform.position+(checkpos))
            {
                open.SetActive(false);
                closed.SetActive(true);
                AudioSource.PlayClipAtPoint(closing, this.transform.position, SettingsHolder.SFXvol/4);
                AudioSource.PlayClipAtPoint(closing, this.transform.position, SettingsHolder.SFXvol/4);
                AudioSource.PlayClipAtPoint(closing, this.transform.position, SettingsHolder.SFXvol / 4);
                AudioSource.PlayClipAtPoint(closing, this.transform.position, SettingsHolder.SFXvol / 4);
                check = false;
            }
        }
    }
}
