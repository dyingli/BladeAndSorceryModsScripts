using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace WandSpellss
{
    public class Protego : MonoBehaviour
    {
        Item item;
        Item npcItem;
        internal AudioSource source;
        public void Start()
        {
            item = GetComponent<Item>();
            source = GetComponent<AudioSource>();



        }

        public void OnParticleCollision(Collision c)
        {

            foreach (Rigidbody rigidbody in c.gameObject.GetComponentInParent<Creature>().ragdoll.parts.Select(part => part.rb))
            {

                c.gameObject.GetComponentInParent<Creature>().ragdoll.SetState(Ragdoll.State.Destabilized);
                rigidbody.AddForce(item.flyDirRef.transform.forward * (40f), ForceMode.Impulse);




            }


        }

    }

}