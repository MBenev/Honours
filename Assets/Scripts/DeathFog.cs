using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFog : MonoBehaviour
{
    [SerializeField] public Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
    int lastFrame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveFog();
        
    }

    private void MoveFog()
    {
        if(Time.frameCount > lastFrame)
        {
            lastFrame = Time.frameCount;
            if (Player.Instance.started)
            {
                gameObject.transform.Translate(direction*Time.deltaTime);
            }
        }        
    }
}
