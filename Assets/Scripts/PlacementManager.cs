using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    private Grid placementGrid;

    private Dictionary<Vector3Int, StructureModel> tempRoadObjects = 
        new Dictionary<Vector3Int, StructureModel>();
    private Dictionary<Vector3Int, StructureModel> structureDictionary =
        new Dictionary<Vector3Int, StructureModel>();

    private void Start()
    {
        placementGrid = new Grid(width, height);
    }

    internal bool CheckIfPositionInBound(Vector3Int position)
    {
        if(position.x >= 0 && position.x < width &&
            position.z >= 0 && position.z < height)
        {
            return true;
        }
        return false;
    }

    internal bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    private bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.z] == type;
    }

    internal void PlaceTempStructure(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        placementGrid[position.x, position.z] = type;

        StructureModel structure = CreateANewStructureModel(position, structurePrefab, type);

        tempRoadObjects.Add(position, structure);
    }

    private StructureModel CreateANewStructureModel(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        GameObject structure = new GameObject(type.ToString());

        structure.transform.SetParent(transform);
        structure.transform.localPosition = position;

        StructureModel structureModel = structure.AddComponent<StructureModel>();
        structureModel.CreateModel(structurePrefab);
        return structureModel;
    }

    public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
    {
        if(tempRoadObjects.ContainsKey(position))
        {
            tempRoadObjects[position].SwapModel(newModel, rotation);
        }
        else if(structureDictionary.ContainsKey(position))
        {
            tempRoadObjects[position].SwapModel(newModel, rotation);
        }
    }

    internal CellType[] GetNeighbourTypesFor(Vector3Int position)
    {
        return placementGrid.GetAllAdjacentCellTypes(position.x, position.z);
    }

    internal List<Vector3Int> GetNeighbourTypesFor(Vector3Int tempPos, CellType type)
    {
        List<Point> neighbourVertices = placementGrid.GetAdjacentCellsOfType(tempPos.x, 
            tempPos.z, type);
         List<Vector3Int> neighbours = new List<Vector3Int>();

        foreach(Point point in neighbourVertices)
        {
            neighbours.Add(new Vector3Int(point.X, 0, point.Y));
        }
        return neighbours;
    }

    internal List<Vector3Int> GetPathBetween(Vector3Int startPos, Vector3Int endPos)
    {
        List<Point> resultPath = GridSearch.AStarSearch(placementGrid, new Point(startPos.x,
            startPos.z), new Point(endPos.x, endPos.z));
        List<Vector3Int> path = new List<Vector3Int>();
        foreach (Point point in resultPath)
        {
            path.Add(new Vector3Int(point.X,0, point.Y));
        }
        return path;
    }

    internal void RemoveAllTempStructures()
    {
        foreach(StructureModel structure in tempRoadObjects.Values)
        {
            Vector3Int position = Vector3Int.RoundToInt(structure.transform.position);
            placementGrid[position.x, position.z] = CellType.Empty;
            Destroy(structure.gameObject);
        }
        tempRoadObjects.Clear();
    }

    internal void AddTempStructuresToStructureDictionary()
    {
        foreach(var structure in tempRoadObjects)
        {
            structureDictionary.Add(structure.Key, structure.Value);
            DestroyNatureAt(structure.Key);
        }
        tempRoadObjects.Clear();
    }

    private void DestroyNatureAt(Vector3Int position)
    {
        RaycastHit[] hits = Physics.BoxCastAll(position + new Vector3(0, 0.5f, 0),
            new Vector3(0.5f, 0.5f, 0.5f), transform.up, Quaternion.identity,
            1f, 1 << LayerMask.NameToLayer("Nature"));

        foreach (RaycastHit item in hits)
        {
            Destroy(item.collider.gameObject);
        }
    }

    internal void PlaceObjectOnTheMap(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        placementGrid[position.x, position.z] = type;

        StructureModel structure = CreateANewStructureModel(position, structurePrefab, type);

        structureDictionary.Add(position, structure);
        DestroyNatureAt(position);
    }
}
