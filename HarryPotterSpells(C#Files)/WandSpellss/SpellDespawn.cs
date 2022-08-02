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
            Debug.Log("Before avada check");
            Debug.Log(c.gameObject.name);
            if (item.GetComponent<AvadaKedavra>() != null) {
                Debug.Log("After avada check");
                Debug.Log(c.gameObject);
                if (c.gameObject.name == "Quad 1(Clone)")
                {
                    Debug.Log("Colliders Hit this with avadaKedavra: " + c.gameObject.name);
                    item.IgnoreObjectCollision(c.gameObject.GetComponent<Item>());

                }

                else item.Despawn();
            }

            else item.Despawn();
        }
    }
}
