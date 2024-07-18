using UnityEngine;

namespace Common
{
    public class EnemyActionType
    {
        public static readonly int Move = Animator.StringToHash("Move");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Die = Animator.StringToHash("Die");
        public static readonly int CastSpell1 = Animator.StringToHash("CastSpell1");
        public static readonly int CastSpell2 = Animator.StringToHash("CastSpell2");
        public static readonly int CastSpell3 = Animator.StringToHash("CastSpell3");
        public static readonly int CastSpell4 = Animator.StringToHash("CastSpell4");
        public static readonly int CastSpell5 = Animator.StringToHash("CastSpell5");
    }
}