﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ball;
    public GameObject prefab_player;
    public GameObject prefab_enemy;
    private List<GameObject> list_players;
    private List<GameObject> list_enemies;
    private float match_time = 140;
    float m_time = 0;
    public EnergyFiller energy_filter;
    void Start()
    {
        GameObject end = GameObject.Find("Endgame");
        if (end != null)
            end.SetActive(false);
        Vector3 ballPosition = new Vector3(Random.Range(-4.0f, 4.0f), 0.3f, Random.Range(0.0f, -6.0f));// y = 0.3 to place ball on plane
        //Instantiate(ball, ballPosition, Quaternion.identity);
        ball.transform.position = ballPosition;
        ball.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        GameObject remain_time = GameObject.FindGameObjectWithTag("RemainTime");
        int i_remainTime = (int)(match_time - m_time);
        remain_time.GetComponent<UnityEngine.UI.Text>().text = i_remainTime.ToString() + "s";

        //Process click event
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayhit;
            if (Physics.Raycast(ray, out rayhit)
            && (rayhit.collider.tag == "Ground" || rayhit.collider.tag == "Detective_range"))
            {
                var position = rayhit.point;
                position.y = 0.5f;
                if (position.z <= 0)
                {
                    if (energy_filter.playper_energy >= 2)
                    {
                        Instantiate(prefab_player, position, Quaternion.identity);
                        energy_filter.playper_energy -= 2;
                    }
                }
                else
                {
                    if (energy_filter.enemy_energy >= 3)
                    {
                        Instantiate(prefab_enemy, position, Quaternion.identity);
                        energy_filter.enemy_energy -= 3;
                    }
                }
            }
        }
    }
}
