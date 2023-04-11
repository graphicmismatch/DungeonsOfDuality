using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private int tickdelay;
    private bool called;
    private bool nexttick;
    private bool damagedealt;
    private Animator anim;
    public bool active;
    public float trapdamage;
    public float poisonchance;
    public int poisonturns;
    public BoxCollider coll;
    public AudioClip act;
    public AudioClip deact;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        called = false;
        active = false;
        nexttick = false;
        damagedealt = false;
        GlobalTimekeeper.inst.dotick.AddListener(tick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activate()
    {
        active = false;
        called = true;
        nexttick = false;
        damagedealt = false;
        
    }

    public void tick()
    {
       

       

        if (called && !active )
        {
  
                anim.SetBool("activated", true);
                anim.Play("spikesActivate");
                AudioSource.PlayClipAtPoint(act, this.transform.position);
                active = true;
                called = false;
               
        }

        

        if (active && nexttick)
        {
            Collider[] r = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);

            if (r.Length > 0)
            {
                foreach (Collider i in r)
                {
                    if (i.transform.tag == "Player")
                    {
                        PlayerValues.Hurt(trapdamage);
                        if (Random.Range(1f, 100f) <= poisonchance)
                        {
                            PlayerValues.Poison(poisonturns);
                        }
                    }
                    else if (i.transform.tag == "Enemy")
                    {
                        i.transform.gameObject.GetComponent<Enemy>().die();
                    }

                }
            }

            damagedealt = true;
        }

        if (active && !called && damagedealt)
        {

            anim.SetBool("activated", false);

            AudioSource.PlayClipAtPoint(deact, this.transform.position);
            active = false;
            called = false;
            damagedealt = false;
            
        }
        nexttick = true;
       
    }

    
}
