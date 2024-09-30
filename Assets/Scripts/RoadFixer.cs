using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadFixer : MonoBehaviour
{
    public GameObject deadEnd, roadStraight, corner, threeWay, fourWay;

    public void FixRoadAtPosition(PlacementManager placementManager, Vector3Int tempPosition)
    {
        //[right, up, left, down]
        CellType[] result = placementManager.GetNeighbourTypesFor(tempPosition);
        int roadCount = 0;
        roadCount = result.Where(x => x == CellType.Road).Count();

        if (roadCount == 0 || roadCount == 1)
        {
            CreateDeadEnd(placementManager, result, tempPosition);
        }

        if (roadCount == 2)
        {
            if(CreateStraightRoad(placementManager, result, tempPosition))
            {
                return;
            }
            else
            {
                CreateCorner(placementManager, result, tempPosition);
            }
        }
        else if (roadCount == 3)
        {
            CreateThreeWay(placementManager, result, tempPosition);
        }
        else if (roadCount == 4)
        {
            CreateFourWay(placementManager, result, tempPosition);
        }
    }

    private void CreateFourWay(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        placementManager.ModifyStructureModel(tempPosition, fourWay, Quaternion.identity);
    }

    //[left, up, right, down]
    private void CreateThreeWay(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[1] == CellType.Road && result[2] == CellType.Road 
            && result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, threeWay, Quaternion.identity);
        }
        else if (result[2] == CellType.Road && result[3] == CellType.Road
            && result[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, threeWay, Quaternion.Euler(0, 90, 0));
        }
        else if (result[3] == CellType.Road && result[0] == CellType.Road
            && result[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, threeWay, Quaternion.Euler(0, 180, 0));
        }
        else if (result[0] == CellType.Road && result[1] == CellType.Road
            && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, threeWay, Quaternion.Euler(0, 270, 0));
        }
    }

    private void CreateCorner(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[1] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, corner, Quaternion.Euler(0, 90, 0));
        }
        else if (result[2] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, corner, Quaternion.Euler(0, 180, 0));
        }
        else if (result[3] == CellType.Road && result[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, corner, Quaternion.Euler(0, 270, 0));
        }
        else if (result[0] == CellType.Road && result[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, corner, Quaternion.identity);
        }
    }

    private bool CreateStraightRoad(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[0] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, roadStraight, Quaternion.identity);
            return true;
        }
        else if (result[1] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, roadStraight, Quaternion.Euler(0, 90, 0));
            return true;
        }
        return false;
    }

    private void CreateDeadEnd(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, deadEnd, Quaternion.Euler(0, 270, 0));
        }
        else if (result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, deadEnd, Quaternion.identity);
        }
        else if (result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, deadEnd, Quaternion.Euler(0, 90, 0));
        }
        else if (result[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, deadEnd, Quaternion.Euler(0, 180, 0));
        }
    }
}