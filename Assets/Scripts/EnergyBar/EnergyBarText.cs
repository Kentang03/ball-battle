using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyBarText : MonoBehaviour
{
    public TextMeshProUGUI energyBarText;

    void Awake()
    {
        SetEnergy(0);
    }

    private void SetEnergy(int energy)
    {
        energyBarText.text = energy.ToString();
    }

    public void UpdateEnergy(Component sender, object data)
    {
        if(data is int)
        {
            int amount = (int) data;

            SetEnergy(amount);
        }
    }    
}
