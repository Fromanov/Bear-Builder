using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    //public PlacementManager placementManager;

    public float honey;
    public float income;

    public Dictionary<CellType, int> structureDictionary = new();

    // Start is called before the first frame update
    void Start()
    {
        structureDictionary.Add(CellType.Road, 5);
        structureDictionary.Add(CellType.Structure, 25);
        structureDictionary.Add(CellType.SpecialStructure, 50);
    }

    // Update is called once per frame
    void Update()
    {
        honey += Time.deltaTime * income;
    }

    public bool CanAfford(CellType type)
    {
        if (honey > structureDictionary[type])
        {
            return true;
        }
        return false;
    }

    public void SpendHoneyToBuild(CellType type)
    {
        honey -= structureDictionary[type];
    }

    /*public void RecountIncome()
    {
        income = 0;
        foreach (var structure in placementManager.structureDictionary.Values)
        {
            //if structure.
        }
    }*/

    public void AddIncome(float change)
    {
        income += change;
    }
}
