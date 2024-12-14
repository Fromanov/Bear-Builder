using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnRoadPlacement, OnHousePlacement, OnWindmillPlacement, OnApiaryPlacement, OnShopPlacement, OnElectricGeneratorPlacement;
    public Button showBuildPanelButton, placeRoadButton, placeHouseButton, placeWindmillButton, placeApiaryButton, placeShopButton, placeElectricGeneratorButton;

    public Color outlineColor;
    List<Button> buttonList;

    public Animator animator;

    private void Start()
    {
        buttonList = new List<Button> { placeHouseButton, placeRoadButton, placeWindmillButton, placeApiaryButton, placeShopButton, placeElectricGeneratorButton };

        placeRoadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();
        });
        placeHouseButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeHouseButton);
            OnHousePlacement?.Invoke();

        });
        placeWindmillButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeWindmillButton);
            OnWindmillPlacement?.Invoke();
        });
        placeApiaryButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeApiaryButton);
            OnApiaryPlacement?.Invoke();
        });
        placeShopButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeShopButton);
            OnShopPlacement?.Invoke();
        });
        placeElectricGeneratorButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeElectricGeneratorButton);
            OnElectricGeneratorPlacement?.Invoke();
        });
        showBuildPanelButton.onClick.AddListener(() =>
        {
            bool tempBool = animator.GetBool("isShow");
            animator.SetBool("isShow", !tempBool);
        });
    }


    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        foreach (Button button in buttonList)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }
}
