using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Text energyText;
    public Slider energyBar;
    public Image sliderColor;
    float lerpSpeed;
    float regenTime = 1f;
    float regenSmooth = 0.01f;
    float energyRegen = 0.5f;
    float count;
    public Image[] energyPoints;
    float energy, maxEnergy = 6 ;

    // Start is called before the first frame update
    void Start()
    {
        
        energy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (count >= regenTime * regenSmooth)
        {
            count = 0;
            energy += energyRegen * regenSmooth;
        }
        // StartCoroutine("Regen");
        energyText.text = "energy: " + (int)energy;
        if(energy > maxEnergy)
        {
            energy = maxEnergy;
        }

        lerpSpeed = 3f * Time.deltaTime;
        EnergyBarFiller();
        ColorChanger();

    }

    public void EnergyBarFiller()
    {
        energyBar.value = Mathf.Lerp(energyBar.value, (energy / maxEnergy), lerpSpeed);

        for (int i = 0; i < energyPoints.Length; i++)
        {
            energyPoints[i].enabled = !DisplayEnergyPoint((int)energy, i);   
        }
    }

    void ColorChanger()
    {
        Color energyColor = Color.Lerp(Color.red, Color.green, (energy / maxEnergy) );
        sliderColor.color = energyColor;
    }

    bool DisplayEnergyPoint(int _energy, int pointNumber)
    {
        return ((pointNumber * 1) >= _energy);
    }

    public void Damage(float damagePoints)
    {
        // StopCoroutine("Regen");
        if (energy > 0)
        {
            energy -= damagePoints;
        }
    }

    public void Heal(float healPoints)
    {
        // StopCoroutine("Regen");
        if(energy < maxEnergy)
        {
            energy += healPoints;
        }
    }

//     IEnumerator Regen()
//     {
//         for (float currentenergy = energy; currentenergy <= 6; currentenergy += 0.0005)
//         {
//             energy = currentenergy;
//             yield return new WaitForSeconds(Time.deltaTime);
//         }

//         // if(energy <= maxEnergy)
//         // {
//         //     energy += regenenergy;
//         //     yield return new WaitForSeconds (Time.deltaTime);
//         // }
//         maxEnergy = 6f;
//     }
}