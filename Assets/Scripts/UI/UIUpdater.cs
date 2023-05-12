using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.SceneManagement;
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
    public GameObject torchholder;
    public GameObject deadscreen;
    public GameObject goodDeath;
    public GameObject badDeath;
    public AudioSource asMus;
    public Material skybox;
    public GameObject post;
    public GameObject pausemenu;
    public string lastUpdate = "";
    // Start is called before the first frame update

    private void Awake()
    {
        SceneManager.sceneLoaded += onsceneload;
    }
    void Start()
    {
        inst = this;
    }
    public void onsceneload(Scene scene, LoadSceneMode mode)
    {
        if (PersistentData.lightlevel > 0.5f)
        {
            torchholder.SetActive(false);
        }
        else 
        {
            torchholder.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausemenu.SetActive(true);
            PlayerMovement.togglePause();
        }

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

    public static void updateSFX(float vol)
    {
        SettingsHolder.SFXvol = vol;
    }

    public static void updateMus(float vol)
    {
        SettingsHolder.Musvol = vol;
        inst.asMus.volume = SettingsHolder.Musvol;
    }

    public static void moodymode(bool x)
    {
        inst.post.SetActive(x);
    }
}
