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
                    parentLocal = parent.gameObject;
                if (parentLocal.gameObject.GetComponent<Item>() != null)
                {
                    startPoint = parent.gameObject.transform.position;
                    endPoint = Player.local.handLeft.transform.position;
                    cantAccio = false;
                }

                else {

                    cantAccio = true;
                
                }
                    
                    


                }
            

        }


    }

}