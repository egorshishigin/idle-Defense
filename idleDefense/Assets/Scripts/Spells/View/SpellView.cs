using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Spells.View
{
    public class SpellView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private Image _image;

        [SerializeField] private TMP_Text _cooldownText;

        [SerializeField] private SpellBase _spell;

        private void OnEnable()
        {
            _button.onClick.AddListener(SpellButtonClick);

            _spell.SpellUsed += SpellUsedHandler;

            _spell.SpellCooldown += SpellCooldownAnimation;

            _spell.Refreshed += SpellRefreshedHandler;
        }


        private void OnDisable()
        {
            _button?.onClick.RemoveListener(SpellButtonClick);

            _spell.SpellUsed -= SpellUsedHandler;

            _spell.SpellCooldown -= SpellCooldownAnimation;

            _spell.Refreshed -= SpellRefreshedHandler;
        }

        private void SpellButtonClick()
        {
            _spell.CastSpell();
        }

        private void SpellUsedHandler()
        {
            _image.fillAmount = 0f;
        }

        private void SpellCooldownAnimation(float time)
        {
            _image.fillAmount += 0.25f / time * Time.deltaTime;

            _cooldownText.text = time.ToString("0.00");
        }

        private void SpellRefreshedHandler()
        {
            _image.fillAmount = 1f;

            _cooldownText.text = string.Empty;
        }
    }
}