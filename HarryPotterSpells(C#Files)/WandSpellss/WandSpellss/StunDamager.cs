using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace WandSpellss
{
    public class StunDamager : MonoBehaviour
    {
        Item item;
        public void Start()
        {
            item = GetComponent<Item>();
            


        }


        public void OnCollisionEnter(Collision c) {

            c.gameObject.GetComponentInParent<Creature>().ragdoll.SetState(Ragdoll.State.Destabilized);
            c.gameObject.GetComponentInParent<Creature>().TryElectrocute(1, 3, true, false, Catalog.GetData<EffectData>("ImbueLightningRagdoll", true));


        }
    }
}


