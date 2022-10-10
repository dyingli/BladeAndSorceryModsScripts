using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace WandSpellss
{
    class Stupefy : MonoBehaviour
    {
        Item item;
        internal AudioSource source;
        public void Start() {
            item = GetComponent<Item>();
            item.gameObject.AddComponent<StunDamager>();
        }
    }
}
