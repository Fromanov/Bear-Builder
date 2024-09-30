using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public List<Vector3Int> tempPlacementPos = new List<Vector3Int>();
    public List<Vector3Int> roadsPosToRecheck = new List<Vector3Int>();

    public RoadFixer roadFixer;

    private Vector3Int startPos;
    private bool placementMode = false;

    private void Start()
    {
        roadFixer = GetComponent<RoadFixer>();
    }

    public void PlaceRoad(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false) 
        {
            return;
        }
        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            return;
        }

        if(placementMode == false)
        {
            tempPlacementPos.Clear();
            roadsPosToRecheck.Clear();

            placementMode = true;
            startPos = position;

            tempPlacementPos.Add(position);
            placementManager.PlaceTempStructure(position, roadFixer.deadEnd, CellType.Road);            
        }
        else
        {
            placementManager.RemoveAllTempStructures();
            tempPlacementPos.Clear();

            foreach(Vector3Int posToFix in roadsPosToRecheck)
            {
                roadFixer.FixRoadAtPosition(placementManager, posToFix);
            }

            roadsPosToRecheck.Clear();

            tempPlacementPos = placementManager.GetPathBetween(startPos, position);

            foreach (Vector3Int tempPos in tempPlacementPos)
            {
                if (placementManager.CheckIfPositionIsFree(tempPos) == false)
                {
                    continue;
                }
                placementManager.PlaceTempStructure(tempPos, roadFixer.deadEnd, CellType.Road);
            }
        }

        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach(Vector3Int tempPos in tempPlacementPos)
        {
            roadFixer.FixRoadAtPosition(placementManager, tempPos);
            List<Vector3Int> neighbours = placementManager.GetNeighbourTypesFor(tempPos, CellType.Road);

            foreach (Vector3Int roadPos in neighbours)
            {
                if(roadsPosToRecheck.Contains(roadPos) == false)
                {
                    roadsPosToRecheck.Add(roadPos);
                }                
            }
        }

        foreach(Vector3Int positionsToFix in roadsPosToRecheck)
        {
            roadFixer.FixRoadAtPosition(placementManager, positionsToFix);
        }
    }

    public void FinishPlacigRoad()
    {
        placementMode = false;
        placementManager.AddTempStructuresToStructureDictionary();

        if(tempPlacementPos.Count > 0)
        {
            AudioPlayer.instance.PlayPlacementSound();
        }
        tempPlacementPos.Clear();
        startPos = Vector3Int.zero;
    }
}
