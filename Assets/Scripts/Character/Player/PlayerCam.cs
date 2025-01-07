using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCam : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public GameObject playerCenter;
    public float camMoveSpeed;

    public PlayerState.CameraView cameraView;//NEED CHANGE TO UPDATE

    //Camera Position

    public float thirdFreeCameraDistance;

    public float wheelSensitivity;


    public float minThirdFreeCameraDistance;



    // Start is called before the first frame update
    void Start()
    {
        UpdateControlSetting();
    }

    private void Update()
    {
        InputZoomInOut();
    }
    void FixedUpdate()
    {
        if (player.GetComponent<IBaseState>().isAimming)
        {
            transform.position = Vector2.Lerp(transform.position, (playerCenter.transform.position * 5 + player.GetComponent<PlayerState>().aimPosition * 2) / 7, camMoveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, playerCenter.transform.position, camMoveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }

    void UpdateControlSetting()
    {
        wheelSensitivity = player.GetComponent<PlayerState>().wheelSensitivity;
    }

    void InputZoomInOut()
    {
        //미완
        if (Input.mouseScrollDelta.y > 0)
        {
            thirdFreeCameraDistance -= wheelSensitivity;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            thirdFreeCameraDistance += wheelSensitivity;
        }

        thirdFreeCameraDistance = Mathf.Clamp(thirdFreeCameraDistance, minThirdFreeCameraDistance, 100);
    }



}
