using System.Collections;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class with_positive_vertical_inputs
    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            //ARRANGE
            var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.localScale = new Vector3(50, 0.1f, 50);
            floor.transform.position = new Vector3(0, -3, 0);

            var playerObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerObject.AddComponent<CharacterController>();
            playerObject.transform.position = new Vector3(0, 10, 0);

            //ACT
            Player player = playerObject.AddComponent<Player>();
            var testPlayerInput= new TestPlayerInput();
            player.playerInput = testPlayerInput;
            
            testPlayerInput.Vertical = 1f;
            testPlayerInput.Horizontal = 1f;
            
            float startingZPosition = player.transform.position.z;
            float startingXPosition = player.transform.position.x;

            yield return new WaitForSeconds(5f);

            float endingZPosition = player.transform.position.z;
            float endingXPosition = player.transform.position.x;
            
            //ASSERT
            Assert.Greater(endingZPosition, startingZPosition);
            Assert.Greater(endingXPosition, startingXPosition);
        }
    }

    public class TestPlayerInput : IPlayerInput
    {
        public float Vertical { get; set; }
        public float Horizontal { get; set; }
        
    }
}