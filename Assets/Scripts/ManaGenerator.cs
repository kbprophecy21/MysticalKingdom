using UnityEngine;

public class ManaGenerator : MonoBehaviour
{
    public float manaPerSecond = 5f; // Amount of mana generated per second
    public float maxMana = 100f; // Maximum mana capacity
    private float currentMana = 0f; // Current mana amount

    private float timer;
    

    void Update()
    {
        GenerateMana();
    }

    void GenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaPerSecond * Time.deltaTime;
            currentMana = Mathf.Min(currentMana, maxMana); // Clamp to max mana
        }
    }

    public float GetCurrentMana()
    {
        return currentMana;
    }
}