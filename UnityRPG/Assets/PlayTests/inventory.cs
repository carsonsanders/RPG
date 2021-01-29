using NUnit.Framework;
using UnityEngine;

namespace PlayTests
{
    public class inventory
    {
        // Add Items
        [Test]
        public void can_add_items()
        {
            Inventory inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            Item item = new GameObject("ITEM", typeof(SphereCollider)).AddComponent<Item>();
            inventory.Pickup(item);
            
            Assert.AreEqual(1, inventory.Count);
        }
        // Place Into specific slot
        [Test]
        public void can_add_item_to_specific_slot()
        {
            Inventory inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            Item item = new GameObject("ITEM", typeof(SphereCollider)).AddComponent<Item>();
            
            inventory.Pickup(item, 5);
            
            Assert.AreEqual(item,inventory.GetItemInSlot(5));
        }

        [Test]
        public void can_move_item_to_empty_slot()
        {
            Inventory inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            Item item = new GameObject("ITEM", typeof(SphereCollider)).AddComponent<Item>();
            inventory.Pickup(item);
            
            Assert.AreEqual(item,inventory.GetItemInSlot(0));

            inventory.Move(0, 4);
            
            Assert.AreEqual(item, inventory.GetItemInSlot(4));
        }
        
        // Change slots/move items
        // Remove Items
        // drop items
        // HotKey/Hotbar Assignment
        // change visuals
        //modify stats
        //persist & load
        
    }
}