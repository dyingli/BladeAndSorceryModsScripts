using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace WandSpellss
{
    class EngorgioPerItem : MonoBehaviour
    {

        bool cantEngorgio;
        Item item;
        private float elapsedTime;
        private Vector3 engorgioMaxSize;

        void Start() {

            item = GetComponent<Item>();
            cantEngorgio = false;
            engorgioMaxSize = new Vector3(2,2,2);
        
        }


        void Update() {

            if (cantEngorgio == false)
            {

                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / 0.2f;


                item.transform.localScale = Vector3.Lerp(item.transform.localScale, engorgioMaxSize, Mathf.SmoothStep(0, 1, percentageComplete));



                elapsedTime = 0f;

                if (item.transform.localScale == engorgioMaxSize)
                {


                    cantEngorgio = true;

                }





            }


        }
    }
}
