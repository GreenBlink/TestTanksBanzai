using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour
{
    public Text weaponNameText;
    public Text healthText;
    public Text armorText;

    public void Init(Player player)
    {
        SetHeathText(player.healthPoints, player.healthPoints);
        SetArmorText(player.armor);
        player.eventDamage += SetHeathText;
    }

    public void SetNameWeapon(string nameWeapon)
    {
        weaponNameText.text = nameWeapon;
    }

    public void SetHeathText(float points, float maxPoints)
    {
        healthText.text = (points / maxPoints * 100).ToString("0") + "%";
    }

    public void SetArmorText(float points)
    {
        armorText.text = (points * 100).ToString("0") + "%";
    }
}
