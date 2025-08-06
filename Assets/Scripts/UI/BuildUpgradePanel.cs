using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class BuildUpgradePanel : MonoBehaviour
{
    [System.Serializable]
    public class TowerEntry
    {
        public string towerName;
        public GameObject ghostTowerPrefab;
        public Sprite icon;
        public int level;
        public int cost;
        public int damage;
        public int range;
        public int fireRate;

        public TowerEntry(string name, int lvl, int cst, int dmg, int rng, int fr)
        {
            towerName = name;
            level = lvl;
            cost = cst;
            damage = dmg;
            range = rng;
            fireRate = fr;
        }
    }
//--------------------------------------------------------------------------------//
    [Header("UI")]
    public Transform buttonContainer;
    public GameObject towerButtonPrefab;

    [Header("Tower List")]
    public List<TowerEntry> towers;

    [Header("Tower Data")]
    public List<TowerEntry> towerData;

    [Header("Systems")]
    public BuildingSystem buildingSystem;

//-------------------------------------------------------------------------------//
    private void Start()
    {
        foreach (TowerEntry tower in towers)
        {
            GameObject btnObj = Instantiate(towerButtonPrefab, buttonContainer);
            Button btn = btnObj.GetComponent<Button>();
            Image iconImage = btnObj.transform.GetChild(0).GetComponent<Image>();
            iconImage.sprite = tower.icon;

            btn.onClick.AddListener(() =>
            {
                buildingSystem.StartPlacingTower(tower.ghostTowerPrefab);
            });
        }
    }

}