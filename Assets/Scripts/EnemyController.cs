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
    // Start is called before the first frame update
    void Start()
    {
        GameObject direct_arrow = GameObject.Find("Indicator_Direction");
        if (direct_arrow != null)
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
                    GameObject direct_arrow = GameObject.Find("Indicator_Direction");
                    if (direct_arrow != null)
                        direct_arrow.SetActive(true);
                    // transform.Find("Indicator_Direction").gameObject.SetActive(true);
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
            //anim.SetTrigger("makered");
        }
    }
}
