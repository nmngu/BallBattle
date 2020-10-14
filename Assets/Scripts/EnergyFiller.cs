using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnergyFiller : MonoBehaviour
{

    private int energy = 0;
    private int mintuesBetweenFills = 5;
    private DateTime lastEnergyFill;
    float enemy_fill_amount = 0;
    float player_fill_amount = 0;
    public GameObject enemy_energy;
    public GameObject player_energy;
    // Start is called before the first frame update
    void Start()
    {
        lastEnergyFill = DateTime.Parse(PlayerPrefs.GetString("lastEnergyFill", DateTime.Now.ToString()));
    }
    private void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        TimeSpan timeSinceEnergyFill = (DateTime.Now - lastEnergyFill);
        if (timeSinceEnergyFill > TimeSpan.FromMinutes(mintuesBetweenFills))
        {
            int ammount = timeSinceEnergyFill.Minutes / mintuesBetweenFills;
            energy += ammount;
            lastEnergyFill -= TimeSpan.FromMinutes(mintuesBetweenFills * ammount);
            PlayerPrefs.SetString("lastEnergyFill", lastEnergyFill.ToString());
        }

        enemy_energy.GetComponent<UnityEngine.UI.Image>().fillAmount = enemy_fill_amount;
        player_energy.GetComponent<UnityEngine.UI.Image>().fillAmount = player_fill_amount;
    }

}
