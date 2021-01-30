using System.Collections;
using System.Linq;
using a_player;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace PlayTests
{
    public class ui_selection_cursor
    {
        [Test]
        public void in_default_state_shows_no_icon()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var uiCursor = inventory_helpers.GetSelectionCursor();
            Assert.IsFalse(uiCursor.IconVisible);
            Assert.IsFalse(uiCursor.GetComponent<Image>().enabled);
        }

        [Test]
        public void with_item_selected_shows_item()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var uiCursor = inventory_helpers.GetSelectionCursor();
            inventoryPanel.Slots.First().OnPointerClick(null);
            Assert.IsTrue(uiCursor.IconVisible);
        }

        [Test]
        public void when_item_selected_sets_icon_image_to_correct_sprite()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var uiCursor = inventory_helpers.GetSelectionCursor();
            inventoryPanel.Slots.First().OnPointerClick(null);
            
            Assert.AreSame(inventoryPanel.Slots[0].Icon, uiCursor.Icon);
        }
        
        [Test]
        public void when_item_is_unselected_sets_icon_to_not_visible()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var uiCursor = inventory_helpers.GetSelectionCursor();
            
            Assert.IsFalse(uiCursor.IconVisible);
            inventoryPanel.Slots.First().OnPointerClick(null);
            Assert.IsTrue(uiCursor.IconVisible);
            inventoryPanel.Slots.First().OnPointerClick(null);
            Assert.IsFalse(uiCursor.IconVisible);
        }

        [UnityTest]
        public IEnumerator moves_with_mouse_cursor()
        {
            yield return helpers.LoadItemTestsScene();

            var uiCursor = Object.FindObjectOfType<UISelectionCursor>();

            PlayerInput.Instance = Substitute.For<IPlayerInput>();
            
            for(int i = 0; i < 100; i++)
            {
                var mousePosition = new Vector2(100 + i, 100 + i);
                PlayerInput.Instance.MousePosition.Returns(mousePosition);

                yield return null;

                Assert.AreEqual(new Vector3(mousePosition.x, mousePosition.y, 0), uiCursor.transform.position);
            }
        }

        [Test]
        public void disables_raycastTarget()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var uiCursor = inventory_helpers.GetSelectionCursor();

            var image = uiCursor.GetComponent<Image>();
            Assert.IsFalse(image.raycastTarget);
        }
        
    }
}