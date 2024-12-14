using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnRoadPlacement, OnHousePlacement, OnWindmillPlacement, OnApiaryPlacement, OnShopPlacement, OnElectricGeneratorPlacement;
    public Button showBuildPanelButton, placeRoadButton, placeHouseButton, placeWindmillButton, placeApiaryButton, placeShopButton, placeElectricGeneratorButton;
    public TMP_Text foodText, constructionPolymerText, bearsText, energyText, requiedBears, requiedEnergy;
    public ResourceManager manager;

    public Color outlineColor;
    List<Button> buttonList;

    public Animator animator;

    public GameObject popUpMessage;
    public TMP_Text popUpMessageText;

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

    private void FixedUpdate()
    {
        ShowCurrentResources();
    }

    private void ShowCurrentResources()
    {
        if(manager)
        {
            foodText.text = manager.honey.ToString("0");
            constructionPolymerText.text = manager.constructionPolymer.ToString("0");
            bearsText.text = manager.bears.ToString("0");
            requiedBears.text = manager.requiedBears.ToString("0");
            energyText.text = manager.energy.ToString("0");
            requiedEnergy.text = manager.requiedEnergy.ToString("0");
        }
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

    public void ShowPopUpMessage(string text)
    {
        popUpMessageText.text = text;
        popUpMessage.SetActive(true);
    }
}
