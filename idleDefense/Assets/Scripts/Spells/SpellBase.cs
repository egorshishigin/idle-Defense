using System;
using System.Collections;

using UnityEngine;

namespace Spells
{
    public abstract class SpellBase : MonoBehaviour
    {
        [SerializeField] private float _cooldown;

        [SerializeField] private bool _used;

        public event Action SpellUsed = delegate { };

        public event Action<float> SpellCooldown = delegate { };

        public event Action Refreshed = delegate { };

        [ContextMenu("Cast spell")]
        public void CastSpell()
        {
            if (_used) return;

            StartCoroutine(UseSpell());
        }

        protected abstract void Spell();

        private IEnumerator UseSpell()
        {
            SpellUsed.Invoke();

            _used = true;

            Spell();

            float time = _cooldown;

            while (time > 0)
            {
                time -= Time.deltaTime;

                SpellCooldown.Invoke(time);

                yield return null;
            }

            _used = false;

            Refreshed.Invoke();
        }
    }
}