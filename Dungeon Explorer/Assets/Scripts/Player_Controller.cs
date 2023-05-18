using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jump_Force;
    [SerializeField] private float raycast_JumpDistance;

    private Rigidbody rb;
    public HealthBar my_Health;
    private int player_Health = 100;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        Jump();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Is_Grounded())
            {
                rb.AddForce(0, jump_Force, 0, ForceMode.Impulse);
            }          
        }
    }

    private bool Is_Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycast_JumpDistance);
    }

    private void Move()
    {
        float V_Axis = Input.GetAxisRaw("Vertical");
        float H_Axis = Input.GetAxisRaw("Horizontal");

        Vector3 movement = new Vector3(H_Axis, 0, V_Axis) * speed * Time.fixedDeltaTime;
        Vector3 new_Position = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(new_Position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Trigger")
        {
            Debug.Log("YEEEE");
            GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject);           
        }

        if (other.gameObject.tag == "Fire")
        {
            Debug.Log("Fire works");
            player_Health = player_Health - 5;
            my_Health.Player_Health(player_Health);
        }

        if(player_Health <= 0)
        {
            player_Health = 100;
            SceneManager.LoadScene(1);
        }
    }
}
