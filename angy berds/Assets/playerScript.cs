using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Vector3 mousePosition;
    public Rigidbody2D rb;
    public SpringJoint2D joint2D;
    public Transform defualtPosition;
    private bool throwing;
    [SerializeField] float speed;
    public bool released;

    public LineRenderer line;

   
    private Vector3 lastPosition;
    private bool updateJoints;
    

    private void Start()
    {
        rb.velocity = new Vector2(defualtPosition.position.x - transform.position.x, defualtPosition.position.y - transform.position.y);
        released = false;
        throwing = false;
        transform.position = defualtPosition.position;
    }
    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        Vector3 defualtDirection = new Vector2(defualtPosition.position.x - transform.position.x, defualtPosition.position.y - transform.position.y);
        float stringDistance = Vector2.Distance(mousePosition, defualtPosition.position);
        float throwSpeed = Vector2.Distance(transform.position, defualtPosition.position) * speed;
        float playerStringDistance = Vector2.Distance(transform.position, defualtPosition.position);






        Debug.Log(stringDistance);



        if (Input.GetMouseButton(0) && !throwing && !released)
        {
            line.enabled = true;
            if (playerStringDistance > 5 )
            {
                joint2D.enabled = true;
                
                if(updateJoints)
                {
                    updateJoints = false;
                    joint2D.distance = Vector2.Distance(transform.position, defualtPosition.position);
                }
                

            }
            if(stringDistance < 5)
            {
                updateJoints = true;
                joint2D.enabled = false;
                
            }
            rb.velocity = mouseDirection * 10;
            line.SetPosition(0, defualtPosition.position);
            line.SetPosition(1, transform.position);
        }
        
        else if(!released)
        {
            
            released = false;
            throwing = false;
               
                
            rb.velocity = defualtDirection * 10;
            
            
           
            
            
        }

        if(playerStringDistance > 1.5f && Input.GetMouseButtonUp(0) && !released)
        {
            released = true;
            joint2D.enabled = false;
            line.enabled = false;
            
            Debug.Log("gamer");
            rb.velocity = new Vector2(defualtDirection.x, defualtDirection.y).normalized * throwSpeed;
        }
      

      if(Input.GetButtonDown("Jump"))
        {
            released = false;
        }
        
    }
}
