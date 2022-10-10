using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace WandSpellss
{
    class EvanescoPerItem : MonoBehaviour
    {

        bool cantEvanesco;

        
        Item item;
        private float elapsedTime;
        float dissolveVal;

        void Start()
        {
            if (GetComponent<Item>() != null)
            {
                item = GetComponent<Item>();
            }
            else if (GetComponentInParent<Item>() != null) {

                item = GetComponentInParent<Item>();

            }
            cantEvanesco = false;
            dissolveVal = 0;



        }


        void Update()
        {

            if (cantEvanesco == false)
            {
                if (item.gameObject.GetComponent<Renderer>() != null)
                {

                    if (dissolveVal < 1)
                    {
                        //dissolveVal += Time.deltaTime / 3f;

                        //Debug.Log(dissolveVal);
                        dissolveVal += 0.01f;
                        foreach (Material mat in item.gameObject.GetComponent<Renderer>().materials)
                        {
                            mat.SetFloat("_dissolve", dissolveVal);
                        }


                    }


                    else if (dissolveVal >= 1f)
                    {
                        dissolveVal = 0;
                        cantEvanesco = true;
                        Destroy(item.gameObject);


                    }
                }

                else if (item.gameObject.GetComponentInChildren<Renderer>() != null)
                {

                    if (dissolveVal < 1)
                    {
                        Debug.Log(dissolveVal);
                        dissolveVal += 0.01f;
                        foreach (Material mat in item.gameObject.GetComponentInChildren<Renderer>().materials)
                        {
                            mat.SetFloat("_dissolve", dissolveVal);
                        }

                    }


                    else if (dissolveVal >= 1f)
                    {


                        dissolveVal = 0;
                        cantEvanesco = true;
                        Destroy(item.gameObject);


                    }
                }

            }
        }
    }
}
