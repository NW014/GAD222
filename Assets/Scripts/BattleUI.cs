using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI energyText;

    public Slider healthSlider;
    public Slider energySlider;

    public void OnEnable()
    {
        
    }

    public void SetText(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = $"Lv.{unit.unitLevel}";

        if (healthText != null)
        {
            healthText.text = $"{unit.currentHealth}/{unit.maxHealth}";
        }

        if (energyText != null)
        {
            energyText.text = $"{unit.energy}%";
        }

        if (healthSlider != null)
        {
            healthSlider.maxValue = unit.maxHealth;
            healthSlider.value = unit.currentHealth;
        }

        if (energySlider != null)
        {
            energySlider.maxValue = unit.maxEnergy;
            energySlider.value = unit.energy;
        }
    }

    public void SetHealth(float health, float maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }

        if (healthText != null)
        {
           healthText.text = $"{health}/{maxHealth}"; 
        }
        
    }

    public void SetEnergy(float energy)
    {
        if (energySlider != null)
        {
            energySlider.value = energy;
        }

        if (energyText != null)
        {
           energyText.text = $"{energy}%"; 
        }
        
    }
}
