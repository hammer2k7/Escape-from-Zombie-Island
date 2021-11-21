using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioManager music;
    public TextMeshProUGUI defeatmessage;
    public MyController mycon;
    public GenerateEnemies gen;
    public HealthSystem myhealth;
    public TextMeshProUGUI leveltext;
    public TextMeshProUGUI congrats;


    void Awake()
    {
        StartCoroutine(AnnounceLevel());
        /*
        if (Globals.level == 1)
        {
            myhealth.timerseconds = 60*15f;
            myhealth.maxCollectPoint = 4f;
            gen.enemycount = 3;
            gen.respawnenemyevery = 3;
        }
        if (Globals.level == 2)
        {
            myhealth.timerseconds = 200.0f;
            myhealth.maxCollectPoint = 30f;
            gen.enemycount = 20;
            gen.respawnenemyevery = 3;
        }
        */
        myhealth.timerseconds = 60 * 7f - 5f*(float)(Globals.level)*.8f;
        myhealth.maxCollectPoint = 4*(float)(Globals.level)*.5f;
        gen.enemycount = 3 + Globals.level;
        gen.respawnenemyevery = 3;
    }

    IEnumerator AnnounceLevel()
    {
        
        leveltext.text = "Level " + Globals.level;
        leveltext.gameObject.SetActive(true);

        //yield on a new YieldInstruction that waits for 5 mins.
        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.
        leveltext.gameObject.SetActive(false);
    }

    void Start()
    {
        if (this.gameObject != null)
        {
            music.Play("Music", this.gameObject);
        }
        Globals.isdead = false;
        defeatmessage.gameObject.SetActive(false);
        /*
        if (Globals.level == 1)
        {
            myhealth.timerseconds = 170.0f;
            gen.enemycount = 6;
            gen.respawnenemyevery = 3;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.isdead)
        {
            Globals.isdead = false;
            defeatmessage.gameObject.SetActive(true);
        }
        if (myhealth.collectPoint == myhealth.maxCollectPoint)
        {
            
            StartCoroutine(NextLevel());
            
        }
    }

    IEnumerator NextLevel()
    {

        //leveltext.text = "Congratulations, You Escaped from this Level!";
        //leveltext.gameObject.SetActive(true);
        /*
        if (this.gameObject != null)
        {
            music.Play("Win", congrats.gameObject);
            
        }
        */
        congrats.gameObject.SetActive(true);

        //yield on a new YieldInstruction that waits for 5 mins.
        yield return new WaitForSeconds(4);
        congrats.gameObject.SetActive(false);
        Globals.level++;
        SceneManager.LoadScene(2);
        //After we have waited 5 seconds print the time again.
        
    }

    public void RestartLevel()
    {
        //Globals.level = 2;
        SceneManager.LoadScene(2);
 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

public static class Globals
{
    public static int totalkills = 0;
    public static bool isdead = false;
    public static int level = 1;
    public static bool iswin = false;
  
}


