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
        GameObject floater1;
        GameObject floater2;
        SpringJoint joint;
        internal Creature creature;
        internal Item spawnerWeapon;
        public void Start()
        {
            item = GetComponent<Item>();




        }


        public void OnCollisionEnter(Collision c)
        {

            if (c.gameObject.GetComponentInParent<Creature>() != null)
            {


                if (spawnerWeapon.GetComponent<VoiceWeaponComponent>() != null) {

                    spawnerWeapon.GetComponent<VoiceWeaponComponent>().hitByLevicorpus.Add(c.gameObject.GetComponentInParent<Creature>());
                
                }

                floater1 = new GameObject();
                floater1.AddComponent<Rigidbody>();
                floater1.GetComponent<Rigidbody>().useGravity = false;

                

                floater2 = new GameObject();
                floater2.AddComponent<Rigidbody>();
                floater2.GetComponent<Rigidbody>().useGravity = false;
                




                creature = c.gameObject.GetComponentInParent<Creature>();
                creature.ragdoll.SetState(Ragdoll.State.Destabilized);
                creature.footLeft.gameObject.AddComponent<SpringJoint>();
                creature.footRight.gameObject.AddComponent<SpringJoint>();


                Debug.Log("Floater 1 pre: " + floater1.transform.position);
                Debug.Log("Floater 2 pre: " + floater2.transform.position);
                floater1.transform.position = new Vector3(creature.ragdoll.headPart.transform.position.x, creature.ragdoll.headPart.transform.position.y + 2f, creature.ragdoll.headPart.transform.position.z);
                floater2.transform.position = new Vector3(creature.ragdoll.headPart.transform.position.x, creature.ragdoll.headPart.transform.position.y + 2f, creature.ragdoll.headPart.transform.position.z);

                

                Debug.Log("Floater 1 post: " + floater1.transform.position);
                Debug.Log("Floater 2 post: " + floater2.transform.position);

                Debug.Log("footLeft transform: " + creature.footLeft.transform.position);
                Debug.Log("footLeft transform joint: " + creature.footLeft.gameObject.GetComponent<SpringJoint>().transform.position);

                creature.footLeft.gameObject.GetComponent<SpringJoint>().connectedBody = floater1.GetComponent<Rigidbody>();
                creature.footLeft.gameObject.GetComponent<SpringJoint>().autoConfigureConnectedAnchor = false;
                creature.footLeft.gameObject.GetComponent<SpringJoint>().connectedAnchor = new Vector3(0,0,0);
                Debug.Log("Creature connected Anchor: " + creature.footLeft.gameObject.GetComponent<SpringJoint>().connectedAnchor);

                creature.footLeft.gameObject.GetComponent<SpringJoint>().spring = 3000f;
                creature.footLeft.gameObject.GetComponent<SpringJoint>().damper = 100f;


                creature.footRight.gameObject.GetComponent<SpringJoint>().connectedBody = floater2.GetComponent<Rigidbody>();
                creature.footRight.gameObject.GetComponent<SpringJoint>().autoConfigureConnectedAnchor = false;
                creature.footRight.gameObject.GetComponent<SpringJoint>().connectedAnchor = new Vector3(0, 0, 0);
                Debug.Log("Creature connected Anchor: " + creature.footRight.gameObject.GetComponent<SpringJoint>().connectedAnchor);
                creature.footRight.gameObject.GetComponent<SpringJoint>().spring = 3000f;
                creature.footRight.gameObject.GetComponent<SpringJoint>().damper = 100f;


                floater1.AddComponent<FixedJoint>();
                floater2.AddComponent<FixedJoint>();








            }


            

        }
    }

}




