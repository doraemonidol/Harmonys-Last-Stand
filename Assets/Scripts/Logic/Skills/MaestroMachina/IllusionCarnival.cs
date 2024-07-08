using System;
using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Facade;
using Logic.Helper;
using Logic.Villains;
using Logic.Weapons;

namespace Logic.Skills.MaestroMachina
{
    public class IllusionCarnival : AcSkill
    {
        private Thread _postGameThread;
        
        public IllusionCarnival(Weapon owner) : base(owner)
        {
        }

        public IllusionCarnival(Weapon owner, long coolDownTime) : base(owner, coolDownTime)
        {
        }

        private void Update()
        {
            var thread = new Thread(() =>
            {
                // Notify the user
                var user = (Villain) User;
                
                var randomNumber = new Random().Next(0, 5);

                var arr = new List<int> { 1, 2, 3, 4, 5 };

                for (var i = 0; i < 5; i++)
                {

                    Notify();

                    Thread.Sleep(2000);

                    Shuffle(arr);
                }

                _postGameThread = new Thread(() =>
                {
                    Thread.Sleep(3000); 
                
                    user.NotifySubscribers(new EventUpdateVisitor
                    {
                        ["ev"] =
                        {
                            ["type"] = "End_of_time_Skill_3",
                        }
                    });
                });
                
                _postGameThread.Start();

                return;

                void Shuffle(List<int> _arr)
                {
                    // Shuffle the array
                    for (var i = 0; i < _arr.Count; i++)
                    {
                        var temp = _arr[i];
                        var randomIndex = new Random().Next(0, _arr.Count);
                        _arr[i] = _arr[randomIndex];
                        _arr[randomIndex] = temp;
                    }
                }

                void Notify()
                {
                    var message = new EventUpdateVisitor
                    {
                        ["ev"] =
                        {
                            ["type"] = "skill2_moving",
                        },
                        ["data"] = {
                            ["main"] = arr[randomNumber],
                            ["virtual1"] = (randomNumber == 0) ? arr[1] : arr[0],
                            ["virtual2"] = (randomNumber == 1) ? arr[2] : arr[1],
                            ["virtual3"] = (randomNumber == 2) ? arr[3] : arr[2],
                            ["virtual4"] = (randomNumber == 3) ? arr[4] : arr[3],
                        }
                    };
                    user.NotifySubscribers(message);
                }
                
            });
            thread.Start();
        }

        public override void Activate()
        {
            if (this.Locked)
            {
                throw new Exception("Exception thrown when trying to activate a locked skill");
            }
            if (this.NextTimeToAvailable > Time.WhatIsIt())
            {
                throw new Exception("Exception thrown when trying to activate a skill that is not available");
            }
            this.NextTimeToAvailable = Time.WhatIsIt() + CoolDownTime;
            this.Lock();
            Update();
        }

        public override void Cancel()
        {
            if (_postGameThread.IsAlive)
                _postGameThread.Abort();
        }

        public override void Affect(ICharacter attacker, ICharacter target, EventDto context)
        {
            var cxt = (string)context["cxt"];
            if (cxt != "pre" && cxt != "post") throw new Exception("Invalid context");
            switch (cxt)
            {
                case "pre":
                    target.ReceiveEffect(EffectHandle.Rooted, new EventDto
                    {
                        ["timeout"] = 5,
                    });
                    break;
                case "post":
                    target.ReceiveEffect(EffectHandle.GetHit, new EventDto
                    {
                        [EffectHandle.HpReduce] = 30,
                    });
                    target.ReceiveEffect(EffectHandle.Stunt, new EventDto
                    {
                        ["timeout"] = 2,
                    });
                    break;
            }
        }
    }
}