using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SpawnObject : MonoBehaviour
{
    private Transform attackerHighlight;
    private Transform deffenderHighlight;
    private Transform attackerSelection;
    private Transform deffenderSelection;
    public SpawnCost spawnCost;
    private RaycastHit raycastHit;
    public GameObject attacker;
    public GameObject deffender;
    private Camera cam = null;
    // enum TeamSide {Attacker, Deffender}
    // [Header("Player Side")]
    // [SerializeField] TeamSide playerSide;
    // [Header("Enemy Side")]
    // [SerializeField] TeamSide enemySide;
    // public GameEvent onSpawnCost;
    // public SpawnCost spawnCost;

    void Start() 
    {
        cam = Camera.main;
        
    }

    void Update()
    {
        // Highlight
        if (attackerHighlight != null)
        {
            attackerHighlight = null;
            
        }

        if (deffenderHighlight != null)
        {
            deffenderHighlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            attackerHighlight = raycastHit.transform;
            deffenderHighlight = raycastHit.transform;
            if (attackerHighlight.CompareTag("Attacker_Field") && attackerHighlight == attackerSelection)
            {
               attackerHighlight = null;
            }
            if (deffenderHighlight.CompareTag("Deffender_Field") && deffenderHighlight == deffenderSelection)
            {
                deffenderHighlight = null;
            }
            
        }
        // Selection Attacker
        if (Input.GetMouseButtonDown(0))
        {
            if (spawnCost.attackerEnergyCost.energy < spawnCost.attackerSpawnCost)
            {
                Debug.Log("energi tidak cukup ");
            }
            else
            {
                SpawnAttacker();
            }
            
            // StartCoroutine(SpawnTime());
            
        }

        // Selection Deffender
        if (Input.GetMouseButtonDown(1))
        {
            if (spawnCost.deffenderEnergyCost.energy < spawnCost.deffenderSpawnCost)
            {
                Debug.Log("energi tidak cukup ");
            }
            else
            {
                SpawnDeffender();
                
            }
            // StartCoroutine(SpawnTime());
            
        }

        void SpawnAttacker()
        {
            if (attackerHighlight)
            {
                if (attackerSelection == null  && attackerHighlight.CompareTag("Attacker_Field"))
                {
                    RaycastHit hit;
                    if(Physics.Raycast(ray, out hit))
                    {
                        Instantiate(attacker, hit.point, Quaternion.identity);
                        spawnCost.AttackerCost();   
                        
                    }
                    Debug.Log("SELECTED");
                    attackerSelection = raycastHit.transform;
                    attackerHighlight = null;
            
                }
            }
                else
                {
                    if (attackerSelection)
                    {
                        attackerSelection = null;
                    }
                }
        }

        void SpawnDeffender()
        {
            if (deffenderHighlight)
            {
                if (deffenderSelection == null &&  deffenderHighlight.CompareTag("Deffender_Field"))
                {
                    RaycastHit hit;
                    if(Physics.Raycast(ray, out hit))
                    {
                        Instantiate(deffender, hit.point, Quaternion.identity);
                        spawnCost.DeffenderCost();
                        
                                               
                    }
                    Debug.Log("SELECTED");
                    deffenderSelection = raycastHit.transform;
                    deffenderHighlight = null;
                }
            }
            else
            {
                if (deffenderSelection)
                {
                    deffenderSelection = null;
                }
            }
        }

        

        
    }

}
