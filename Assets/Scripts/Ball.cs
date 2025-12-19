using System;
using UnityEngine;
using Object = System.Object;

public class Ball : MonoBehaviour
{

    [SerializeField] private float movementForce = 5f;
    [SerializeField] private float movementJump = 5f;
    [SerializeField] private float gluttony;
    [SerializeField] private GameObject Congrats;
    [SerializeField] private GameObject YouDied;
    private Rigidbody rb;
    float vInput;
    float hInput;
    float timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Instead of time.deltatime, with forces, we put this:
        Application.targetFrameRate = 10000000;
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxisRaw("Vertical") * 0.25f;
        hInput = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (Physics.Raycast(transform.position, Vector3.down, transform.localScale.y + 0.001f))
            {
                rb.AddForce(Vector3.up * movementJump, ForceMode.Impulse);
            }
            
        }
        Shrinking();
    }

    //update void but able to run physics (constant forces). Every 0.02 seconds
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(hInput, 0, vInput) * movementForce, ForceMode.Force);
        
    }

    private void Shrinking()
    {
        transform.localScale -= new Vector3(0.25f, 0.25f, 0.25f) * Time.deltaTime;
        movementForce += 0.3f * Time.deltaTime;
        rb.mass -=  0.01f * Time.deltaTime;

        if (transform.localScale.x <= 0)
        {
            Destroy(this.gameObject);
            YouDied.SetActive(true);
            
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bonus"))
        {
            transform.localScale += new Vector3(gluttony, gluttony, gluttony);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
            Congrats.SetActive(true);
            
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
    
}
