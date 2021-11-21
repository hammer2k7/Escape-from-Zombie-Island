using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToCollection : MonoBehaviour
{
    public GameObject myplayer;
    MyController mycon;
    HealthSystem myhealth;
    public GameObject myhealthobj;
    // Start is called before the first frame update
    void Start()
    {
        mycon = myplayer.GetComponent<MyController>();
        myhealth = myhealthobj.GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {

                Debug.Log("collected loot");


                 myhealth.AddCollectPoint(1.0f);
            //this.gameObject.SetActive(false);
            this.gameObject.GetComponent<Renderer>().enabled = false;
            this.gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(RegenGem());


        }

    }

    IEnumerator RegenGem()
    {
  

        //yield on a new YieldInstruction that waits for 5 mins.
        yield return new WaitForSeconds(60*5);

        //After we have waited 5 seconds print the time again.
        this.gameObject.GetComponent<Renderer>().enabled = true;
        this.gameObject.GetComponent<Collider>().enabled = true;
    }
}
