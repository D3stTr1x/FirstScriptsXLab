using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private TMP_Dropdown m_menu;
    [SerializeField] private TMP_InputField m_count;
    
    private void Start()
    {
        inventory = new Inventory();
    }
    
    public void AddButton(){
        int currentCount;
        int index  = m_menu.value;
        string itemName = m_menu.options[index].text;
        
        if (string.IsNullOrEmpty(m_count.text))
        {
            currentCount = 1;
        }
        else 
        {
            currentCount = Convert.ToInt32(m_count.text);
        }
        
        if (itemName != null)
        {
            Item newItem = new Item(itemName, currentCount);
            inventory.AddItem(newItem);
        }
    }

    public void RemoveButton(){
        int currentCount;
        int index  = m_menu.value;
        string itemName = m_menu.options[index].text;
        if (string.IsNullOrEmpty(m_count.text))
            currentCount = 1;
        else   
            currentCount = Convert.ToInt32(m_count.text);
        if (itemName != null || string.IsNullOrEmpty(itemName))
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
