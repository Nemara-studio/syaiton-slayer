using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public enum ShopItem
{
    DAMAGE,
    MAXHP,
    REGEN,
    MOVESPEED,
    CRITICAL
}

public class Shop : MonoBehaviour
{
    [Serializable]
    public struct Item
    {
        public ShopItem type;
        public ShopItemUI itemUI;
        public string name;
        public int value;
        public int maxValue;
        public int price;
    }

    public static Shop singleton;

    [Header("UI")]
    public GameObject shopObject;
    public GameObject descriptionBoxObject;
    public TMP_Text descriptionText;

    [Header("PROPERTIES")]
    public Player player;
    public List<Item> items;
    private Item selectedItem;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetShopUI();
        UpdateShopItemUI();
    }

    private void Update()
    {
        InputPlayer();
    }

    private void InputPlayer()
    {
        if (GameLogic.singleton.gameState == GameLogic.GameState.PREP_PHASE && Input.GetKeyDown(KeyCode.B))
        {
            shopObject.SetActive(!shopObject.gameObject.activeSelf);
        }
    }

    #region UI Function
    public void SetShopUI()
    {
        foreach (Item item in items)
        {
            item.itemUI.itemBuyButton.onClick.AddListener(() => SetSelectedItem(item.type));
            item.itemUI.itemBuyButton.onClick.AddListener(() => ShowConfirmationBox());
        }
    }

    public void UpdateShopItemUI()
    {
        foreach (Item item in items)
        {
            switch (item.type)
            {
                case ShopItem.DAMAGE:
                    item.itemUI.descItemText.text = $"{item.name}: {player.damage}";
                    item.itemUI.itemBuyButton.gameObject.SetActive(player.damage < item.maxValue);
                    break;
                case ShopItem.MAXHP:
                    item.itemUI.descItemText.text = $"{item.name}: {player.maxHealthPoint}";
                    item.itemUI.itemBuyButton.gameObject.SetActive(player.maxHealthPoint < item.maxValue);
                    break;
                case ShopItem.REGEN:
                    item.itemUI.descItemText.text = $"{item.name}";
                    item.itemUI.itemBuyButton.gameObject.SetActive(player.currentHealthPoint < player.maxHealthPoint);
                    break;
                case ShopItem.MOVESPEED:
                    item.itemUI.descItemText.text = $"{item.name}: {player.moveSpeed}";
                    item.itemUI.itemBuyButton.gameObject.SetActive(player.moveSpeed < item.maxValue);
                    break;
                case ShopItem.CRITICAL:
                    item.itemUI.descItemText.text = $"{item.name}";
                    // item.itemUI.itemBuyButton.gameObject.SetActive(player.damage < item.maxValue);
                    break;
                default:
                    break;
            }
        }
    }

    public void ShowConfirmationBox()
    {
        descriptionText.text = $"{selectedItem.name} +{selectedItem.value} = {selectedItem.price} Points";
        descriptionBoxObject.SetActive(true);
    }

    public void UnshowConfirmationBox()
    {
        descriptionBoxObject.SetActive(false);
    }
    #endregion

    public void SetSelectedItem(ShopItem item)
    {
        selectedItem = items.Find(x => x.type == item);
    }

    public void Buy()
    {
        if (GameLogic.singleton.point < selectedItem.price) return;

        switch (selectedItem.type)
        {
            case ShopItem.DAMAGE:
                Debug.Log($"Attack upgraded...");
                player.damage += selectedItem.value;
                break;
            case ShopItem.CRITICAL:
                Debug.Log($"Critical upgraded...");
                GameLogic.singleton.point -= selectedItem.price;
                break;
            case ShopItem.MAXHP:
                Debug.Log($"Max HP upgraded...");
                player.maxHealthPoint += selectedItem.value;
                break;
            case ShopItem.REGEN:
                Debug.Log($"Max HP upgraded...");
                player.currentHealthPoint += selectedItem.value;
                break;
            case ShopItem.MOVESPEED:
                Debug.Log($"Movement Spd upgraded...");
                player.moveSpeed += selectedItem.value;
                break;
            default:
                break;
        }

        GameLogic.singleton.AddPoint(-selectedItem.price);

        UpdateShopItemUI();
        UnshowConfirmationBox();
    }
}
