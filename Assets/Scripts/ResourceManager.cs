using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;


[Serializable]
public class Building
{
    public int costHoney;
    public float incomeHoney;

    public int costConstructionPolymer;
    public float incomeConstructionPolymer;

    public int requiedBears;
    public int producedBears;
    
    public int requiedEnergy;
    public int producedEnergy;
}
public class ResourceManager : MonoBehaviour
{
    public PlacementManager placementManager;
    
    public float honey;
    public float incomeHoney;

    public float constructionPolymer;
    public float incomeConstructionPolymer;

    public int requiedBears;
    public int producedBears;

    public int requiedEnergy;
    public int producedEnergy;

    public float honeyPerBear;

    public Dictionary<CellType, Building> structureDictionary = new();

    // Start is called before the first frame update
    void Start()
    {
        honeyPerBear = 0.1f;

        Building _newBuilding = new();
        _newBuilding.costConstructionPolymer = 5;
        structureDictionary.Add(CellType.Road, _newBuilding);

        _newBuilding = new();
        _newBuilding.costConstructionPolymer = 15;
        _newBuilding.producedEnergy = 1;
        structureDictionary.Add(CellType.Windmill, _newBuilding);

        _newBuilding = new();
        _newBuilding.costConstructionPolymer = 25;
        _newBuilding.requiedEnergy = 1;
        _newBuilding.producedBears = 2;
        structureDictionary.Add(CellType.House, _newBuilding);

        _newBuilding = new();
        _newBuilding.costConstructionPolymer = 30;
        _newBuilding.requiedEnergy = 1;
        _newBuilding.requiedBears = 1;
        _newBuilding.incomeHoney = 1;
        structureDictionary.Add(CellType.Apiary, _newBuilding);

        _newBuilding = new();
        _newBuilding.costConstructionPolymer = 50;
        _newBuilding.requiedBears = 1;
        _newBuilding.requiedEnergy = 1;
        _newBuilding.incomeConstructionPolymer = 0.5f;
        structureDictionary.Add(CellType.Shop, _newBuilding);

        _newBuilding = new();
        _newBuilding.costConstructionPolymer = 70;
        _newBuilding.requiedBears = 2;
        _newBuilding.producedEnergy = 5;
        structureDictionary.Add(CellType.ElectricGenerator, _newBuilding);
    }

    // Update is called once per frame
    void Update()
    {
        honey += Time.deltaTime * incomeHoney;
        constructionPolymer += Time.deltaTime * incomeConstructionPolymer;
    }

    public bool CanAfford(CellType type, int amount = 1)
    {
        if (honey >= structureDictionary[type].costHoney * amount 
            && constructionPolymer >= structureDictionary[type].costConstructionPolymer * amount)
        {
            return true;
        }
        return false;
    }

    public void SpendResourceToBuild(CellType type, int amount = 1)
    {
        honey -= structureDictionary[type].costHoney * amount;
        constructionPolymer -= structureDictionary[type].costConstructionPolymer * amount;
    }

    public void Recount()
    {
        incomeHoney = 0;
        incomeConstructionPolymer = 0;

        requiedBears = 0;
        producedBears = 0;

        requiedEnergy = 0;
        producedEnergy = 0;

        foreach (var structure in placementManager.structureDictionary.Values)
        {
            if (structure.isActive)
                {
                incomeHoney += structureDictionary[structure.type].incomeHoney;
                incomeConstructionPolymer += structureDictionary[structure.type].incomeConstructionPolymer;
                requiedBears += structureDictionary[structure.type].requiedBears;
                producedBears += structureDictionary[structure.type].producedBears;
                requiedEnergy += structureDictionary[structure.type].requiedEnergy;
                producedEnergy += structureDictionary[structure.type].producedEnergy;
            }
        }

        if (requiedBears > producedBears || requiedEnergy > producedEnergy)
        { 
            incomeHoney = 0;
            incomeConstructionPolymer = 0;
        }

        incomeHoney -= honeyPerBear * producedBears;
    }
}
