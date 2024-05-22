using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public List<WeaponController> availableWeapons; // List of available weapons in the store
    public int[] weaponPrices; // Corresponding prices of the weapons
    public WeaponController playerWeapon; // Reference to the player's current weapon

    public void BuyWeapon(int weaponIndex)
    {
        int weaponPrice = weaponPrices[weaponIndex];
        if (GameManager.instance.score >= weaponPrice)
        {
            GameManager.instance.score -= weaponPrice;
            EquipWeapon(weaponIndex);
            // Update UI or other game elements
        }
    }

    void EquipWeapon(int weaponIndex)
    {
        playerWeapon = availableWeapons[weaponIndex];
        // Your code to equip the weapon to the player
    }
}


