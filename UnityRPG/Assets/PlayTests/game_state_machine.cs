using a_player;
using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class game_state_machine
    {
        [TearDown]
        public void teardown()
        {
            GameObject.Destroy(Object.FindObjectOfType<GameStateMachine>());
        }
        
        [UnityTest]
        public IEnumerator switched_to_loading_when_level_to_load_selected()
        {
            yield return helpers.LoadMenuScene();
            var stateMachine = GameObject.FindObjectOfType<GameStateMachine>();
            
            Assert.AreEqual(typeof(Menu), stateMachine.CurrentStateType);
            PlayButton.LevelToLoad = "Level1";
            yield return null;
            
            Assert.AreEqual(typeof(LoadLevel), stateMachine.CurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator switched_to_play_when_level_to_load_completed()
        {
            yield return helpers.LoadMenuScene();
            var stateMachine = GameObject.FindObjectOfType<GameStateMachine>();
            
            Assert.AreEqual(typeof(Menu), stateMachine.CurrentStateType);
            PlayButton.LevelToLoad = "Level1";
            yield return null;
            
            Assert.AreEqual(typeof(LoadLevel), stateMachine.CurrentStateType);

            yield return new WaitUntil(()=>stateMachine.CurrentStateType == typeof(Play));
            
            Assert.AreEqual(typeof(Play), stateMachine.CurrentStateType);
        }

        [UnityTest]
        public IEnumerator only_allows_one_instance_to_exist()
        {
            var firstGameStateMachine = new GameObject("First State Machine").AddComponent<GameStateMachine>();
            var secondGameStateMachine = new GameObject("Second State Machine").AddComponent<GameStateMachine>();
            yield return null;

            Assert.IsTrue(secondGameStateMachine == null);
            Assert.IsNotNull(firstGameStateMachine);
        }
    }
}