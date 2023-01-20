using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.AI.Effect
{
    public class Hit : MonoBehaviour, AIEffect
    {
        private WaitForSeconds wait = new WaitForSeconds(1f);
        private delegate IEnumerator destroy();
        destroy _destroy = null;

        private void Awake()
        {
            _destroy = DestroyThis;
            StartCoroutine(_destroy());
        }

        IEnumerator DestroyThis()
        {
            yield return wait;

            Destroy(this.gameObject);
        }
    }

    
}

