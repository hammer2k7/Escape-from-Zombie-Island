using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyController : MonoBehaviour
{
    public AudioManager myaudio;
    public MyWeapon myweap1;
    public MyWeapon myweap2;

    private Animator myanim;
    private CharacterController mychar;
    private Vector3 move = Vector3.zero;
    private bool isAttacking = false;
   
    public Transform cam;

    public float speed;
    public float gravity;
    public float jumpHeight;
    public float numrocks;
    Vector3 velocity;
    bool isGrounded;
    bool isDead;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    int totalkills;

    public GameObject healthsystem;
    HealthSystem myhealth;



    // Start is called before the first frame update
    void Start()
    {
        myanim = GetComponent<Animator>();
        mychar = GetComponent<CharacterController>();
        isDead = false;
        
        myhealth = healthsystem.GetComponent<HealthSystem>();
        numrocks = 0;
        totalkills = 0;
    }


    public int GetTotalKills()
    {
        return totalkills;
    }

    public bool GetAttackStatus()
    {
        return isAttacking;
    }

    public bool GetDeadStatus()
    {
        return isDead;
    }

    public void PlayWeakAttackSound()
    {
        myaudio.PlayOneShot("WeakAttack", this.gameObject);
    }

    public void PlayStrongAttackSound()
    {
        myaudio.PlayOneShot("StrongAttack", this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (myhealth.hitPoint < 0.01f)
        {
            isDead = true;
            myanim.SetBool("IsWeakAttack", false);
            myanim.SetBool("IsStrongAttack", false);
            myanim.SetBool("IsDead", true);
            Globals.isdead = true;
        }


        if (!isDead)
        {
            //if (mychar.isGrounded)
            if (isPlaying("Weak Attack") || isPlaying("Strong Attack"))
            {
                isAttacking = true;

            }
            else
            {
                isAttacking = false;
            }


            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                if (!isAttacking && isGrounded)
                {
                    myanim.SetBool("IsRunning", true);
                }
            }
            else
            {
                myanim.SetBool("IsRunning", false);
            }
            if (Input.GetButton("Jump") && isGrounded)
            {
                if (!isAttacking)
                {
                    myanim.SetBool("IsJumping", true);
                }
            }
            else
            {
                myanim.SetBool("IsJumping", false);
            }
            if (Input.GetMouseButton(0))
            {
                myanim.SetBool("IsWeakAttack", true);
                //myaudio.PlayOneShot("WeakAttack", this.gameObject);
            }
            else
            {
                myanim.SetBool("IsWeakAttack", false);
            }
            if (Input.GetMouseButton(1))
            {
                myanim.SetBool("IsStrongAttack", true);
                
            }
            else
            {
                myanim.SetBool("IsStrongAttack", false);
            }

            if (Input.GetButton("Fire1"))
            {

                gravity = -3000f;
            }
            else
            {
                gravity = -900f;
            }



            /*
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //move.y -= 20.0f * Time.deltaTime;
            transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * 90.0f, 0);
            move = transform.TransformDirection(move);
            mychar.Move(move * Time.deltaTime * 10.0f);
            */

            //jump
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                
            }
            

            if (myhealth.manaPoint >= 500.0f)
            {
                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                    myhealth.UseMana(500.0f);
                    myaudio.PlayOneShot("Jump", this.gameObject);
                }
            }
        
            //gravity
            velocity.y += gravity * Time.deltaTime;
            mychar.Move(velocity * Time.deltaTime);
            //walk
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f && isAttacking == false)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                mychar.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }

    }

    bool isPlaying(string stateName)
    {
        /*
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
        */

        return myanim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        /*
        if (myanim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            return true;
        }
        else
        {
            return false;
        }
        */
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gem")
        {
            myaudio.PlayOneShot("Gem", this.gameObject);
            myhealth.HealDamage(2000.0f);
            
        }
        else
        {
            if (other.gameObject.tag == "Abyss")
            {
                myhealth.hitPoint = 0f;

            }
        }
       
    }


   

    public void ApplyDamage(float damage)
    {
        
        myhealth.TakeDamage(damage);
        

    }
}
