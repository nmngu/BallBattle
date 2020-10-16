using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>().isGetBall)
            {
                this.GetComponentInParent<EnemyController>().isDetectBall = true;
            }
        }
    }
}
