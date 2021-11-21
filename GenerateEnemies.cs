using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateEnemies : MonoBehaviour
{
    

    public TextMeshProUGUI killstext;
   
    

    public EnemyAI enemyprefab;
    public AudioManager myaudio;
    public MyController mycon;
    public Transform enemypos;
    public int enemycount;
    public int respawnenemyevery;
    int lasttotalkills;

    void SpawnEnemies()
    {
        EnemyAI clone;
        for (int i = 0; i < enemycount; i++)
        {
            clone = Instantiate(enemyprefab, enemypos.position, Quaternion.identity);
            //Instantiate(enemyprefab, this.gameObject.transform.position, Quaternion.identity);
            clone.mycon = mycon;
            clone.sound = myaudio;
        }
    }

    void Start()
    {

        Globals.totalkills = 0;
        SpawnEnemies();
    }



    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log(lasttotalkills);
        //killstext.SetText("test");
        killstext.SetText("Killz: " + Globals.totalkills.ToString());
        if (TimeToRespawnEnemy())
        {
            SpawnEnemies();
        }
    }

    public bool TimeToRespawnEnemy()
    {
        if (lasttotalkills != Globals.totalkills)
        {
            
            if (Globals.totalkills % respawnenemyevery == 0)
            {
                lasttotalkills = Globals.totalkills;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
