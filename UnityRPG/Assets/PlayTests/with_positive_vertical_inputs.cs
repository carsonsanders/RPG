using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace a_player
{
    public class with_positive_vertical_inputs
    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            //ARRANGE
            yield return helpers.LoadMovementTestsScene();
            var player = helpers.GetPlayer();

            //ACT
            PlayerInput.Instance.Vertical.Returns(1f);
            PlayerInput.Instance.Horizontal.Returns(1f);
            
            float startingZPosition = player.transform.position.z;
            float startingXPosition = player.transform.position.x;

            yield return new WaitForSeconds(2f);

            float endingZPosition = player.transform.position.z;
            float endingXPosition = player.transform.position.x;
            
            //ASSERT
            Assert.Greater(endingZPosition, startingZPosition);
            Assert.Greater(endingXPosition, startingXPosition);
        }
    }
    
    public class with_negative_vertical_inputs
    {
        [UnityTest]
        public IEnumerator moves_backward()
        {
            //ARRANGE
            yield return helpers.LoadMovementTestsScene();
            var player = helpers.GetPlayer();

            //ACT
            PlayerInput.Instance.Vertical.Returns(-1f);
            PlayerInput.Instance.Horizontal.Returns(-1f);
            
            float startingZPosition = player.transform.position.z;
            float startingXPosition = player.transform.position.x;

            yield return new WaitForSeconds(2f);

            float endingZPosition = player.transform.position.z;
            float endingXPosition = player.transform.position.x;
            
            //ASSERT
            Assert.Less(endingZPosition, startingZPosition);
            Assert.Less(endingXPosition, startingXPosition);
        }
    }

    public class with_negative_mouse_x
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            //ARRANGE
            yield return helpers.LoadMovementTestsScene();
            var player = helpers.GetPlayer();

            PlayerInput.Instance.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.2f);

            float turnAmount = helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Less(turnAmount, 0);
        }
    }
    
    public class with_positive_mouse_x
    {
        [UnityTest]
        public IEnumerator turns_right()
        {
            //ARRANGE
            yield return helpers.LoadMovementTestsScene();
            var player = helpers.GetPlayer();

            PlayerInput.Instance.MouseX.Returns(1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.2f);

            float turnAmount = helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Greater(turnAmount, 0);
        }
    }

    public class itemPickup
    {
        [UnityTest]
        public IEnumerator item_pickup()
        {
            //Arrange
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
            yield return helpers.LoadMovementTestsScene();
            var player = helpers.GetPlayer();

            //ACT (move forward to pick it up)
            PlayerInput.Instance.Vertical.Returns(1f);
            Item item = Object.FindObjectOfType<Item>();

            yield return new WaitForSeconds(1f);
            //ASSERT
            Assert.AreSame(item, player.GetComponent<Inventory>().ActiveItem);
        }
    }
    
    public class changes_crosshair_to_item_crosshair
    {
        
        [UnityTest]
        public IEnumerator crosshair_change()
        {
            //Arrange
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
            yield return helpers.LoadItemTestsScene();
            var player = helpers.GetPlayer();
            var crosshair = Object.FindObjectOfType<Crosshair>();

            //ACT (move forward to pick it up)
            PlayerInput.Instance.Vertical.Returns(1f);
            Item item = Object.FindObjectOfType<Item>();
            
            Assert.AreNotSame(item.CrosshairDefinition.Sprite, crosshair.GetComponent<Image>().sprite);

            yield return new WaitForSeconds(1f);
            //ASSERT
            Assert.AreEqual(item.CrosshairDefinition.Sprite, crosshair.GetComponent<Image>().sprite);
        }
        
       
        
    }
}