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
    public AudioSource winAudio;
    public AudioSource wallAudio;
    public GameObject explosionFX;
    public GameObject pickupFX;

    public GameObject winFX;
    public GameObject trailFX;
    private GameObject enemy;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //The one attacthed to the gameObject
        count = 0;

        SetCountText();
        winTextObject.gameObject.SetActive(false);
        enemy = GameObject.FindWithTag("Enemy");
        
       

    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
        var movementFX = Instantiate(trailFX, rb.transform.position, Quaternion.identity);
        Destroy(movementFX, 10);

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 13)
        {
            winTextObject.gameObject.SetActive(true);
            var winnerFX = Instantiate(winFX, rb.transform.position, Quaternion.identity);
            Destroy(winnerFX, 50);


            winTextObject.text = "You win!";
            winAudio.Play();
            enemy.SetActive(false);
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
          // dieAudio.Play();
          AudioSource enemyAudio = collision.gameObject.GetComponent<AudioSource>();
            enemyAudio.Play();

            Instantiate(explosionFX, transform.position, Quaternion.identity);




            //Set the text to "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You lose!";
            //Destroy
            Destroy(GameObject.FindGameObjectWithTag("Enemy"), 0.3f);
            Destroy(gameObject, 0.1f);

        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallAudio.Play();
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup")) {
            pickupAudio.Play();

            var currentPickupFX = Instantiate(pickupFX, other.transform.position, Quaternion.identity);

            Destroy(currentPickupFX, 3);


            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
            //Debug.Log("Sound played: " + pickupAudio);
            
        }
    }
}
