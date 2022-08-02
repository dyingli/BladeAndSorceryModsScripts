using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;
using System.Collections;

namespace WandSpellss
{
    public class WingardiumLeviosa : MonoBehaviour
    {
        Item item;
        internal bool canLift;
        internal GameObject parentLocal;
        Vector3 radius;
        Vector3 direction;
        float distance;
        
        public void Start()
        {
            item = GetComponent<Item>();
            canLift = false;
            //item.OnHeldActionEvent += Item_OnHeldActionEvent;

        }

        private void Item_OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
            if (action == Interactable.Action.AlternateUseStart && canLift == true) {

                canLift = false;
            
            
            }
        }

        internal void CastRay()
        {

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

                    canLift = true;
                    radius = parentLocal.transform.position - item.flyDirRef.position;
                    distance = Math.Abs(Vector3.Distance(parentLocal.transform.position, item.flyDirRef.position));

                }
                else if (parentLocal.GetComponentInParent<Creature>() != null) {

                    canLift = true;
                    distance = Math.Abs(Vector3.Distance(parentLocal.transform.position, item.flyDirRef.position));

                }

            }


        }

        IEnumerator LevitateCoroutine() {

            yield return new WaitForSeconds(3);

            

        }


        void Update()
        {
            direction = item.flyDirRef.forward;

            
            if (parentLocal != null)
            {

                if (canLift == true)
                {

                   
                    parentLocal.transform.position = item.flyDirRef.position + (direction * distance);
                    


                }


            }



        }
    }

}
