using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace WandSpellss
{
    public class Evanesco : MonoBehaviour
    {
        Item item;
        Item npcItem;
        internal Vector3 startPoint;
        internal Vector3 endPoint;
        internal GameObject parentLocal;
        internal Vector3 ogScale;
        internal bool cantEvanesco;
        internal Material evanescoDissolve;
        List<Material> myMaterials;
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

                Debug.Log("Did hit.");
                Debug.Log(hit.collider.gameObject.transform.parent.name);

                parent = hit.collider.gameObject.transform.parent;
                parentLocal = parent.gameObject;
                if (parentLocal.GetComponent<Item>() != null)
                {
                    if (parentLocal.gameObject.GetComponent<Renderer>() != null)
                    {
                        myMaterials = parentLocal.GetComponent<Renderer>().materials.ToList();
                        Material[] matDefGood = new Material[myMaterials.Count];

                        for (int i = 0; i < myMaterials.Count; i++)
                        {
                            //Debug.Log("Im in the " +i+ " iteration of the loop");
                            evanescoDissolve.SetTexture("_Albedo", myMaterials[i].GetTexture("_BaseMap"));
                            evanescoDissolve.SetColor("_color", myMaterials[i].GetColor("_BaseColor"));
                            //Debug.Log(evanescoDissolve.GetTexture("_Albedo"));
                            evanescoDissolve.SetTexture("_Normal", myMaterials[i].GetTexture("_BumpMap"));
                            //Debug.Log(evanescoDissolve.GetTexture("_Normal"));
                            evanescoDissolve.SetTexture("_Metallic", myMaterials[i].GetTexture("_MetallicGlossMap"));
                            //Debug.Log(evanescoDissolve.GetTexture("_Metallic"));

                            matDefGood[i] = evanescoDissolve;
                        }
                        parentLocal.GetComponent<Renderer>().materials = matDefGood;





                        cantEvanesco = false;
                        parentLocal.AddComponent<EvanescoPerItem>();

                    }

                    else if (parentLocal.gameObject.GetComponentInParent<Renderer>() != null)
                    {
                        myMaterials = parentLocal.GetComponentInChildren<Renderer>().materials.ToList();
                        Material[] matDefGood = new Material[myMaterials.Count];

                        for (int i = 0; i < myMaterials.Count; i++)
                        {
                            //Debug.Log("Im in the " + i + " iteration of the loop");
                            evanescoDissolve.SetTexture("_Albedo", myMaterials[i].GetTexture("_BaseMap"));
                            evanescoDissolve.SetColor("_color", myMaterials[i].GetColor("_BaseColor"));
                            //Debug.Log(evanescoDissolve.GetTexture("_Albedo"));
                            evanescoDissolve.SetTexture("_Normal", myMaterials[i].GetTexture("_BumpMap"));
                            //Debug.Log(evanescoDissolve.GetTexture("_Normal"));
                            evanescoDissolve.SetTexture("_Metallic", myMaterials[i].GetTexture("_MetallicGlossMap"));
                            //Debug.Log(evanescoDissolve.GetTexture("_Metallic"));

                            matDefGood[i] = evanescoDissolve;
                        }
                        parentLocal.GetComponentInParent<Renderer>().materials = matDefGood;
                        cantEvanesco = false;
                        parentLocal.AddComponent<EvanescoPerItem>();
                    }
                    else if (parentLocal.gameObject.GetComponentInChildren<Renderer>() != null)
                    {
                        myMaterials = parentLocal.GetComponentInChildren<Renderer>().materials.ToList();
                        Material[] matDefGood = new Material[myMaterials.Count];

                        for (int i = 0; i < myMaterials.Count; i++)
                        {
                            //Debug.Log("Im in the " + i + " iteration of the loop");
                            evanescoDissolve.SetTexture("_Albedo", myMaterials[i].GetTexture("_BaseMap"));
                            evanescoDissolve.SetColor("_color", myMaterials[i].GetColor("_BaseColor"));
                            //Debug.Log(evanescoDissolve.GetTexture("_Albedo"));
                            evanescoDissolve.SetTexture("_Normal", myMaterials[i].GetTexture("_BumpMap"));
                            //Debug.Log(evanescoDissolve.GetTexture("_Normal"));
                            evanescoDissolve.SetTexture("_Metallic", myMaterials[i].GetTexture("_MetallicGlossMap"));
                            //Debug.Log(evanescoDissolve.GetTexture("_Metallic"));

                            matDefGood[i] = evanescoDissolve;
                        }
                        parentLocal.GetComponentInChildren<Renderer>().materials = matDefGood;
                        cantEvanesco = false;
                        parentLocal.AddComponent<EvanescoPerItem>();
                    }
                    
                }

                else
                {
                    
                    cantEvanesco = true;

                }




            }


        }


    }

}