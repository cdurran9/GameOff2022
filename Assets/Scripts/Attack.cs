using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float punchDuration;
        [SerializeField] private float punchCooldown;
        private Animate _animate;
        private bool _canPunch = true;

        private void Start()
        {
            _animate = GetComponent<Animate>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1") && _canPunch)
            {
                Punch();
            }
        }

        public void Punch()
        {
            _canPunch = false;
            SetAnimations("Punch");
            StartCoroutine(DoPunch());
            StartCoroutine(DoPunchAnimation());
        }
        
        private void SetAnimations(string sprite)
        {
            if (object.ReferenceEquals(_animate, null)) return;
        
            _animate.SetSprite(sprite);
        }

        IEnumerator DoPunch()
        {
            yield return new WaitForSeconds(punchCooldown);
            _canPunch = true;
        }
        
        IEnumerator DoPunchAnimation()
        {
            yield return new WaitForSeconds(punchDuration);
            SetAnimations("Idle");
        }
    }
}