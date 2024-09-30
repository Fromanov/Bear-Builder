using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public InputManager inputManager;
    public RoadManager roadManager;
    public UIController uiController;
    public StructureManager structureManager;

    private void Start()
    {
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnSpecialPlacement += SpecialPlacementHandler;
    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += structureManager.PlaceSpecial;
    }

    private void HousePlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += structureManager.PlaceHouse;
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += roadManager.PlaceRoad;
        inputManager.OnMouseHold += roadManager.PlaceRoad;
        inputManager.OnMouseUp += roadManager.FinishPlacigRoad;
    }

    private void ClearInputActions()
    {
        inputManager.OnMouseClick += null;
        inputManager.OnMouseHold += null;
        inputManager.OnMouseUp += null;
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x,0,
            inputManager.CameraMovementVector.y));
    }
}
