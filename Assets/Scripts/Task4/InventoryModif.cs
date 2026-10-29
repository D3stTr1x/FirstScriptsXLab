using UnityEngine;
using System.Collections.Generic;

public class InventoryModif
{
    private static int capacity = 1;
    private List<ItemModif> inventoryList = new List<ItemModif>();

    public string name
    {
        get
        {
            if (inventoryList.Count > 0)
                return inventoryList[0].name;
            return null;
        }
    }

    public void AddItem(ItemModif item)
    {
        ItemModif newItemToAdd = new ItemModif(item.name);

        if (HasWeaponOnHand())
        {
            Debug.Log("Инвентарь заполнен");
        }
        else
        {
            ItemModif addItem = new ItemModif(item.name, item.count);
            inventoryList.Add(addItem);
        }
    }

    public void RemoveItem(ItemModif item)
    {
        if (HasItem(item.name))
        {
            ItemModif itemToRemove = FindItem(item.name);
            if (itemToRemove.count > item.count)
            {
                itemToRemove.count -= item.count;
            }
            else if (itemToRemove.count == 1 || itemToRemove.count == item.count)
            {
                inventoryList.Remove(itemToRemove);
            }
            else {
                Debug.Log("Error невозможно удалить больше, чем есть");
            }
        }
        else{
            Debug.Log("Error Элемент отсутсвует");
        }
    }

    public void Print()
    {
        if (inventoryList.Count == 0)
            Debug.Log("Инвентарь пуст");
        string result = "";
        int fullCount = 0;
        foreach (var item in inventoryList)
        {
            result += $" {item.name}: {item.count} ;";
            fullCount += item.count;
        }
        Debug.Log(result);
        Debug.Log($"Уникальных предметов: {inventoryList.Count}, Всего предметов: {fullCount}");
    }

    private bool HasItem(string name)
    {
        if (FindItem(name) == null)
            return false;
        return true;
    }
    private ItemModif FindItem(string name)
    {
        return inventoryList.Find(item => item.name == name);
    }
    private bool HasWeaponOnHand()
    {
        if (inventoryList.Count == 0)
            return false;
        return true;
    }
}

public class ItemModif{
    public string name;
    public int count;

    public ItemModif (string name, int count = 1)
    {
        this.name = name;
        this.count = count;
    }
}
