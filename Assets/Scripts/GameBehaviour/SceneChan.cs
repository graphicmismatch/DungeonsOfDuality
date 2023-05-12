using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChan : MonoBehaviour
{

    public string sc;
    public static List<string> scenes = new List<string>();
    public static float[] lightlevels = { 1, 1, 0.8f, 0.2f, 1 };
    private void Start()
    {

        scenes.Add("MainMenu");
        scenes.Add("Cutscene");
        scenes.Add("OverWorld");
        scenes.Add("Dungeon1");
        scenes.Add("End");
    }
    public void change()
    {
        SceneManager.LoadScene(sc);
        PersistentData.world = scenes.IndexOf(sc);
    }

    public static void change(string sc)
    {
        SceneManager.LoadScene(sc);
        PersistentData.world = scenes.IndexOf(sc);
    }


}
