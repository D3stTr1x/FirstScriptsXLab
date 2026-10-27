using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private TMP_Dropdown menu;
    [SerializeField] private TMP_InputField count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = new Inventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddButton(){
        int currentCount;
        int index  = menu.value;
        string itemName = menu.options[index].text;
        if (string.IsNullOrEmpty(count.text))
            currentCount = 1;           
        else 
            currentCount = Convert.ToInt32(count.text);
        if (itemName != null)
        {
            Item newItem = new Item(itemName, currentCount);
            inventory.AddItem(newItem);
        }
    }

    public void RemoveButton(){
        int currentCount;
        int index  = menu.value;
        string itemName = menu.options[index].text;
        if (string.IsNullOrEmpty(count.text))
            currentCount = 1;
        else   
            currentCount = Convert.ToInt32(count.text);
        if (itemName != null)
        {
            Item newItem = new Item(itemName, currentCount);
            inventory.RemoveItem(newItem);
        }
    }

    public void PrintButton()
    {
        inventory.Print();
    }
}
