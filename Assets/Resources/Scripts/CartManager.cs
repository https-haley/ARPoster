using System.Collections.Generic;
using UnityEngine;

public static class CartManager
{
    public static Dictionary<string, CartItem> cartItems = new Dictionary<string, CartItem>();

    public static void AddItem(string name, string price)
    {
        if (cartItems.ContainsKey(name))
        {
            cartItems[name].quantity++;
        }
        else
        {
            cartItems.Add(name, new CartItem(name, price, 1));
        }

        Debug.Log(name + " added to cart.");
    }

    public static void ClearCart()
    {
        cartItems.Clear();
    }
}

public class CartItem
{
    public string itemName;
    public string itemPrice;
    public int quantity;

    public CartItem(string name, string price, int amount)
    {
        itemName = name;
        itemPrice = price;
        quantity = amount;
    }
}