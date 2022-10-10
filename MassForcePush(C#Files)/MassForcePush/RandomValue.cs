using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;
namespace MassForcePush
{
    class RandomValue : MonoBehaviour
    {
        internal float randomFloat;
        internal Vector3 startpoint;
        internal float randomAngle;
        internal Vector3 direction;
        internal float distance;
        internal Vector3 transformPos;
        Vector3 point;



        public void SetStartPoint(Vector3 vector3In) {

            this.startpoint = vector3In;
        
        }

        public void SetFloat(float floatIn) {


            this.randomFloat = floatIn;
        
        }
        public void SetFloatAngle(float floatIn)
        {


            this.randomAngle = floatIn;

        }



        public void CalculateDirection(Vector3 directionIn) {

            point = directionIn * distance;

            direction = Player.local.head.transform.forward + (Player.local.head.transform.position + (Player.local.head.transform.forward * (Vector3.Distance(transformPos,Player.local.head.transform.position) + distance) - transformPos)).normalized;
            //direction = point - transformPos;



        }
    }
}
