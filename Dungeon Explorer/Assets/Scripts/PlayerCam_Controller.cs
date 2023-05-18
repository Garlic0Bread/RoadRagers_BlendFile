using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam_Controller : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float smoothness_Mouse;

    private Vector2 currentLooking_Pos;
    private Vector2 Velocity_smoothed;

    private GameObject Player;
    public GameObject FloatingBox;
    public GameObject Box;

    void Start()
    {
        Player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    { 
        Floating_Block();
        Delete_Block();
        Cam_Rotation();
        Block();
        Hit();
    }

    private void Block()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Floor")
                {
                    Instantiate(Box, hit.point, Quaternion.identity);
                }

                if (hit.transform.tag == "Fire")
                {
                    Instantiate(Box, hit.point, Quaternion.identity);
                }
            }
        }
    }
    private void Floating_Block()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Floor")
                {
                    Instantiate(FloatingBox, hit.point, Quaternion.identity);
                }                
            }
        }
    }

    private void Delete_Block()
    {
        if (Input.GetMouseButtonDown(2))
        {
            GameObject[] Block = GameObject.FindGameObjectsWithTag("Block");
            foreach (GameObject block in Block)
                GameObject.Destroy(block);
        }
    }

    private void Hit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit what_Hit;
            if(Physics.Raycast(transform.position, transform.forward, out what_Hit, Mathf.Infinity))
            {
                Debug.Log(what_Hit.collider.name);
            }
        }
    }
    private void Cam_Rotation()
    {
        Vector2 Input_Values = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Input_Values = Vector2.Scale(Input_Values, new Vector2(mouseSensitivity * smoothness_Mouse, mouseSensitivity * smoothness_Mouse));

        Velocity_smoothed.x = Mathf.Lerp(Velocity_smoothed.x, Input_Values.x, 1f / smoothness_Mouse);
        Velocity_smoothed.y = Mathf.Lerp(Velocity_smoothed.y, Input_Values.y, 1f / smoothness_Mouse);

        currentLooking_Pos += Velocity_smoothed;
        currentLooking_Pos.y = Mathf.Clamp(currentLooking_Pos.y, -80f, 80f);

        transform.localRotation = Quaternion.AngleAxis(-currentLooking_Pos.y, Vector3.right);
        Player.transform.localRotation = Quaternion.AngleAxis(currentLooking_Pos.x, Vector3.up);
    }
    
}
