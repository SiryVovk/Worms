using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventoryBackground;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private PlayerInventory playerInventory;

    private List<Button> buttonList = new List<Button> (); 

    private void Awake()
    {
        playerInventory.OnInventoryOpened += ToggleInventory;

        BuildInventory();
    }

    private void OnDestroy()
    {
        playerInventory.OnInventoryOpened -= ToggleInventory;
    }

    private void ToggleInventory()
    {
        inventoryBackground.SetActive(!inventoryBackground.activeSelf);

        foreach (var button in buttonList)
        {
            button.gameObject.SetActive(!button.gameObject.activeSelf);
        }
    }

    private void BuildInventory()
    {
        var weapons = playerInventory.GetWeapons();

        foreach (var weapon in weapons)
        {
            var item = Instantiate(inventoryItemPrefab, inventoryPanel.transform);
            var button = item.GetComponent<Button>();
            button.image.sprite = weapon.WeaponUI.UIWeaponSprite;
            buttonList.Add(button);
            button.onClick.AddListener(() => playerInventory.EquipWeapon(weapon));
        }

        foreach (var button in buttonList)
        {
            button.gameObject.SetActive(!button.gameObject.activeSelf);
        }
    }
}
