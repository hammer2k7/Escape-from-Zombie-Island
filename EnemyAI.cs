using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioManager sound;
    

    Animator myanim;
    NavMeshAgent myagent;
    public MyController mycon;
    float mydist;
    bool isDead;
    bool isPlayerdead;

    public void PlayAttackSound()
    {
        if (this.gameObject != null)
        {
            sound.PlayOneShot("Zombiesound", this.gameObject);
            Debug.Log("Playing attack sound");
        }
    }
    void Start()
    {
        myanim = GetComponent<Animator>();
        myagent = GetComponent<NavMeshAgent>();
        isDead = false;
        isPlayerdead = false;
                //agent.Move(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && !isPlayerdead)
        {
            
                
            mydist = Vector3.Distance(myagent.transform.position, mycon.transform.position);
            //if (myagent.velocity.x > 0f || myagent.velocity.z > 0f || myagent.velocity.y > 0f)
            if (mydist > 2.0f)
            {
                myanim.SetBool("IsAttacking", false);
                myanim.SetBool("IsRunning", true);
                myagent.destination = mycon.transform.position;
            }
            else
            {
                myanim.SetBool("IsRunning", false);
                myanim.SetBool("IsAttacking", true);
                
            }
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
        //Debug.Log(mydist);
    }
    

    private void FixedUpdate()
    {
      
    }

    void OnTriggerStay(Collider other)
    {
        if (!isDead)
        {
            MyController mycon;
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("caught player");

                mycon = other.gameObject.GetComponent<MyController>();
                mycon.ApplyDamage(20.0f);
                if (mycon.GetDeadStatus())
                {
                    myanim.SetBool("IsAttacking", false);
                    myanim.SetBool("IsRunning", false);
                    isPlayerdead = true;
                }

            }
        }

    }

    public void ApplyDamage(float damage)
    {
        if (damage > 0f)
        {
            isDead = true;
            myanim.SetBool("IsAttacking", false);
            myanim.SetBool("IsRunning", false);
            myanim.SetBool("IsDead", true);

        }
    }

    public void DestroyObject()
    {
        
        Globals.totalkills++;
        Destroy(this.gameObject);
        
    }
}
