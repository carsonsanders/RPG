using System.Linq;
using NUnit.Framework;

namespace PlayTests
{
    public class ui_selection_cursor
    {
        [Test]
        public void with_no_item_selected_show_no_icon()
        {
            var uiCursor = inventory_helpers.GetSelectionCursor();
            Assert.IsFalse(uiCursor.IconVisible);
        }

        [Test]
        public void with_item_selected_shows_item()
        {
            var inventoryPanel = inventory_helpers.GetInventoryPanelWithItems(1);
            var uiCursor = inventory_helpers.GetSelectionCursor();
            inventoryPanel.Slots.First().OnPointerClick(null);
            Assert.IsTrue(uiCursor.IconVisible);
        }
    }
}