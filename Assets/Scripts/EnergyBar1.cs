using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar1 : MonoBehaviour
{
    public Slider energySlider;
    public Slider easeEnergySlider;
    public float energy;
    public float maxEnergy = 100f;
    private float lerpSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(energySlider.value != energy)
        {
            energySlider.value = energy;
        }

        

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Damage(10);
        }

       
        if(energySlider.value != easeEnergySlider.value)
        {
            easeEnergySlider.value = Mathf.Lerp(easeEnergySlider.value, energy, lerpSpeed);
        }
    }

    public void Damage(float damagePoints)
    {
        if(energy > 0)
        {
            energy -= damagePoints;
        }
            
    }

    public void Heal(float damagePoints)
    {
        if(energy < maxEnergy)
        {
            energy += 1;
        }
            
    }
}
