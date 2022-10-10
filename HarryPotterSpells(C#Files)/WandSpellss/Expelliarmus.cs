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

        internal float power;
        public void Start()
        {
            item = GetComponent<Item>();


        }


        public void OnCollisionEnter(Collision c)
        {

            if (c.gameObject.GetComponentInParent<Creature>() != null)
            {
                c.gameObject.GetComponentInParent<Creature>().handRight.UnGrab(false);



                c.gameObject.GetComponentInParent<Creature>().handLeft.UnGrab(false);


                foreach (Rigidbody rigidbody in c.gameObject.GetComponentInParent<Creature>().ragdoll.parts.Select(part => part.rb))
                {

                    Debug.Log("Rigidbody name: " + rigidbody.name);
                    c.gameObject.GetComponentInParent<Creature>().ragdoll.SetState(Ragdoll.State.Destabilized);
                    rigidbody.AddForce(item.flyDirRef.transform.forward * (power), ForceMode.Impulse);





                }


            }

            else if (c.gameObject.GetComponentInParent<Item>() != null) {

                Item tempItem = c.gameObject.GetComponentInParent<Item>();


                tempItem.mainHandler.otherHand.otherHand.UnGrab(false);
                tempItem.mainHandler.otherHand.creature.ragdoll.SetState(Ragdoll.State.Destabilized);
                           

            
            }






        }
    }

}
