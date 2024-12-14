using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public RoadManager roadManager;
    public InputManager inputManager;

    public UIController uiController;

    public StructureManager structureManager;

    private void Start()
    {
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnWindmillPlacement += WindmillPlacementHandler;
    }

    private void StructurePlacementHandler(CellType type)
    {
        ClearInputActions();
        inputManager.OnMouseClick += structureManager.PlaceStructure;
        inputManager.OnRMBUp += roadManager.DeleteObject;
        structureManager.structureType = type;
    }

    private void WindmillPlacementHandler()
    {
        StructurePlacementHandler(CellType.Windmill);
    }

    private void HousePlacementHandler()
    {
        StructurePlacementHandler(CellType.House);
    }

    private void ApiaryPlacementHandler()
    {
        StructurePlacementHandler(CellType.Apiary);
    }

    private void ShopPlacementHandler()
    {
        StructurePlacementHandler(CellType.Shop);
    }

    private void ElectricGeneratorPlacementHandler()
    {
        StructurePlacementHandler(CellType.ElectricGenerator);
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();

        inputManager.OnMouseClick += roadManager.PlaceRoad;
        inputManager.OnMouseHold += roadManager.PlaceRoad;
        inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
        inputManager.OnRMBUp += roadManager.DeleteObject;
    }

    private void ClearInputActions()
    {
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
        inputManager.OnRMBUp = null;
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
    }
}
