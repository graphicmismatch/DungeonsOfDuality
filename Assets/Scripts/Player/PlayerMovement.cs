using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public static float rotation = 0;
    public bool checkinput = true;
    private bool rotating = false;
    private bool moving = false;
    private float startrot;
    private float endrot;
    private Vector3 dir = new Vector3(0, 0, 0);
    private Vector3 startpos = new Vector3(0, 0, 0);
    public Vector3 endpos = new Vector3(0, 0, 0);
    public float t = 0;

    public float timeout;
    public Transform tf;
    public AnimationCurve rotcurve;
    public AnimationCurve movcurve;

    public int queue = -1;
    public int lastinp = -1;
    private Vector3 forward = new Vector3(0, 0, 1);
    private Vector3 right = new Vector3(1, 0, 0);
    public bool dead;
    public static bool pause;
    public static PlayerMovement inst;
    public float currentrot = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        SceneManager.sceneLoaded += onsceneload;
    }
    void Start()
    {
        tf = this.GetComponent<Transform>();
        forward = new Vector3(0, 0, 1);
        right = new Vector3(1, 0, 0);
        inst = this;
        pause = false;
        
    }

    public void onsceneload(Scene scene, LoadSceneMode mode)
    {
        softreset();
        this.transform.position = PersistentData.pos;
        this.transform.eulerAngles = new Vector3(0, PersistentData.rot, 0);
    }
    private void Update()
    {
        if (!dead && !pause)
        {
            if (checkinput)
            {
                this.transform.position = new Vector3(Mathf.Round(this.transform.position.x), this.transform.position.y, Mathf.Round(this.transform.position.z));
            
            }
          
            if (Mathf.Abs(currentrot) == 360)
            {
                currentrot = 0;
            }
            if (!checkinput)
            {
                t += Time.deltaTime;

                if (rotating)
                {
                    float newrot = 0;
                    newrot = Mathf.Lerp(startrot, endrot, rotcurve.Evaluate(t / timeout));
                    tf.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, newrot, this.transform.rotation.z);
                    rotation = this.transform.rotation.eulerAngles.y;

                }

                if (moving)
                {
                    Vector3 newpos = new Vector3();
                    newpos = Vector3.Lerp(startpos, endpos, movcurve.Evaluate(t / timeout));
                    this.transform.position = newpos;

                }
                checkinput = Mathf.Approximately(t, timeout) || t > timeout;
            }

            if (checkinput)
            {
                rotating = false;
                moving = false;
                this.transform.position = new Vector3(Mathf.Round(this.transform.position.x), this.transform.position.y, Mathf.Round(this.transform.position.z));
                if (queue == -1)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {

                        t = 0;
                        checkinput = false;
                        GlobalTimekeeper.Tick();
                        lastinp = 6;
                    }
                    else if (Input.GetKey(KeyCode.Q))
                    {
                        startrot = currentrot;
                        currentrot -= 90;

                        endrot = currentrot;
                        t = 0;
                        checkinput = false;
                        rotating = true;
                        GlobalTimekeeper.Tick(0);
                        lastinp = 0;
                    }
                    else if (Input.GetKey(KeyCode.E))
                    {
                        startrot = currentrot;
                        currentrot += 90;

                        endrot = currentrot;
                        t = 0;
                        checkinput = false;
                        rotating = true;
                        GlobalTimekeeper.Tick(0);
                        lastinp = 1;
                    }
                    else if (Input.GetKey(KeyCode.W))
                    {
                        if (feasiblepath(1))
                        {
                            startpos = this.transform.position;
                            endpos = startpos + (rotate(forward, currentrot));
                            moving = true;
                        }
                        t = 0;
                        checkinput = false;
                        lastinp = 2;
                        GlobalTimekeeper.Tick();

                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        if (feasiblepath(2))
                        {
                            startpos = this.transform.position;
                            endpos = startpos + (rotate(right, currentrot));
                            moving = true;
                        }
                        t = 0;
                        checkinput = false;
                        lastinp = 3;
                        GlobalTimekeeper.Tick();

                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        if (feasiblepath(3))
                        {
                            startpos = this.transform.position;
                            endpos = startpos + (rotate(-forward, currentrot));
                            moving = true;
                        }
                        t = 0;
                        checkinput = false;

                        lastinp = 4;
                        GlobalTimekeeper.Tick();
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        if (feasiblepath(4))
                        {
                            startpos = this.transform.position;
                            endpos = startpos + (rotate(-right, currentrot));
                            moving = true;
                        }
                        t = 0;
                        checkinput = false;

                        lastinp = 5;
                        GlobalTimekeeper.Tick();
                    }
                }
                else
                {
                    t = 0;
                    checkinput = false;
                    switch (queue)
                    {
                        case 0:
                            startrot = currentrot;
                            currentrot -= 90;

                            endrot = currentrot;
                            
                            rotating = true;
                            GlobalTimekeeper.Tick(0);
                            lastinp = 0;
                            queue = -1;
                            break;
                        case 1:
                            startrot = currentrot;
                            currentrot += 90;

                            endrot = currentrot;
                           
                            rotating = true;
                            GlobalTimekeeper.Tick(0);
                            lastinp = 1;
                            queue = -1;
                            break;
                        case 2:
                            if (feasiblepath(1))
                            {
                                startpos = this.transform.position;
                                endpos = startpos + (rotate(forward, currentrot));
                                moving = true;
                            }
                            
                            lastinp = 2;
                            queue = -1;
                            GlobalTimekeeper.Tick();
                            break;
                        case 3:
                            if (feasiblepath(2))
                            {
                                startpos = this.transform.position;
                                endpos = startpos + (rotate(right, currentrot));
                                moving = true;
                            }
                         
                            lastinp = 3;
                            queue = -1;
                            GlobalTimekeeper.Tick();
                            break;
                        case 4:
                            if (feasiblepath(3))
                            {
                                startpos = this.transform.position;
                                endpos = startpos + (rotate(-forward, currentrot));
                                moving = true;
                            }
                 
                            queue = -1;
                            lastinp = 4;
                            GlobalTimekeeper.Tick();
                            break;
                        case 5:
                            if (feasiblepath(4))
                            {
                                startpos = this.transform.position;
                                endpos = startpos + (rotate(-right, currentrot));
                                moving = true;
                            }
                            
                            queue = -1;
                            lastinp = 5;
                            GlobalTimekeeper.Tick();
                            break;
                        default:
                            break;
                    }

                }

            }
            else if (queue == -1)
            {

                if (Input.GetKey(KeyCode.Q) && lastinp != 0)
                {
                    queue = 0;
                }
                else if (Input.GetKey(KeyCode.E) && lastinp != 1)
                {
                    queue = 1;
                }
                else if (Input.GetKey(KeyCode.W) && lastinp != 2)
                {
                    queue = 2;
                }
                else if (Input.GetKey(KeyCode.D) && lastinp != 3)
                {
                    queue = 3;
                }
                else if (Input.GetKey(KeyCode.S) && lastinp != 4)
                {
                    queue = 4;
                }
                else if (Input.GetKey(KeyCode.A) && lastinp != 5)
                {
                    queue = 5;
                }
            }
        }

    }
    // Update is called once per frame
    

    public Vector3 rotate(Vector3 inp, float angle)
    {
        float turn = angle / 90;
        Vector3 outp = new Vector3();

        if (turn % 2 == 0)
        {
            outp = new Vector3(inp.x, inp.y, inp.z);

        }
        else
        {
            outp = new Vector3(inp.z, inp.y, inp.x);
        }

        if ((turn == -1 || turn == -2 || turn == 2 || turn == 3) && (inp == forward || inp == -forward))
        {
            outp = -outp;
        }
        if ((turn == 1 || turn == 2 || turn == -2 || turn == -3) && (inp == right || inp == -right))
        {
            outp = -outp;
        }
        return outp;
    }

    public bool feasiblepath(int inp)
    {
        
        RaycastHit hit = new RaycastHit();
        Vector3 dir = new Vector3();
        if (inp == 1)
        {
            dir = this.transform.forward;
        }
        else if (inp == 2)
        {
            dir = this.transform.right;
        }
        else if (inp == 3)
        {
            dir = -this.transform.forward;
        }
        else
        {
            dir = -this.transform.right;
        }

        bool hits = Physics.Linecast(this.transform.position, this.transform.position + dir, out hit, (1 << 7));
        if (hits)
        {
            if (!CameraShake.shaking)
            {
                CameraShake.shake(timeout * 0.4f);
            }
            return false;

        }

        return true;
    }
    public void die()
    {
        dead = true;
        softreset();
    }

    public void revive()
    {
        dead = false;
        startpos = this.transform.position;
        startrot = this.transform.rotation.eulerAngles.y;
        softreset();
    }

    public void softreset()
    {
        checkinput = true;
        moving = false;
        rotating = false;
        t = 0;
        queue = -1;
        endpos = startpos;
        endrot = startrot;
    }
    public static void togglePause()
    {
        pause = !pause;
    }
    public void pauseunpause()
    {
        pause = !pause;
    }
}



