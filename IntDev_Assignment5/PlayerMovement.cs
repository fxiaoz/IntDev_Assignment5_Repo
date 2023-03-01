using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer myRenderer;

    public float speed = 2f;

    public bool keyCollect;
    public GameObject keyText;
    public GameObject textbgkey;

    // Start is called before the first frame update
    void Start()
    {
        keyCollect = false;
        keyText.SetActive(false);
        textbgkey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            newPos.y += speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            newPos.x -= speed * Time.deltaTime;
            //myRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            newPos.y -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            newPos.x += speed * Time.deltaTime;
            //myRenderer.flipX = true;
        }

        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "orange" && Input.GetKey(KeyCode.Space))
        {
            keyText.SetActive(true);
            textbgkey.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "key")
        {
            keyCollect = true;
            Destroy(collision.gameObject);
        }
    }
}
