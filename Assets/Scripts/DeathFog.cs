using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFog : MonoBehaviour
{
    [SerializeField] public Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] public float backStrength = 1000.0f;
    int lastFrame;

    public GameObject text;
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

    public void GoBack()
    {
        text.SetActive(true);
        gameObject.transform.Translate(-direction * Time.deltaTime * backStrength);
        StartCoroutine(ExampleCoroutine());
        //text.SetActive(false);
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        text.SetActive(false);
        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
