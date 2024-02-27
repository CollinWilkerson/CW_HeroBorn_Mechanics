using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//initalizing a generic with type T
public class InventoryList<T> where T: class
{
    //creates variable and backing variable of type T
    private T _item;
    public T item
    {
        get;
    }
    public InventoryList()
    {
        Debug.Log("Generic list initalized...");
    }
    //creates a function using type T
    public void SetItem(T newItem)
    {
        _item = newItem;
        Debug.Log("New item added");
    }
}
