using UnityEngine;



public class MenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject scrollMenuPanel;
    public GameObject kingdomStatsPanel;
    public GameObject buildUpgradePanel;
    public GameObject shopPanel;
    public GameObject settingsPanel;


    [Header("Tower System")]
    public BuildingSystem buildingSystem;



    private void HideAllPanels()
    {
        kingdomStatsPanel.SetActive(false);
        buildUpgradePanel.SetActive(false);
        shopPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void onClickScrollMenuButton()
    {
        bool isActive = scrollMenuPanel.activeSelf;

        if (isActive)
        {
            scrollMenuPanel.SetActive(false);
            HideAllPanels();
        }
        else
        {
            scrollMenuPanel.SetActive(true);
            ShowOnlyPanel(kingdomStatsPanel);

        }
    }

    private void ShowOnlyPanel(GameObject panelToShow)
    {
        HideAllPanels();
        panelToShow.SetActive(true);
    }


    public void onClickKingdomStatsButton() => ShowOnlyPanel(kingdomStatsPanel);
    public void onClickBuildUpgradeButton() => ShowOnlyPanel(buildUpgradePanel);
    public void onClickShopButton() => ShowOnlyPanel(shopPanel);
    public void onClickSettingsButton() => ShowOnlyPanel(settingsPanel); 
}