using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;
using System.Collections;

namespace MassForcePush
{
    class CoroutineManager : MonoBehaviour
    {
        IEnumerator WaitForPush()
        {


            yield return new WaitForSeconds(1.5f);

        }


        internal void StartCoroutineMethod() {

            StartCoroutine(WaitForPush());

        }
    }
}
