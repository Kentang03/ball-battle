using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCost : MonoBehaviour
{
    public EnergyBar deffenderEnergyCost;
    public EnergyBar attackerEnergyCost;

   public float attackerSpawnCost = 2;
   public float deffenderSpawnCost = 3;

    

    public void DeffenderCost()
    {
        deffenderEnergyCost.SpawnCost(deffenderSpawnCost);
    }

    public void AttackerCost()
    {
        attackerEnergyCost.SpawnCost(attackerSpawnCost);
    }
}
