using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace WandSpellss
{
    public class Levicorpus : MonoBehaviour
    {
        Item item;
        Item npcItem;
        public void Start()
        {
            item = GetComponent<Item>();




        }


        public void OnCollisionEnter(Collision c)
        {

            foreach (Rigidbody rigidbody in c.gameObject.GetComponentInParent<Creature>().ragdoll.parts.Select(part => part.rb))
            {
                
                UnityEngine.Vector3 vector3;
                vector3.z = 10f;
                Vector3 creature = c.gameObject.GetComponentInParent<Creature>().transform.position;
                c.gameObject.GetComponentInParent<Creature>().ragdoll.SetState(Ragdoll.State.Destabilized);
                rigidbody.useGravity = false;
                rigidbody.AddForce(c.gameObject.GetComponentInParent<Creature>().transform.up * 15f, ForceMode.Impulse);
                //c.gameObject.GetComponentInParent<Creature>().footLeft.transform.position
                
                

                




            }






        }
    }

}




