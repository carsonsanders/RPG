using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

namespace a_player
{
    public static class helpers
    {
        public static void CreateFloor()
        {
            var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.localScale = new Vector3(50, 0.1f, 50);
            floor.transform.position = new Vector3(0, -3, 0);
        }

        public static Player CreatePlayer()
        {
            var playerObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerObject.AddComponent<CharacterController>();
            playerObject.AddComponent<NavMeshAgent>();
            playerObject.transform.position = new Vector3(0, 10, 0);
            
            Player player = playerObject.AddComponent<Player>();

            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.playerInput = testPlayerInput;

            return player;
        }

        public static float CalculateTurn(Quaternion originalRotation, Quaternion transformRotation)
        {
            var cross = Vector3.Cross(originalRotation * Vector3.forward, transformRotation * Vector3.forward);
            var dot = Vector3.Dot(cross, Vector3.up);
            return dot;
        }
    }
    public class with_positive_vertical_inputs
    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            //ARRANGE
            helpers.CreateFloor();
            var player = helpers.CreatePlayer();

            //ACT
            player.playerInput.Vertical.Returns(1f);
            player.playerInput.Horizontal.Returns(1f);
            
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

    public class with_negative_mouse_x
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            //ARRANGE
            helpers.CreateFloor();
            var player = helpers.CreatePlayer();

            player.playerInput.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Less(turnAmount, 0);
        }
    }
}