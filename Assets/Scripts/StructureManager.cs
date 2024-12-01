using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct Structure
{
    public CellType type;
    public GameObject prefab;
}


public class StructureManager : MonoBehaviour
{
    public Structure[] structures;
    public PlacementManager placementManager;

    public ResourceManager resourceManager;

    public CellType structureType;

    public void PlaceStructure(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            if (!resourceManager.CanAfford(structureType)) return;

            resourceManager.SpendResourceToBuild(structureType);
            foreach (var structure in structures)
            {
                if (structure.type == structureType)
                {
                    placementManager.PlaceObjectOnTheMap(position, structure.prefab, structureType);
                }
            }
            AudioPlayer.instance.PlayPlacementSound();
            resourceManager.Recount();
        }
    }

    private bool CheckPositionBeforePlacement(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("This position is out of bounds");
            return false;
        }
        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            Debug.Log("This position is not EMPTY");
            return false;
        }
        if (placementManager.GetNeighboursOfTypeFor(position, CellType.Road).Count <= 0)
        {
            Debug.Log("Must be placed near a road");
            return false;
        }
        return true;
    }
}

[Serializable]
public struct StructurePrefabWeighted
{
    public GameObject prefab;
    [Range(0, 1)]
    public float weight;
}
