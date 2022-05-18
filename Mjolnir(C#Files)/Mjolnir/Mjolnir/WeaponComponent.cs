using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace Mjolnir { 
    public class WeaponComponent : MonoBehaviour
    {
        Item item;
        EffectData lightning;
        EffectInstance effectInstance;
        void Start() {

            lightning = Catalog.GetData<EffectData>("MjolnirLightning");
            item = GetComponent<Item>();

            item.OnHeldActionEvent += Item_OnHeldActionEvent;
        
        }

        private void Item_OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
            if(action == Interactable.Action.UseStart) {

                effectInstance =  lightning.Spawn(item.transform.parent);
                effectInstance.Play();
            
            }
        }

        
    }
}
