using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer myRenderer;
    public Animator myAnim;

    public float speed = 2f;

    public bool keyCollect;

    public GameObject keyText;
    public GameObject textbgkey;
    public GameObject friendText;
    public GameObject exitText;
    public GameObject textbgexit;

    public bool orangeCollide;
    public bool blueCollide;

    public AudioSource mySource;
    public AudioClip blipSound;
    public AudioClip pickupSound;
    public AudioClip openSound;
    public AudioClip endSound;

    // Start is called before the first frame update
    void Start()
    {
        keyCollect = false;

        keyText.SetActive(false);
        textbgkey.SetActive(false);
        friendText.SetActive(false);
        exitText.SetActive(false);
        textbgexit.SetActive(false);

        orangeCollide = false;
        blueCollide = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            newPos.y += speed * Time.deltaTime;
            myAnim.SetBool("backward", true);
            myAnim.SetBool("forward", false);
            myAnim.SetBool("right", false);
            myAnim.SetBool("backward idle", false);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            myAnim.SetBool("backward idle", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            newPos.x -= speed * Time.deltaTime;
            myRenderer.flipX = true;
            myAnim.SetBool("backward", false);
            myAnim.SetBool("forward", false);
            myAnim.SetBool("right", true);
            myAnim.SetBool("right idle", false);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            myRenderer.flipX = true;
            myAnim.SetBool("right idle", true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            newPos.y -= speed * Time.deltaTime;
            myAnim.SetBool("backward", false);
            myAnim.SetBool("forward", true);
            myAnim.SetBool("right", false);
            myAnim.SetBool("forward idle", false);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            myAnim.SetBool("forward idle", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            newPos.x += speed * Time.deltaTime;
            myRenderer.flipX = false;
            myAnim.SetBool("backward", false);
            myAnim.SetBool("forward", false);
            myAnim.SetBool("right", true);
            myAnim.SetBool("right idle", false);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            myAnim.SetBool("right idle", true);
        }

        transform.position = newPos;

        if (orangeCollide && Input.GetKeyDown(KeyCode.Space) && keyCollect == false)
        {
            keyText.SetActive(true);
            textbgkey.SetActive(true);
            mySource.PlayOneShot(blipSound);
        }

        if (orangeCollide && Input.GetKeyDown(KeyCode.Space) && keyCollect == true)
        {
            friendText.SetActive(true);
            textbgkey.SetActive(true);
            mySource.PlayOneShot(blipSound);
        }

        if (blueCollide && Input.GetKeyDown(KeyCode.Space))
        {
            exitText.SetActive(true);
            textbgexit.SetActive(true);
            mySource.PlayOneShot(blipSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "orange")
        {
            Debug.Log("collide");
            orangeCollide = true;
        }

        if (collision.gameObject.name == "door" && keyCollect == true)
        {
            Destroy(collision.gameObject);
            mySource.PlayOneShot(openSound);
        }

        if (collision.gameObject.name == "blue")
        {
            blueCollide = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        keyText.SetActive(false);
        textbgkey.SetActive(false);
        friendText.SetActive(false);
        exitText.SetActive(false);
        textbgexit.SetActive(false);

        orangeCollide = false;
        blueCollide = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "key")
        {
            keyCollect = true;
            Destroy(collision.gameObject);
            mySource.PlayOneShot(pickupSound);
        }

        if (collision.gameObject.name == "exit")
        {
            mySource.PlayOneShot(endSound);
            SceneManager.LoadScene("Title Screen");
        }
    }
}
