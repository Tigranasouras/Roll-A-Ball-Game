using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;



public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public AudioSource pickupAudio;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //The one attacthed to the gameObject
        count = 0;

        SetCountText();
        winTextObject.gameObject.SetActive(false);
        pickupAudio = GetComponent<AudioSource>();

    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.gameObject.SetActive(true);
            winTextObject.text = "You win!";
        }
    }

    private void FixedUpdate() //Called more frequently than Update()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Destroy
            Destroy(gameObject);

            //Set the text to "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You lose!";

            Destroy(GameObject.FindGameObjectWithTag("Enemy"));

        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup")) {
            pickupAudio.Play();
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
            //Debug.Log("Sound played: " + pickupAudio);
            
        }
    }
}
