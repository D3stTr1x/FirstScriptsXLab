using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private TMP_Dropdown menu;
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
        int index  = menu.value;
        string itemName = menu.options[index].text;
        if (itemName != null)
        {
            Item newItem = new Item(itemName);
            inventory.AddItem(newItem);
        }
    }

    public void RemoveButton(){
        int index  = menu.value;
        string itemName = menu.options[index].text;
        if (itemName != null)
        {
            Item newItem = new Item(itemName);
            inventory.RemoveItem(newItem);
        }
    }

    public void PrintButton()
    {
        inventory.Print();
    }
}
