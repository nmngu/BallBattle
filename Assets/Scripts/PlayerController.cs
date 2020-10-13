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
    // Start is called before the first frame update
    void Start()
    {

    }
    private void FixedUpdate()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
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
    }
    void MoveForward()
    {
        transform.position += new Vector3(1.0f, 0, 0) * (isGetBall ? carry_speed : normal_speed) * Time.deltaTime;
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
    }
}
