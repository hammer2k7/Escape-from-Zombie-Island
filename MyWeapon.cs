using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWeapon : MonoBehaviour
{
    
    public GameObject myplayer;
    MyController mycon;
    HealthSystem myhealth;
    public GameObject myhealthobj;
    int totalkills;
    // Start is called before the first frame update
    void Start()
    {
        mycon = myplayer.GetComponent<MyController>();
        myhealth = myhealthobj.GetComponent<HealthSystem>();
        totalkills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getTotalKills()
    {
        return totalkills;
    }

    void OnTriggerStay(Collider other)
    {
        EnemyAI myenemy;
        
        if (other.gameObject.tag == "Enemy")
        {
            if (mycon.GetAttackStatus())
            {

                Debug.Log("weapon hit enemy");
                myenemy = other.gameObject.GetComponent<EnemyAI>();
                myenemy.ApplyDamage(20.0f);
                myhealth.RestoreMana(5.0f);
                totalkills++;
            }

        }

    }
}
