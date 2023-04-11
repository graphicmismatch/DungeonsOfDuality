using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;
public class UIUpdater : MonoBehaviour
{
    public Image health;
    public Image morality;
    public Image transformation;
    public GameObject Poison;
    public GameObject CanTransform;
    public static UIUpdater inst;
    public GameObject HumanHand;
    public GameObject MonsterHand;
    public TMP_Text status;
    public GameObject HumanPortrait;
    public GameObject MonsterPortrait;

    public GameObject deadscreen;
    public GameObject goodDeath;
    public GameObject badDeath;

    public Material skybox;
    public VolumeParameter post;

    public string lastUpdate = "";
    // Start is called before the first frame update
    void Start()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
       

        health.fillAmount = PlayerValues.calcHealthFill();
        morality.fillAmount = PlayerValues.calcMoralityFill();
        if (!PlayerValues.canTransform)
        {
            transformation.fillAmount = PlayerValues.calcTransformTimerFill();
            CanTransform.SetActive(false);
        }
        else
        {
            transformation.fillAmount = 1;
            CanTransform.SetActive(true);
        }
        Poison.SetActive(PlayerValues.isPoisoned);
        Color test = skybox.GetColor("_Tint");
        float h, s, v;
        Color.RGBToHSV(test, out h, out s, out v);
        s = (1f- morality.fillAmount);
        skybox.SetColor("_Tint", Color.HSVToRGB(h, s, v));
        
    }

    public void trans()
    {
        MonsterHand.SetActive(PlayerValues.monstermode);
        MonsterPortrait.SetActive(PlayerValues.monstermode);
        HumanHand.SetActive(!PlayerValues.monstermode);
        HumanPortrait.SetActive(!PlayerValues.monstermode);
    }

    public void die()
    {
        deadscreen.SetActive(true);
        if (PlayerValues.calcMoralityFill() >= 0.5f && PlayerValues.hassaved)
        {
            
            goodDeath.SetActive(true);
            badDeath.SetActive(false);
        }
        else {
            goodDeath.SetActive(false);
            badDeath.SetActive(true); ;

        }
    }
    private void OnApplicationQuit()
    {
        Color test = skybox.GetColor("_Tint");
        float h, s, v;
        Color.RGBToHSV(test, out h, out s, out v);
        s = 0;
        skybox.SetColor("_Tint", Color.HSVToRGB(h, s, v));

    }

    public static void UpdateStatus(string a)
    {
        if (a != inst.lastUpdate && a != inst.status.text)
        {
            inst.lastUpdate = inst.status.text;
            inst.status.text = a;
        }
    
    }

    public static void UpdateStatus(string a, Color c)
    {
        if (a != inst.lastUpdate && a != inst.status.text)
        {
            inst.lastUpdate = inst.status.text;
            inst.status.text = a;
            inst.status.color = c;
        }

    }
}
