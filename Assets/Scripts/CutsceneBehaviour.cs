using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CutsceneBehaviour : MonoBehaviour
{
    public string scenename;
    public AudioSource clip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            endcutsene(); 
        }
        if (clip.time == clip.clip.length)
        {
            endcutsene();
        }
    }

    public void endcutsene()
    {
        SceneManager.LoadScene(scenename);
    }
}
