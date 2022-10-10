using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace PlanetaryDevastation
{
    class CastPlanetaryDevastation : SpellCastProjectile
    {
        Item item;
        public override void Fire(bool active)
        {
            base.Fire(active);

            if (!active)
            {

                return;
            }

            Catalog.GetData<ItemData>("PlanetaryDevastationObject").SpawnAsync(new System.Action<Item>(SpawnDevastation));

            


        }
        public void SpawnDevastation(Item spawnedItem)
        {

            if (item != null) {

                item.Despawn();

            }
            if (spawnedItem.gameObject.GetComponent<Planet>() == null) {

                spawnedItem.gameObject.AddComponent<Planet>();
            
            }
            item = spawnedItem;
            spawnedItem.transform.position = new Vector3(Player.local.head.transform.position.x, Player.local.head.transform.position.y + 100f, Player.local.head.transform.position.z);
            spawnedItem.rb.useGravity = false;
            spawnedItem.rb.drag = 0.0f;
            spawnedItem.rb.mass = 1000f;


            


        }

        

    }
}
