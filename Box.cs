using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    private float min_X = -2.5f, max_X = 2.5f;

    private bool canMove;
    private float move_Speed = 2.0f;

    private Rigidbody2D myBody;

    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.gravityScale = 0f;

        
    }

    void Start()
    {
        canMove = true;
        
        if(Random.Range(0, 2) > 0)
        {
            move_Speed *= -1f;
        }

        GamePlayController.instance.currentBox = this;
    }

    void Update()
    {
        CanMove();
    }

    void CanMove()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += move_Speed * Time.deltaTime;
            if (temp.x > max_X||temp.x < min_X)
            {
                move_Speed *= -1f;
            }
            transform.position = temp;
        }
    }

    public void DropBox()
    {
        canMove = false;

        myBody.gravityScale = Random.Range(2, 4);

    }


    void Landed()
    {
        if (gameOver)
            return;

        ignoreCollision = true;
        ignoreTrigger = true;

        GamePlayController.instance.SpawnNewBox();
        GamePlayController.instance.MoveCamera();
    }

    void RestartGame()
    {
        GamePlayController.instance.RestaetGame();
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;
        if(target.gameObject.tag == "Platform")
        {
            Invoke("Landed", 1f);
            ignoreCollision = true;
        }

        if (target.gameObject.tag == "Box")
        {
            Invoke("Landed", 1f);
            ignoreCollision = true;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
            return;
        if(target.tag == "GameOver")
        {
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;

            Invoke("RestartGame", 2f);
        }
    }

}
