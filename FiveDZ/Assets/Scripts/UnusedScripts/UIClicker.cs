using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class UIClicker : MonoBehaviour
{
    [SerializeField]
    private GameObject uiCanvas;
    private GraphicRaycaster uiRaycaster;
    private PointerEventData clickData;
    private List<RaycastResult> clickResults;
    [SerializeField]
    private Transform mainMenuButtons;
    [SerializeField]
    private Transform menuButtons;
    [SerializeField]
    private Transform menuToggles;
    [SerializeField]
    private Transform menuDrops;
    [SerializeField]
    private Transform menuInput;
    [SerializeField]
    private Transform menuScrollView;
    [SerializeField]
    private TextMeshProUGUI textUI;
    private TextMeshProUGUI toggleText1;
    private TextMeshProUGUI toggleText2;
    private TextMeshProUGUI toggleText3;
    private string currentButtonText;
    private string currentToggleText;
    private string currentDropsText;
    private bool disableButtons;
    private Image imageButtonBack;
    private Image imageButtons;
    private Image imageToggles;
    private Image imageDrops;
    private Image imageInput;
    private Image imageScrollView;
    private Image imageButton1;
    private Image imageButton2;
    private Image imageButton3;
    private Toggle toggle1;
    private Toggle toggle2;
    private Toggle toggle3;
    private TMP_Dropdown dropdawn;


    void Start()
    {
        uiRaycaster = uiCanvas.GetComponent<GraphicRaycaster>();
        clickData = new PointerEventData(EventSystem.current);
        clickResults = new List<RaycastResult>();
        textUI.gameObject.SetActive(false);
        imageButtonBack= uiCanvas.transform.GetChild(2).GetChild(2).GetComponent<Image>();
        imageButtonBack.gameObject.SetActive(false);
        imageButtons = mainMenuButtons.GetChild(0).GetChild(0).GetComponent<Image>();
        currentButtonText = "Buttons";
        disableButtons = false;
        imageButton1 = menuButtons.GetChild(0).GetChild(0).GetComponent<Image>();
        imageButton2 = menuButtons.GetChild(1).GetChild(0).GetComponent<Image>();
        imageButton3 = menuButtons.GetChild(2).GetChild(0).GetComponent<Image>();

        imageToggles = mainMenuButtons.GetChild(1).GetChild(0).GetComponent<Image>();
        toggle1 = menuToggles.GetChild(0).GetChild(0).GetComponent<Toggle>();
        toggle2 = menuToggles.GetChild(1).GetChild(0).GetComponent<Toggle>();
        toggle3 = menuToggles.GetChild(2).GetChild(0).GetComponent<Toggle>();
        toggleText1 = toggle1.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        toggleText2 = toggle2.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        toggleText3 = toggle3.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        currentToggleText = toggleText1.text;
        toggle1.isOn = true;
        toggle2.isOn = false;
        toggle3.isOn = false;
        toggle1.onValueChanged.AddListener(delegate { MenuToggle1Clicked(); });
        toggle2.onValueChanged.AddListener(delegate { MenuToggle2Clicked(); });
        toggle3.onValueChanged.AddListener(delegate { MenuToggle3Clicked(); });

        imageDrops = mainMenuButtons.GetChild(2).GetChild(0).GetComponent<Image>();
        dropdawn = menuDrops.GetChild(0).GetChild(0).GetComponent<TMP_Dropdown>();
        dropdawn.onValueChanged.AddListener(delegate { MenuDropsClicked(); });
        currentDropsText= dropdawn.options[dropdawn.value].text;

        imageInput = mainMenuButtons.GetChild(3).GetChild(0).GetComponent<Image>();
        imageScrollView = mainMenuButtons.GetChild(4).GetChild(0).GetComponent<Image>();
    }
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GetUiElementsClicked();
        }
    }
    private void GetUiElementsClicked()
        {
        clickData.position = Mouse.current.position.ReadValue();
        clickResults.Clear();
        uiRaycaster.Raycast(clickData, clickResults);
        foreach (RaycastResult result in clickResults)
        {
            GameObject uiElement = result.gameObject;
            MainMenuClicked(uiElement);
            MenuButtonsClicked(uiElement);
        }
        }
    private void MainMenuClicked(GameObject uiElement)
    {
            if (uiElement== imageButtons.gameObject)
            {
                imageButtonBack.gameObject.SetActive(true);
                disableButtons = false;
                imageButton1.SetTransparency(255);
                imageButton2.SetTransparency(255);
                imageButton3.SetTransparency(255);
                mainMenuButtons.gameObject.SetActive(false);
                menuButtons.gameObject.SetActive(true);
                textUI.gameObject.SetActive(true);
                textUI.text= currentButtonText;
            }
            if (uiElement == imageToggles.gameObject)
            {
                imageButtonBack.gameObject.SetActive(true);
                mainMenuButtons.gameObject.SetActive(false);
                menuToggles.gameObject.SetActive(true);
                textUI.gameObject.SetActive(true);
                textUI.text = currentToggleText;
            }
            if (uiElement == imageDrops.gameObject)
            {
                imageButtonBack.gameObject.SetActive(true);
                mainMenuButtons.gameObject.SetActive(false);
                menuDrops.gameObject.SetActive(true);
                textUI.gameObject.SetActive(true);
                textUI.text = currentDropsText;
            }
            if (uiElement == imageInput.gameObject)
            {
                imageButtonBack.gameObject.SetActive(true);
                mainMenuButtons.gameObject.SetActive(false);
                menuInput.gameObject.SetActive(true);
            }
            if (uiElement == imageScrollView.gameObject)
            {
                imageButtonBack.gameObject.SetActive(true);
                mainMenuButtons.gameObject.SetActive(false);
                menuScrollView.gameObject.SetActive(true);
            }
            if (uiElement.tag == "ButtonBack")
            {
                imageButtonBack.gameObject.SetActive(false);
                menuButtons.gameObject.SetActive(false);
                menuToggles.gameObject.SetActive(false);
                menuDrops.gameObject.SetActive(false);
                menuInput.gameObject.SetActive(false);
                menuScrollView.gameObject.SetActive(false);
                textUI.gameObject.SetActive(false);
                mainMenuButtons.gameObject.SetActive(true);
            }
    }
    private void MenuButtonsClicked(GameObject uiElement)
    {
        if(disableButtons == false) 
        {

                if (uiElement == imageButton1.gameObject)
                {
                    currentButtonText = "One Clicked";
                    textUI.text = currentButtonText;
                }
                if (uiElement == imageButton2.gameObject)
                {
                    currentButtonText = "Two Clicked";
                    textUI.text = currentButtonText;
                }
                if (uiElement == imageButton3.gameObject)
                {
                    imageButton1.SetTransparency(100);
                    imageButton2.SetTransparency(100);
                    imageButton3.SetTransparency(100);
                    disableButtons = true;
                }
        }
    }
    private void MenuToggle1Clicked()
    {
        if (toggle1.isOn == true)
        {
            toggle2.isOn = false;
            toggle3.isOn = false;
            currentToggleText = toggleText1.text;
            textUI.text = currentToggleText;
        }
    }
    private void MenuToggle2Clicked()
    {
        if (toggle2.isOn == true)
        {
            toggle1.isOn = false;
            toggle3.isOn = false;
            currentToggleText = toggleText2.text;
            textUI.text = currentToggleText;
        }
    }
    private void MenuToggle3Clicked()
    {
        if (toggle3.isOn == true)
        {
            toggle1.isOn = false;
            toggle2.isOn = false;
            currentToggleText = toggleText3.text;
            textUI.text = currentToggleText;
        }
    }
    private void MenuDropsClicked()
        {
            currentDropsText=dropdawn.options[dropdawn.value].text;
            textUI.text = currentDropsText;
        }
    
}
