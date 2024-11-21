using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace CodeBase.PostEffect
{
    public class VolumeEffectsEnabler : VolumeEffects
    {
        [SerializeField] private Volume _volume;

        private Vignette _vignette;

        private Coroutine _coroutine;

        private void Awake()
        {
            _volume.profile.TryGet(out _vignette);
        }

        public override void DamageEffect()
        {
            _vignette.active = true;
            _vignette.color = new ColorParameter(new Color(1, 0, 0, 0.8f));
            _coroutine = StartCoroutine(EffectAppearance());
        }

        private IEnumerator EffectAppearance()
        {
            while (_vignette.intensity.value < 0.5f)
            {
                _vignette.intensity.value += 0.05f;
                
                yield return new WaitForSeconds(0.1f);
            }
            
            yield return new WaitForSeconds(0.5f);
            
            while (_vignette.intensity.value > 0f)
            {
                _vignette.intensity.value -= 0.05f;
                
                yield return new WaitForSeconds(0.1f);
            }
            
            _vignette.active = false;
        }
    }
}