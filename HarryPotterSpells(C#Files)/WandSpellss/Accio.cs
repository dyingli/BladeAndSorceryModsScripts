using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace WandSpellss
{
    public class Accio : MonoBehaviour
    {
        Item item;
        internal Item wand;
        Item npcItem;
        internal Vector3 startPoint;
        internal Vector3 endPoint;
        internal GameObject parentLocal;
        internal bool cantAccio;
        public void Start()
        {
            item = GetComponent<Item>();


            

        }

        internal void CastRay() {

            
                RaycastHit hit;
                Transform parent;

            if (Physics.Raycast(item.flyDirRef.position, item.flyDirRef.forward, out hit))
            {

                Debug.Log("Did hit.");
                Debug.Log(hit.collider.gameObject.transform.parent.name);

                parent = hit.collider.gameObject.transform.parent;
                parentLocal = hit.collider.gameObject;
                if (parentLocal.gameObject.GetComponent<Item>() != null)
                {
                    startPoint = parentLocal.gameObject.transform.position;
                    endPoint = wand.mainHandler.otherHand.transform.position;
                    cantAccio = false;
                }

                else if (parentLocal.gameObject.GetComponentInParent<Item>() != null) {

                    startPoint = parentLocal.gameObject.transform.position;
                    endPoint = wand.mainHandler.otherHand.transform.position;
                    cantAccio = false;


                }

                else {

                    cantAccio = true;

                }
                    
                    


                }
            

        }


    }

}