using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public AudioManager myaudio;
    // Start is called before the first frame update
    void Start()
    {
        myaudio.Play("Forest", this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
