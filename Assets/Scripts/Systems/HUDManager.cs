using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("Resource UI")]
    public TMP_Text goldText;
    public TMP_Text manaText;

    [Header("Progress UI")]
    public TMP_Text xpText; // optional
    public TMP_Text levelText; // optional

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateGold(int amount)
    {
        goldText.text = "Gold: " + amount;
    }

    public void UpdateMana(int amount)
    {
        manaText.text = "Mana: " + amount;
    }

    public void UpdateLevel(int level)
    {
        levelText.text = "Level: " + level;
    }

    public void UpdateXP(int xp)
    {
        xpText.text = "XP: " + xp;
    }
}
