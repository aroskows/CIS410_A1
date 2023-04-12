using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce = 5;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layermask;
    //public float gravityScale = 5;


    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private int count; 


    // Start is called before the first frame update
    void Start() {
        count = 0;
        rb = GetComponent<Rigidbody>();

        SetCountText();

        winTextObject.SetActive(false);
        
    }

    void OnMove (InputValue movementValue) {
        //movement = x, y direction
        // Input = type`
        Vector2 movementVector = movementValue.Get<Vector2>();


        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();

        if(count >= 12) {
            winTextObject.SetActive(true);
        }
    }

    bool GroundCheck() {
        if (Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, layermask)) {
            return true;
        } else {
            return false;
        }
    }

    void FixedUpdate() {

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space)) {
           // if (GroundCheck()){
                rb.AddForce( Vector3.up * jumpForce, ForceMode.Impulse);


          //  }
           
        }

        //rb.gravityScale = gravityScale;

    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    void OnTriggerEnter(Collider other) {
        // called when player object interactes with trigger collider
        if(other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            ++count;
            SetCountText();
        }


    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
