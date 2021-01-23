using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace a_player
{
    public static class helpers
    {
        public static IEnumerator LoadMovementTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("MovementTests");
            while (operation.isDone == false)
                yield return null;
        }

        public static Player GetPlayer()
        {
            Player player = GameObject.FindObjectOfType<Player>();

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
            yield return helpers.LoadMovementTestsScene();
            var player = helpers.GetPlayer();

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
    
    public class with_negative_vertical_inputs
    {
        [UnityTest]
        public IEnumerator moves_backward()
        {
            //ARRANGE
            yield return helpers.LoadMovementTestsScene();
            var player = helpers.GetPlayer();

            //ACT
            player.playerInput.Vertical.Returns(-1f);
            player.playerInput.Horizontal.Returns(-1f);
            
            float startingZPosition = player.transform.position.z;
            float startingXPosition = player.transform.position.x;

            yield return new WaitForSeconds(5f);

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

            player.playerInput.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.3f);

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

            player.playerInput.MouseX.Returns(1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.3f);

            float turnAmount = helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Greater(turnAmount, 0);
        }
    }
}