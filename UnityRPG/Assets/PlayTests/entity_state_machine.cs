using System.Collections;
using a_player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class entity_state_machine
    {
        [UnityTest]
        public IEnumerator starts_in_idle_state()
        {
            yield return helpers.LoadEntityStateMachineTestsScene();
            var stateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            Assert.AreEqual(typeof(Idle), stateMachine.CurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator switches_to_chase_player_state_when_in_range()
        {
            yield return helpers.LoadEntityStateMachineTestsScene();
            var player = helpers.GetPlayer();
            
            var stateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            yield return null;
            
            Assert.AreEqual(typeof(Idle), stateMachine.CurrentStateType);

            player.transform.position = stateMachine.transform.position + new Vector3(4.9f, 0, 0);
            yield return null;

            Assert.AreEqual(typeof(ChasePlayer), stateMachine.CurrentStateType);


        }
    }
}