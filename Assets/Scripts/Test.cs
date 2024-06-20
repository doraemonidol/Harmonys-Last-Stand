
    using UnityEngine;

    public class Test : MonoBehaviour
    {
        private PlayerSkill _playerSkill;
        public Test()
        {
            Debug.Log("Constructor");
            _playerSkill = new PlayerNormalSkill();
        }
        
        // Start is called before the first frame update
        public void Start()
        {
            Debug.Log("Start");
        }
    }