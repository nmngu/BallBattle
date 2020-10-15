using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 ballPosition;
    public float normal_speed;
    public float carry_speed;
    public bool isGetBall = false;
    private bool isHasBallToChase = true;
    public bool isActivated = true;
    GameObject ball;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void FixedUpdate()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball == null)
        {
            isHasBallToChase = false;
        }
        else
        {
            isHasBallToChase = true;
            ballPosition = ball.transform.position;
        }

        if (isActivated)
        {
            if (isHasBallToChase)
                ChaseBall();
            else
                MoveForward();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    void ChaseBall()
    {
        transform.position = Vector3.MoveTowards(transform.position, ballPosition, (isGetBall ? carry_speed : normal_speed) * Time.deltaTime);
        Vector3 targetDir;
        targetDir = ballPosition - transform.position;
        targetDir.y = 0;
        transform.rotation = Quaternion.LookRotation(targetDir);
    }
    void MoveForward()
    {
        Vector3 targetDir;
        if (isGetBall)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 7.0f), carry_speed * Time.deltaTime);
            targetDir = new Vector3(0, 0, 7.0f) - transform.position;
            targetDir.y = 0;
            transform.rotation = Quaternion.LookRotation(targetDir);
        }
        else
        {
            transform.position += new Vector3(0, 0, 1.0f) * normal_speed * Time.deltaTime;
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnCollisionEnter(Collision other)
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Ball")
        {
            other.gameObject.SetActive(false);
            this.isHasBallToChase = false;
            isGetBall = true;
        }
        else if (other.tag == "DeadField")
        {
            this.gameObject.SetActive(false);
        }
        else if (other.tag == "Finish" && isGetBall)
        {
            Debug.Log("Match End: You Win!");
        }
    }
}
