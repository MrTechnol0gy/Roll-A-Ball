using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();

        winTextObject.SetActive(false);

        //sets original position so player can be reset upon an out of bounds situation
        originalPos = gameObject.transform.position;
    }

    void OnMove(InputValue movementValue)
    {
        // Function Body
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()
    {
        //Displays the score
        countText.text = "Count: " + count.ToString();
        if(count >= 32)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Picks up the tagged object and iterates the game score
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
        
        //Resets player location and velocity if they fall out of bounds.
        if (other.gameObject.CompareTag("End"))
        {
            //resets position
            gameObject.transform.position = originalPos;
            //resets velocity
            rb.Sleep();
        }

    }

}
