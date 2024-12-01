using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;


[Serializable]
public class Building
{
    public int cost;
    public float income;

    public int requiedBears;
    public int producedBears;
    
    public int requiedEnergy;
    public int producedEnergy;
}
public class ResourceManager : MonoBehaviour
{
    public PlacementManager placementManager;
    
    public float honey;
    public float income;

    public int requiedBears;
    public int producedBears;

    public int requiedEnergy;
    public int producedEnergy;

    public Dictionary<CellType, Building> structureDictionary = new();

    // Start is called before the first frame update
    void Start()
    {
        Building _newBuilding = new();
        _newBuilding.cost = 5;
        structureDictionary.Add(CellType.Road, _newBuilding);

        _newBuilding = new();
        _newBuilding.cost = 25;
        _newBuilding.producedBears = 2;
        structureDictionary.Add(CellType.Structure, _newBuilding);

        _newBuilding = new();
        _newBuilding.cost = 50;
        _newBuilding.requiedBears = 1;
        _newBuilding.income = 0.5f;
        structureDictionary.Add(CellType.SpecialStructure, _newBuilding);
    }

    // Update is called once per frame
    void Update()
    {
        honey += Time.deltaTime * income;
    }

    public bool CanAfford(CellType type)
    {
        if (honey > structureDictionary[type].cost)
        {
            return true;
        }
        return false;
    }

    public void SpendHoneyToBuild(CellType type)
    {
        honey -= structureDictionary[type].cost;
    }

    public void Recount()
    {
        income = 0;

        requiedBears = 0;
        producedBears = 0;

        requiedEnergy = 0;
        producedEnergy = 0;

        foreach (var structure in placementManager.structureDictionary.Values)
        {
            if (structure.isActive)
                {
                income += structureDictionary[structure.type].income;
                requiedBears += structureDictionary[structure.type].requiedBears;
                producedBears += structureDictionary[structure.type].producedBears;
                requiedEnergy += structureDictionary[structure.type].requiedEnergy;
                producedEnergy += structureDictionary[structure.type].producedEnergy;
            }
        }

        if (requiedBears > producedBears || requiedEnergy > producedEnergy)
        { income = 0;}
    }
}
