using UnityEngine.Serialization;

namespace Presentation
{
    public abstract class PSkill : PresentationObject
    {
        public int ID { get; set; }
        public bool isReady = true;
    }
}