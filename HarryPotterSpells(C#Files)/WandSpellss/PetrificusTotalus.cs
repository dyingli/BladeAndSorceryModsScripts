using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ThunderRoad;
using UnityEngine;

namespace WandSpellss
{
    class PetrificusTotalus : MonoBehaviour
    {
        Item item;
        Item npcItem;
        GameObject enemy;
        internal AudioSource source;
        public System.Timers.Timer aTimer;
        public void Start()
        {
            item = GetComponent<Item>();



        }

        public void OnCollisionEnter(Collision c)
        {
            if (c.gameObject.GetComponentInParent<Creature>() != null) {
                SetTimer();
                c.gameObject.GetComponentInParent<Creature>().StopAnimation();
                c.gameObject.GetComponentInParent<Creature>().ToogleTPose();

                enemy = c.gameObject;
            }


        }

        private void SetTimer()
        {

            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(7500);
            // Hook up the Elapsed event for the timer. 

            aTimer.Elapsed += OnTimedEvent;

            aTimer.AutoReset = false;
            aTimer.Enabled = true;

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            enemy.GetComponentInParent<Creature>().StopAnimation();
            enemy.GetComponentInParent<Creature>().ToogleTPose();


        }




    }
}
