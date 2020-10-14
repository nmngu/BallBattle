using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeController : MonoBehaviour
{
    private float normal_speed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Detect player");
            if (other.gameObject.GetComponent<PlayerController>().isGetBall)
            {
                this.GetComponentInParent<EnemyController>().isDetectBall = true;
            }
        }

    }
}
