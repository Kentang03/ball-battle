using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnergyBar : MonoBehaviour
{
    public Slider energyBar;
    public Image sliderColor;
    // public string side;
    float lerpSpeed;
    float regenTime = 1f;
    float regenSmooth = 0.01f;
    float energyRegen = 0.5f;
    float count;
    public Image[] energyPoints;
    public float energy, maxEnergy = 6;
    enum TeamSide {Blue, Red}
    [Header("Team Side")]
    [SerializeField] TeamSide side;

    void Start()
    {
        
        energy = 0;
    }

    void Update()
    {
        RegenEnergy();
        EnergyBarFiller();
        ColorChanger();
        // onSpawnCost.Raise(this, energy);
    }


    public void EnergyBarFiller()
    {
        if(energy > maxEnergy) energy = maxEnergy;
        if(energy <= 0) energy = 0;

        lerpSpeed = 5f * Time.deltaTime;
        energyBar.value = Mathf.Lerp(energyBar.value, (energy / maxEnergy), lerpSpeed);

        for (int i = 0; i < energyPoints.Length; i++)
        {
            energyPoints[i].enabled = !DisplayEnergyPoint((int)energy, i);
        }
    }

    void ColorChanger()
    {
        if(side == TeamSide.Blue)
        {
            Color energyColor = Color.Lerp(Color.blue, Color.blue, (energy / maxEnergy) );
            sliderColor.color = energyColor;
            
        }

        if(side == TeamSide.Red)
        {
            Color energyColor = Color.Lerp(Color.red, Color.red, (energy / maxEnergy) );
            sliderColor.color = energyColor;
        }
        
    }

    bool DisplayEnergyPoint(int _energy, int pointNumber)
    {
        if(side == TeamSide.Blue)
        {
            energyPoints[pointNumber].color = Color.blue;
        }

        if(side == TeamSide.Red)
        {
            energyPoints[pointNumber].color = Color.red;
        }
        
        return ((pointNumber * 1) >= _energy);
    }

    public void RegenEnergy()
    {
        count += Time.deltaTime;
        if (count >= regenTime * regenSmooth)
        {
            count = 0;
            energy += energyRegen * regenSmooth;
        }
    }

    public void SpawnCost(float costPoints)
    {
        if (energy > 0)
        {
            energy -= costPoints;
        }
    }

    public void Heal(float healPoints)
    {
        if(energy < maxEnergy)
        {
            energy += healPoints;
        }
    }


}