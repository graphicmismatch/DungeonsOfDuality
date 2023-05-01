using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressureplate : MonoBehaviour
{

    private Vector3 thispos;
    private PlayerMovement player;
    public bool activated;
    public Trap trap;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        thispos = this.transform.position;
        GlobalTimekeeper.inst.dotick.AddListener(tick);
        anim = this.GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("steppedon", activated);
    }

    void tick()
    {
        if (player.endpos.x == thispos.x && player.endpos.z == thispos.z)
        {
            if (!activated)
            {
                activated = true;
                anim.Play("plateonstep");
                trap.activate();
            }
        }
        else
        {

            activated = false;
        }
    }
}
