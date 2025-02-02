﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnergyFiller : MonoBehaviour
{
    public int enemy_energy = 0;
    public int playper_energy = 0;
    public int timeBetweenFills = 2;
    // float enemy_fill_amount = 0.00f;
    // float player_fill_amount = 0.00f;
    public GameObject gobj_enemy_energy_amount;
    public GameObject gobj_player_energy_amount;
    public GameObject gobj_enemy_energy_filter;
    public GameObject gobj_player_energy_filter;
    private float time;

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time > timeBetweenFills)
        {
            if (playper_energy < 6)
                playper_energy += 1;
            if (enemy_energy < 6)
                enemy_energy += 1;
            time -= 2.00f;
            // enemy_fill_amount = time + (enemy_energy / 6);
            // player_fill_amount = time + (playper_energy / 6);
        }
    }
    // Update is called once per frame
    void Update()
    {
        gobj_enemy_energy_filter.GetComponent<UnityEngine.UI.Image>().fillAmount = time / 12 + enemy_energy / 6.00f;
        gobj_player_energy_filter.GetComponent<UnityEngine.UI.Image>().fillAmount = time / 12 + playper_energy / 6.00f;
        gobj_enemy_energy_amount.GetComponent<UnityEngine.UI.Image>().fillAmount = enemy_energy / 6.00f;
        gobj_player_energy_amount.GetComponent<UnityEngine.UI.Image>().fillAmount = playper_energy / 6.00f;
    }
}
