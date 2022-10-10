using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace Kamui
{
    class CastKamui : SpellCastProjectile
    {

        Item item;
        ItemData kamuiData;
        private float spawned = 0f;
        Item currentKamui;

        public override void Fire(bool active)
        {
            base.Fire(active);
            if (!active)
            {

                return;
            }
            kamuiData = Catalog.GetData<ItemData>("KamuiObject");

            kamuiData.SpawnAsync(new System.Action<Item>(this.SpawnKamui));
            
            
        }




        public void SpawnKamui(Item spawnedItem)
        {

            if (currentKamui != null) {

                currentKamui.Despawn();
            
            }
            currentKamui = spawnedItem;
            spawnedItem.transform.position = Player.local.head.transform.position;
            spawnedItem.rb.useGravity = false;
            spawnedItem.rb.drag = 0.0f;
            
            //spawnedItem.RefreshCollision(true);

            spawnedItem.IgnoreRagdollCollision(Player.local.creature.ragdoll);
            spawnedItem.rb.AddForce(Player.local.head.transform.forward * (15f * spawnedItem.rb.mass), ForceMode.Impulse);


        }
    }
}
