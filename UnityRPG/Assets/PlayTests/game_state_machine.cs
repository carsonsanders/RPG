using a_player;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class game_state_machine
    {
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
    }
}