using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace WandSpellss
{
    class WingardiumLeviosaJoint : MonoBehaviour
    {

        Item item;
        internal Item wand;
        Item npcItem;
        Creature creature;
        internal Item hitObjectItem;
        internal bool objectIsHovering;
        internal GameObject parentLocal;
        internal Vector3 targetPos;
        HingeJoint joint;
        public void Start()
        {
            item = GetComponent<Item>();


        }

        internal void CastRay()
        {

            RaycastHit hit;
            Transform parent;



            if (Physics.Raycast(item.flyDirRef.position, item.flyDirRef.forward, out hit))
            {

                parent = hit.collider.gameObject.transform.parent;
                parentLocal = hit.collider.gameObject;

                if (parentLocal.gameObject.GetComponent<Item>() is Item hitItem)
                {

                    wand.gameObject.AddComponent<HingeJoint>();
                    joint = wand.gameObject.GetComponent<HingeJoint>();
                    joint.connectedBody = hitItem.gameObject.GetComponent<Rigidbody>();
                    joint.anchor = wand.flyDirRef.transform.position;
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = new Vector3(0,0,0);

                }


                else if (parentLocal.gameObject.GetComponentInParent<Item>() is Item hitItem2) {

                    wand.gameObject.AddComponent<HingeJoint>();
                    joint = wand.gameObject.GetComponent<HingeJoint>();
                    joint.connectedBody = hitItem2.gameObject.GetComponent<Rigidbody>();
                    joint.anchor = wand.flyDirRef.transform.position;
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = new Vector3(0, 0, 0);

                }

            }


             

        }
    }
}
