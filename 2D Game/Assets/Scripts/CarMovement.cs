using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveSpeedX = 6f;
    public float moveSpeedY = 6f;
    private Rigidbody2D myRigidbody;
    public bool isMoving;
    public float moveTime;
    private float moveCounter;
    public float waitTime;
    private float waitCounter;

    private int MoveDirection = 0;

    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        moveCounter = moveTime;

        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            moveCounter -= Time.deltaTime;

            if (moveCounter < 0)
            {
                isMoving = false;
                waitCounter = waitTime;
            }

            // if(MoveDirection == 0)
            // Move up, right, down, left
            switch (MoveDirection)
            {
                case 0:
                    //moveTime = 7f;
                    myRigidbody.velocity = new Vector2(0, moveSpeedY);
                    break;
                case 1:
                    //moveTime = 7.5f;
                    myAnimator.SetFloat("MoveX", 1);
                    myAnimator.SetFloat("MoveY", 0);
                    myRigidbody.velocity = new Vector2(moveSpeedX, 0);
                    break;
                case 2:
                    //moveTime = 7f;
                    myAnimator.SetFloat("MoveX", 0);
                    myAnimator.SetFloat("MoveY", -1);
                    myRigidbody.velocity = new Vector2(0, -moveSpeedY);
                    break;
                case 3:
                    //moveTime = 7.5f;
                    myAnimator.SetFloat("MoveX", -1);
                    myAnimator.SetFloat("MoveY", 0);
                    myRigidbody.velocity = new Vector2(-moveSpeedX, 0);
                    break;
                case 4:
                    //moveTime = 7f;
                    myAnimator.SetFloat("MoveX", 0);
                    myAnimator.SetFloat("MoveY", 1);
                    myRigidbody.velocity = new Vector2(0, moveSpeedY);
                    break;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;

            myRigidbody.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()
    {
        // 0,1,2,3
        if (MoveDirection >= 4)
        {
            MoveDirection = 0;
        }
        // Debug.Log(MoveDirection);
        MoveDirection++;
        isMoving = true;
        moveCounter = moveTime;
    }
}
