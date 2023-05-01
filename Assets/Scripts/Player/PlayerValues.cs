using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PlayerValues : MonoBehaviour
{

    public static bool monstermode = false;
    public static bool canTransform = true;
    public static bool isPoisoned;
    public static float health = 100;
    public static float maxhealth = 100;
    public static float morality = 100;
    public static float maxMorality = 100;
    public static float TransformTimer;
    private static float TimeSinceTransform;
    public float moralityDrainPerTickInMonsterMode;
    public float poisonDamage;
    public static float poisoncounter;
    public UnityEvent OnSwitchMode;
    public UnityEvent OnDie;
    public AudioClip pray;
    public AudioClip hurts;
    public AudioClip heal;
    public Animator fade;
    public static bool hassaved;
    public Vector3 savedpos;
    public Quaternion savedrot;
    public bool dead;
    public static PlayerValues inst;
    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        TransformTimer = 20;
        TimeSinceTransform = TransformTimer;
        hassaved = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (health <= 0)
        {
            dead = true;
            OnDie.Invoke();
        }
        if (calcMoralityFill() >= 0.65f)
        {
            UIUpdater.UpdateStatus("You Feel Motivated");
        }
        else if (calcMoralityFill() >= 0.30f)
        {
            UIUpdater.UpdateStatus("You Feel Scared");
        }
        else if (calcMoralityFill() < 0.30f)
        {
            UIUpdater.UpdateStatus("SOMETHING IS MISSING.", Color.red);
        }

        if (this.transform.position == new Vector3(4, this.transform.position.y, 52))
        {
            UIUpdater.UpdateStatus("YOu FeEl GOoFy.", Color.green);
        }
        if (!dead)
        {
            if (Input.GetKey(KeyCode.R) && canTransform)
            {
                monstermode = !monstermode;
                canTransform = false;
                TimeSinceTransform = 0;
                OnSwitchMode.Invoke();
                fade.Play("Fade");
                GlobalTimekeeper.Tick(1);
            }
            if (Input.GetKeyDown(KeyCode.F) && !monstermode && PlayerMovement.inst.checkinput && !PlayerMovement.pause)
            {
                

                RaycastHit[] r = Physics.RaycastAll(this.transform.position, this.transform.forward, 1.1f);
                if (r.Length > 0)
                {
                    foreach (RaycastHit i in r)
                    {
                        if (i.transform.tag == "altar")
                        {
                            AudioSource.PlayClipAtPoint(pray, this.transform.position);
                            PlayerMovement.inst.t = 0;
                            PlayerMovement.inst.checkinput = false;
                            if (calcMoralityFill() >= 0.30f)
                            {
                                Hurt(-1000);
                                if (calcMoralityFill() >= 0.65f)
                                {
                                    hassaved = true;
                                    savedpos = this.transform.position;
                                    savedrot = this.transform.rotation;
                                }
                            }
                        }
                        if (i.transform.tag == "Finish")
                        {
                            if (calcMoralityFill() >= 0.65f)
                            {
                                PersistentData.endState = 3;
                            }
                            else if (calcMoralityFill() < 0.30f)
                            {
                                PersistentData.endState = 2;
                            }
                            else
                            {
                                PersistentData.endState = 1;
                            }
                            SceneChan.change("End");
                        }
                    }
                }

            }

            if (Input.GetKeyDown(KeyCode.F) && monstermode && PlayerMovement.inst.checkinput && !PlayerMovement.pause)
            {


                RaycastHit[] r = Physics.RaycastAll(this.transform.position, this.transform.forward, 1);
                if (r.Length > 0)
                {
                    foreach (RaycastHit i in r)
                    {
                        if (i.transform.tag == "Enemy")
                        {
                            i.transform.gameObject.GetComponent<Enemy>().die();
                            Immoral(3);
                        }
                        if (i.transform.tag == "Finish")
                        {
                            if (calcMoralityFill() >= 0.65f)
                            {
                                SceneChan.change("GoodEnd");
                            }
                            else if (calcMoralityFill() < 0.30f)
                            {
                                SceneChan.change("BadEnd");
                            }
                            else 
                            {
                                SceneChan.change("MidEnd");
                            }
                        }
                    }
                }

            }
        }
    }

    public static float calcHealthFill()
    {
        return health / maxhealth;
    }

    public static float calcMoralityFill()
    {
        return morality / maxMorality;
    }

    public static float calcTransformTimerFill()
    {
        return TimeSinceTransform / TransformTimer;
    }

    public static void Hurt(float x)
    {
        if (x > 0)
        {
            AudioSource.PlayClipAtPoint(inst.hurts, inst.transform.position,SettingsHolder.SFXvol);
        }
        if (x < 0)
        {
            AudioSource.PlayClipAtPoint(inst.heal, inst.transform.position, SettingsHolder.SFXvol);
        }
        health -= x;
        if (health <= 0)
        {
            health = 0;
        }
        if (health >= maxhealth)
        {
            health = maxhealth;
        }

    }
    public static void Poison(float x)
    {
        isPoisoned = true;
        poisoncounter = x;

    }
    public static void Immoral(float x)
    {
        morality -= x;
        if (morality <= 0)
        {
            morality = 0;
        }
        if (morality >= maxMorality)
        {
            morality = maxMorality;
        }

    }
    public void tick()
    {

        if (monstermode && morality > 0)
        {
            Immoral(moralityDrainPerTickInMonsterMode);
            if (canTransform)
            {
                Immoral(0.2f);
            }
        }

        if (!canTransform)
        {
            TimeSinceTransform += 1;
            canTransform = TimeSinceTransform >= TransformTimer;
        }

        if (isPoisoned)
        {
            poisoncounter--;
            Hurt(poisonDamage);

            isPoisoned = poisoncounter > 0;
        }
    }

    public void revive()
    {
        health = maxhealth / 2;
        this.transform.position = savedpos;
        this.transform.rotation = savedrot;
        dead = false;
    }
}
