using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool isPass2Attacker;
    public Transform attacker;
    private float ball_speed = 1.5f;
    // Start is called before the first frame update
    Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        isPass2Attacker = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isPass2Attacker && attacker != null)
        {
            // Debug.Log("isPass2Attacker " + attacker.position.x + " " + attacker.position.y + " " + attacker.position.z);
            // transform.position += attacker.transform.position * Time.deltaTime * ball_speed;
            // transform.Translate(attacker.transform.position * Time.deltaTime * ball_speed, Space.Self);// Vector3.MoveTowards(transform.position, attacker.transform.position, ball_speed * Time.deltaTime);
            // rigidbody.MovePosition((transform.position - attacker.transform.position) * Time.deltaTime * ball_speed);
            transform.position = Vector3.MoveTowards(transform.position, attacker.position, ball_speed * Time.deltaTime);
            Vector3 targetDir = attacker.position - transform.position;

            transform.rotation = Quaternion.LookRotation(targetDir);
        }

    }
}
