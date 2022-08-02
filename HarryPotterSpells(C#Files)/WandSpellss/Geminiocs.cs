using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace WandSpellss
{
    public class Geminio : MonoBehaviour
    {
        Item item;
        internal GameObject parentLocal;
        internal bool cantEvanesco;
        Item duplicate;
        System.Random random;
        public void Start()
        {
            item = GetComponent<Item>();

            random = new System.Random();


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
                duplicate = Instantiate(parent.gameObject.GetComponentInParent<Item>());
                parentLocal = parent.gameObject;
                if (parentLocal.GetComponent<Item>() != null)
                {
                    double range = (double)-0.1 - (double)(-0.5);
                    double sample = random.NextDouble();
                    double scaled = (sample * range);
                    duplicate.transform.position = new Vector3(parent.gameObject.transform.position.x + (float)scaled, parent.gameObject.transform.position.y + (float)scaled, parent.gameObject.transform.position.z);

                }





            }


        }


    }

}