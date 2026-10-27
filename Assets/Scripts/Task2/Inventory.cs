using UnityEngine;
using System.Collections.Generic;

public class Inventory
{
    private static int capacity = 10;
    private List<Item> inventoryList = new List<Item>();


    public void AddItem(Item item)
    {
        Item newItemToAdd = new Item(item.name);

        if (HasItem(newItemToAdd.name))
        {
            Item currentItem = FindItem(newItemToAdd.name);
            currentItem.count += item.count;
        }
        else if (inventoryList.Count != capacity) 
        {
            Item addItem = new Item(item.name, item.count);
            inventoryList.Add(addItem);
        }
        else Debug.Log("Инвентарь заполнен");
    }

    public void RemoveItem(Item item)
    {
        if (HasItem(item.name))
        {
            Item itemToRemove = FindItem(item.name);
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
    private Item FindItem(string name)
    {
        return inventoryList.Find(item => item.name == name);
    }
    
}

public class Item{
    public string name;
    public int count;

    public Item (string name, int count = 1)
    {
        this.name = name;
        this.count = count;
    }
}
