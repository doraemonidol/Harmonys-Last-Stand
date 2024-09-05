using UnityEngine;

namespace Runtime
{
    public class MonoBehaviorInstance <T> : AbstractMonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                    Debug.Log($"Can't find {typeof(T)}");
                return instance;
            }
        }

        protected void Awake()
        {
            if(instance == null || instance.gameObject == null)
            {
                instance = this as T;
            }
            if(instance != null && instance.gameObject != this.gameObject)
            {
                Destroy(this.gameObject);
                return;
            }
            ChildAwake();
        }
        protected virtual void ChildAwake()
        {

        }
    }
}

