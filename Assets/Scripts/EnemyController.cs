using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float normal_speed;
    public float detective_rang;
    private bool isActivated = true;
    Animator anim;
    public bool isDetectBall = false;
    public GameObject direct_arrow;
    // Start is called before the first frame update
    void Start()
    {
        direct_arrow.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (isActivated && isDetectBall)
        {
            GameObject[] list_players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in list_players)
            {
                if (player.GetComponent<PlayerController>().isGetBall)
                {
                    if (direct_arrow != null)
                        direct_arrow.SetActive(true);
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, normal_speed * Time.deltaTime);
                    Vector3 targetDir = player.transform.position - transform.position;
                    targetDir.y = 0;
                    transform.rotation = Quaternion.LookRotation(targetDir);
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player"
        && other.gameObject.GetComponent<PlayerController>().isGetBall)
        {
            other.gameObject.GetComponent<PlayerController>().isActivated = false;
            this.isActivated = false;
            if (direct_arrow != null)
                direct_arrow.gameObject.SetActive(false);
            //anim.SetTrigger("makered");
        }
    }
}
