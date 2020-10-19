using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 ballPosition;
    public float normal_speed;
    public float carry_speed;
    public bool isGetBall = false;
    private bool isHasBallToChase;
    public bool isActivated = true;
    GameObject ball;
    Animator animator;
    private float time_deactive;
    public float reactive_time = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isHasBallToChase = false;

    }
    private void FixedUpdate()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void Update()
    {
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
            {
                ChaseBall();
            }
            else
                MoveForward();
        }
        else
        {
            time_deactive += Time.deltaTime;
            if (time_deactive > reactive_time)
                isActivated = true;
        }
    }
    void ChaseBall()
    {
        transform.position = Vector3.MoveTowards(transform.position, ballPosition, normal_speed * Time.deltaTime);
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
        if (other.collider.tag == "Ball" && isActivated)
        {
            Debug.Log("Match------------");
            other.collider.GetComponentInParent<BallController>().isPass2Attacker = false;
            other.gameObject.SetActive(false);
            this.isHasBallToChase = false;
            isGetBall = true;
        }
        else
        if (other.collider.tag == "Enemy"
        && isGetBall)
        {
            Debug.Log("enemy active? " + other.gameObject.GetComponent<EnemyController>().isActivated);
            // && other.gameObject.GetComponent<EnemyController>().isActivated)
            if (other.gameObject.GetComponent<EnemyController>().isActivated)
            {
                other.gameObject.GetComponent<EnemyController>().isActivated = false;
                other.gameObject.GetComponent<EnemyController>().isDetectBall = null;
                if (other.gameObject.GetComponent<EnemyController>().direct_arrow != null)
                    other.gameObject.GetComponent<EnemyController>().direct_arrow.gameObject.SetActive(false);
                Debug.Log("catched by enemy");
                isGetBall = false;
                isActivated = false;
                GameObject[] list_players = GameObject.FindGameObjectsWithTag("Player");
                Transform target = GetClosestAttacker(list_players);
                GameObject ballContainer = GameObject.Find("BallIndicator");
                if (ballContainer != null)
                {
                    ballContainer.transform.GetChild(0).gameObject.SetActive(true);
                    ballContainer.transform.position = transform.position;
                    ballContainer.GetComponent<BallController>().isPass2Attacker = true;
                    ballContainer.GetComponent<BallController>().attacker = target;
                }
                ball = GameObject.Find("Ball");
                if (ball != null)
                {
                    ball.transform.position = transform.position;

                }
            }
            // set animation of attacker to deactive state
            animator.SetBool("isActive", false);
        }
    }
    Transform GetClosestAttacker(GameObject[] attacker)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject obj in attacker)
        {
            if (obj.GetComponent<PlayerController>().isActivated && obj != this.gameObject)
            {
                float dist = Vector3.Distance(obj.transform.position, currentPos);
                if (dist < minDist && dist > 0)
                {
                    tMin = obj.transform;
                    minDist = dist;
                }
            }
        }
        return tMin;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "DeadField")
        {
            this.gameObject.SetActive(false);
        }
        else if (other.tag == "Finish" && isGetBall)
        {
            Debug.Log("Match End: You Win!");
            GameObject end = GameObject.Find("Endgame");
            if (end != null)
                end.SetActive(true);
        }
    }
}
