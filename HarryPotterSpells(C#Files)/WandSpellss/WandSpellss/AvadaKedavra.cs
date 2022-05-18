using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace WandSpellss
{
    public class AvadaKedavra : MonoBehaviour
    {
        Item item;
        internal ItemData avadaLightning;
        internal AudioSource source;
        Item lightningItem;
        public Creature hitCreatures;
        public void Start()
        {
            item = GetComponent<Item>();
            source = GetComponent<AudioSource>();
            source.clip = null;
        }


        public void OnCollisionEnter(Collision c)
        {
            c.gameObject.GetComponentInParent<Creature>().TryElectrocute(0, 2, true, true, Catalog.GetData<EffectData>("AvadaKedavraLightning", true));
            c.gameObject.GetComponentInParent<Creature>().Kill();
            


        }


        


    }

}
