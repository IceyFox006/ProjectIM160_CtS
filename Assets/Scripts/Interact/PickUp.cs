/*
 * PickUp.cs
 * Marlow Greenan
 * 2/19/2025
 * 
 * Placed on a gameObject, allowing a player to put it in their hand.
 */
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Item _item;

    public Item Item { get => _item; set => _item = value; }

    /// <summary>
    /// Picks up item and adds it to the player inventory.
    /// </summary>
    public void OnMouseDown()
    {
        if (_item != null && !InventoryManager.Instance.CheckFull())
        {
            GameManager.Instance.InventoryManager.AddItem(_item);
            AudioManager.Instance.PlayPickUpAudio();
            Destroy(gameObject);
        }
        else if (InventoryManager.Instance.CheckFull())
            GameManager.Instance.Message.NewMessage("Your inventory is full.");
    }
}
