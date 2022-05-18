using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace WandSpellss
{
    class SpellDespawn : MonoBehaviour
    {
        Item item;
        public void Start() {


            this.item = GetComponent<Item>();
            
        }
        public void OnCollisionEnter(Collision c)
        {
            if (item.GetComponent<AvadaKedavra>() != null) {
                if (c.collider.gameObject.name == "Protego")
                {

                    item.IgnoreObjectCollision(c.collider.gameObject.GetComponent<Item>());

                }

                else item.Despawn();
            }

            else item.Despawn();
        }
    }
}
