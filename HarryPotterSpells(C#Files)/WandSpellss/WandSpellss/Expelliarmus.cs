using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace WandSpellss
{
    public class Expelliarmus : MonoBehaviour
    {
        Item item;
        Item npcItem;
        internal AudioSource source;
        public void Start()
        {
            item = GetComponent<Item>();
            source = GetComponentInChildren<AudioSource>();
            source.clip = null;


        }


        public void OnCollisionEnter(Collision c)
        {

            c.gameObject.GetComponentInParent<Creature>().handRight.UnGrab(false);



            c.gameObject.GetComponentInParent<Creature>().handLeft.UnGrab(false);


            foreach (Rigidbody rigidbody in c.gameObject.GetComponentInParent<Creature>().ragdoll.parts.Select(part => part.rb))
            {
                    
                    c.gameObject.GetComponentInParent<Creature>().ragdoll.SetState(Ragdoll.State.Destabilized);
                    rigidbody.AddForce(item.flyDirRef.transform.forward * (80f), ForceMode.Impulse);
                

                


            }






        }
    }

}
