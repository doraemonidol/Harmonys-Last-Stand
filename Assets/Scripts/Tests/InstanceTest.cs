using System;
using System.Collections;
using System.Text;
using System.Threading;
using DTO;
using Logic;
using Logic.Facade;
using Logic.MainCharacters;
using Logic.Skills;
using Logic.Villains;
using Logic.Villains.Maestro;
using MockUp;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Presentation;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;
using Time = Logic.Helper.Time;

namespace Tests
{
    public class InstanceTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestInstantiateCharacter()
        {
            var aurelia = new GameObject().AddComponent<AureliaMockUp>();
            aurelia.Start();
            TestContext.WriteLine(aurelia.SelfHandle);
            var logicHandle = aurelia.LogicHandle;
            var logicObj = (LogicObject) LogicLayer.GetInstance()._objects[logicHandle];
            // Assert.AreEqual(aurelia.SelfHandle, logicObj.PresentationHandle);
        }
        
        [Test]
        public void TestInstantiateWeapon()
        {
            // Use the Assert class to test conditions
            var weapon = new GameObject().AddComponent<WeaponMockUp>();
            weapon.Start();
            var logicHandle = weapon.LogicHandle;
            var logicObj = (LogicObject) LogicLayer.GetInstance()._objects[logicHandle];
            if (weapon.SelfHandle != logicObj.PresentationHandle)
            {
                Assert.IsTrue(false);
            }
            TestContext.WriteLine("Passed Weapon Instantiation");
            var i = 1;
            foreach (var skill in weapon.GetNormalSkills())
            {
                var skillHandle = skill.LogicHandle;
                var skillObj = (LogicObject) LogicLayer.GetInstance()._objects[skillHandle];
                if (skill.SelfHandle != skillObj.PresentationHandle)
                {
                    Assert.IsTrue(false);
                }
                TestContext.WriteLine(new StringBuilder().Append("Passed ").Append(i));
                i++;
            }
            Assert.IsTrue(true);
        }

        private void TestCharacterChooseWeapon(AureliaMockUp aurelia, PWeapon weapon)
        {
            var eventd = new EventDto
            {
                Event = "TAKE_WP",
                ["char"] = aurelia.LogicHandle,
                ["num"] = 1,
                ["wp1"] = weapon.LogicHandle
            };
            LogicLayer.GetInstance().Observe(eventd);
            var logicObj = (Aurelia) LogicLayer.GetInstance()._objects[aurelia.LogicHandle];
            Assert.AreEqual(1, logicObj._weapons.Count);
            TestContext.WriteLine(logicObj._weapons[0].GetType());
        }

        [Test]
        public void TestCharacterActivateSkill()
        {
            var aurelia = new GameObject().AddComponent<AureliaMockUp>();
            aurelia.Start();
            var weapon = new GameObject().AddComponent<WeaponMockUp>();
            weapon.Start();
            TestCharacterChooseWeapon(aurelia,weapon);
            LogicLayer.GetInstance().Observe(new EventDto
            {
                Event = "CAST",
                ["skill"] = weapon.GetNormalSkills()[0].LogicHandle,
            });
            Thread.Sleep(10000);
            try
            {
                LogicLayer.GetInstance().Observe(new EventDto
                {
                    Event = "CAST",
                    ["skill"] = weapon.GetNormalSkills()[0].LogicHandle,
                });
                Assert.IsTrue(false);
            }
            catch (System.Exception e)
            {
                TestContext.WriteLine(e.StackTrace);
                TestContext.WriteLine("Current time: " + Time.WhatIsIt());
                TestContext.WriteLine("Next time to activate: " + ((AcSkill)LogicLayer.GetInstance()._objects[weapon.GetNormalSkills()[0].LogicHandle]).NextTimeToAvailable);
                Assert.IsTrue(((AcSkill)LogicLayer.GetInstance()._objects[weapon.GetNormalSkills()[0].LogicHandle]).IsAvailable());
            }
        }

        [Test]
        public void TestCharacterTakeEffectHallucination()
        {
            var aurelia = new GameObject().AddComponent<AureliaMockUp>();
            aurelia.Start();
            var weapon = new GameObject().AddComponent<WeaponMockUp>();
            weapon.Start();
            var villain = new GameObject().AddComponent<MaestroMockUp>();
            villain.Start();
            LogicLayer.GetInstance().Observe(new EventDto
            {
                Event = "GET_ATTACKED",
                ["attacker"] = aurelia.LogicHandle,
                ["target"] = villain.LogicHandle,
                ["context"] = null,
                ["skill"] = weapon.GetNormalSkills()[0].LogicHandle,
            });
        }
        
        [Test]
        public void TestCharacterTakeEffectBleeding()
        {
            
        }
        
        [Test]
        public void TestCharacterTakeEffectJinxed()
        {
            
        }

        [Test]
        public void TestCharacterTakeEffectNearsight()
        {
            
        }

        [Test]
        public void TestCharacterTakeEffectCharm()
        {
            
        }
        
        [Test]
        public void TestCharacterTakeEffectClone()
        {
            
        }
        
        [Test]
        public void TestCharacterTakeEffectShielded()
        {
            
        }
        
        [Test]
        public void TestCharacterTakeEffectStun()
        {
            
        }
        
        [Test]
        public void TestCharacterTakeEffectSilenced()
        {
            
        }
        
        [Test]
        public void TestCharacterTakeEffectRooted()
        {
            
        }
        
        [Test]
        public void TestCharacterTakeEffectSleepy()
        {
            
        }

        [Test]
        public void TestCharacterDie()
        {
            
        }

        [Test]
        public void TestCharacterContextSwitch()
        {
            
        }

        [Test]
        public void TestVillainInstantiate()
        {
            try
            {
                var villain = new GameObject().AddComponent<MaestroMockUp>();
                villain.Start();
                var logicHandle = villain.LogicHandle; 
                var logicObj = (LogicObject)LogicLayer.GetInstance()._objects[logicHandle];
                Assert.AreEqual(villain.SelfHandle, logicObj.PresentationHandle);
            } catch (System.Exception e)
            {
                TestContext.WriteLine(e.StackTrace);
                Assert.IsTrue(false);
            }
        }
        
        [Test]
        public void TestVillainAutoCasting()
        {
            var villain = new GameObject().AddComponent<MaestroMockUp>();
            villain.Start();
            try
            {
                LogicLayer.GetInstance().Observe(new EventDto
                {
                    Event = "VILLAIN_CAST",
                    ["id"] = villain.LogicHandle,
                });
                Thread.Sleep(1000);
                LogicLayer.GetInstance().Observe(new EventDto
                {
                    Event = "VILLAIN_RESULT",
                    ["id"] = villain.LogicHandle,
                    ["result"] = false,
                });
                var villainLogic = (Villain)LogicLayer.GetInstance()._objects[villain.LogicHandle];
                Assert.AreEqual(typeof(MmSkillCasting), villainLogic.State.GetType());
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e.StackTrace);
                Assert.IsTrue(false);
            }
            Assert.IsTrue(true);
        }

        [Test]
        public void TestVillainDie()
        {
            
        }
    }
    
    
}
