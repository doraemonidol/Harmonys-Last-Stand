using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI
{
    public class HealthBar : MonoBehaviour
    {
        public float maxHealth;

        public float currentHealth;

        public Image mask;
        public TMP_Text healthText;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            GetCurrentFill();
        }

        void GetCurrentFill()
        {
            if (maxHealth == 0)
            {
                return;
            }
            float fillAmount = currentHealth / maxHealth;
            if (healthText != null)
                healthText.text = $"{currentHealth}/{maxHealth}";
            mask.fillAmount = fillAmount;
        }
    }
}
