using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public bool started = false;

    private static Player _instance;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public GameObject[] waypoint;
    public GameObject deathFog;
    public GameObject elevation;
    public GameObject directionalLines;

    public GameObject left;
    public GameObject right;

    [SerializeField] private int collected;

    private List<string> data = new List<string>();

    [HideInInspector]
    public bool canMove = true;
    Vector3 deathFogAfterTeleport = new Vector3(0.0f, 0.0f, -60.0f);

    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Player is null");
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //Camera.main.transform.rotation = Quaternion.Euler();
        //gameObject.transform.rotation = new Quaternion(0, 0, 0, 90);
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        //bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isRunning = false;
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OutputData();
        }
    }

    public void AddCollected()
    {
        collected++;
        //GetComponent<DeathFog>().GoBack();
        deathFog.GetComponent<DeathFog>().GoBackCollectable();
    }

    public void ClearCollected()
    {
        collected = 0;
    }

    public int GetCollected()
    {
        return collected;
    }

    private void OutputData()
    {
        foreach (var x in data)
        {
            print(x.ToString());
        }


        string path = Directory.GetCurrentDirectory() + "/test.txt";
        //string path = "D:/Educational/temp/Honours" + "/test.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        foreach (var x in data)
        {
            writer.WriteLine((x.ToString()));
        }
        //writer.WriteLine("Test");
        writer.Close();
        //StreamReader reader = new StreamReader(path);
        //Print the text from the file
        //Debug.Log(reader.ReadToEnd());
        //reader.Close();
    }

    private void TeleportPlayer(int i)
    {
        characterController.enabled = false;
        //gameObject.transform.position = new Vector3(40, 2, 15);
        gameObject.transform.position = waypoint[i].transform.position;
        characterController.enabled = true;
        deathFog.transform.position=gameObject.transform.position - deathFogAfterTeleport;
        deathFog.transform.position = new Vector3(deathFog.transform.position.x, 26.2f, deathFog.transform.position.z);
        //deathFog.transform.position = (deathFog.transform.position.x, 26.2f, deathFog.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Test Point")
        {
            other.gameObject.SetActive(false);
            data.Add(other.name);
            if(other.name == "Directional Lines")
            {
                elevation.SetActive(false);
            }
            if (other.name == "Elevation")
            {
                directionalLines.SetActive(false);
            }
            if(other.name== "Final Room Right")
            {
                left.SetActive(false);
            }
            if(other.name== "Final Room Left")
            {
                right.SetActive(false);
            }
        }
        if(other.name == "Start Game Portal")
        {
            started = true;
            int i = 0;
            TeleportPlayer(i);
        }
        if(other.name == "First Room Portal")
        {
            int i = 1;
            TeleportPlayer(i);
        }
        if (other.name == "Second Room Portal")
        {
            int i = 2;
            TeleportPlayer(i);
        }
        if (other.name == "Third Room Portal ")
        {
            int i = 3;
            TeleportPlayer(i);
        }
        if (other.name == "Fourth Room Portal Left")
        {
            int i = 4;
            TeleportPlayer(i);
        }
        if (other.name == "Fourth Room Portal Right")
        {
            int i = 4;
            TeleportPlayer(i);
        }
        if (other.name == "Fifth Room Portal Left")
        {
            int i = 5;
            TeleportPlayer(i);
        }
        if (other.name == "Fifth Room Portal Right")
        {
            int i = 5;
            TeleportPlayer(i);
        }
        if (other.name == "Sixth Room Portal Left")
        {
            int i = 6;
            TeleportPlayer(i);
        }
        if (other.name == "Sixth Room Portal Right")
        {
            int i = 6;
            TeleportPlayer(i);
        }
        if (other.name == "Seventh Room Portal Left")
        {
            int i = 7;
            TeleportPlayer(i);
        }
        if (other.name == "Seventh Room Portal Right")
        {
            int i = 7;
            TeleportPlayer(i);
        }
        if (other.name == "Death Fog")
        {
            //print("crash");
            //other.transform.Translate(-other.GetComponent<DeathFog>().direction * Time.deltaTime * 10);
            data.Add("Collided with Death Fog");
            other.GetComponent<DeathFog>().GoBack();
        }
    }
}