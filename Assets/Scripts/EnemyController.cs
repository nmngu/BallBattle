using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 initPosition;
    public float normal_speed;
    public float detective_rang;
    public bool isActivated = true;
    Animator anim;
    public Transform isDetectBall;
    public GameObject direct_arrow;
    private float time_deactive;
    public float reactive_time = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        isDetectBall = null;
        direct_arrow.SetActive(false);
        initPosition = transform.position;
        time_deactive = 0;
    }
    private void FixedUpdate()
    {
        if (isActivated)
        {
            if (isDetectBall != null)
            {
                Move2Position(isDetectBall.transform.position);
            }
        }
        else
        {
            isDetectBall = null;
            Move2Position(initPosition);
            time_deactive += Time.deltaTime;
            if (time_deactive > reactive_time)
                isActivated = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Move2Position(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, normal_speed * Time.deltaTime);
        Vector3 targetDir = position - transform.position;
        targetDir.y = 0;
        if (transform.position != initPosition)
            transform.rotation = Quaternion.LookRotation(targetDir);
    }
}
