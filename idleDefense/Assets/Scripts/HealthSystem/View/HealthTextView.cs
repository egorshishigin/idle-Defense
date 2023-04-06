using TMPro;

using UnityEngine;

namespace HealthSystem.View
{
    public class HealthTextView : MonoBehaviour
    {
        [SerializeField] private Health _health;

        [SerializeField] private TMP_Text _healthText;

        private void OnEnable()
        {
            _health.HealthInitialized += HealthTextUpdate;

            _health.Damaged += HealthTextUpdate;
        }

        private void OnDisable()
        {
            _health.HealthInitialized -= HealthTextUpdate;

            _health.Damaged -= HealthTextUpdate;
        }

        private void HealthTextUpdate(float value)
        {
            _healthText.text = value.ToString();
        }
    }
}