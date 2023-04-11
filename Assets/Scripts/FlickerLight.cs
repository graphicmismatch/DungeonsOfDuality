using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FlickerLight : MonoBehaviour
{
    
    public float lightIntensitymin;
    public float lightIntensitymax;
    public Light pointlight;
    private float time = 0;
    public float scrollspeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        pointlight.intensity = ((lightIntensitymax-lightIntensitymin)*Mathf.PerlinNoise1D(time* scrollspeed)) + lightIntensitymin;
        time += Time.deltaTime;
    }

    public void changelightcolor()
    {
        if (PlayerValues.monstermode)
        {
            pointlight.color = hex2color("FF6E2F");
        }
        else {
            pointlight.color = hex2color("FFDE31");
        }
    }

    public static Color hex2color(string hex)
    {
        Color32 o = new Color32();
        int r = hex2int(hex.Substring(0, 2));
        int g = hex2int(hex.Substring(2, 2));
        int b = hex2int(hex.Substring(4, 2));
        o.r = (byte)r;
        o.g = (byte)g;
        o.b = (byte)b;
        o.a = 255;
        return o;
    }

    public static int hex2int(string hex)
    {
        string reference = "0123456789ABCDEF";
        char[] workwithcharyooo = hex.ToCharArray();
        int o = 0;

        for (int i = workwithcharyooo.Length-1; i >= 0; i--)
        {
            o += reference.IndexOf(workwithcharyooo[i]) * int.Parse(Math.Pow(16, workwithcharyooo.Length - 1-i) + "");
        }

        return o;
    }
}
