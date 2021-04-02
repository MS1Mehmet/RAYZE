using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code kopiert und modifiziert von Tony Bhimani, Link verfügbar unter: https://www.youtube.com/user/TonyBhimani
public class AssetPalette : MonoBehaviour
{
    // Item Prefabs
    public GameObject[] itemPrefabs = new GameObject[7];
    public enum ItemList
    {
        ExtraLife,
        LifeEnergyBig,
        LifeEnergySmall,
        MagnetBeam,
        WeaponEnergyBig,
        WeaponEnergySmall,
        Yashichi,

    }
    public ItemList itemList;

}
