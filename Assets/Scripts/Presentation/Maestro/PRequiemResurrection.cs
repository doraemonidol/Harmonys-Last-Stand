using System.Collections;
using Common;
using MockUp;
using UnityEngine;

namespace Presentation.Maestro
{
    public class PRequiemResurrection : MaestroSkill
    {
        public override void Start()
        {
            base.Start();
        }
        
        public override IEnumerator StartCasting()
        {
            state = SkillState.Casting;
            animator.SetTrigger(EnemyActionType.CastSpell3);
            yield return new WaitForSeconds(1.2f);
            StartCoroutine(StartPrecastVFX());
        }

        public override IEnumerator StartHitting()
        {
            throw new System.NotImplementedException();
        }
    }
}