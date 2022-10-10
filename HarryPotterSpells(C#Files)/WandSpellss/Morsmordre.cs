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
    class Morsmordre : MonoBehaviour
    {

        Item item;
        internal ItemData darkMark;
        private Timer aTimer;
        

        public void Start() {

            item = GetComponent<Item>();

            

            SetTimer();
        }




        private void SetTimer()
        {

            aTimer = new System.Timers.Timer(2500);

            aTimer.Elapsed += OnTimedEvent;

            aTimer.AutoReset = false;
            aTimer.Enabled = true;

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {

            Debug.Log("DarkMark ItemData: " + darkMark);

            darkMark.SpawnAsync(projectile => {

                projectile.gameObject.AddComponent<DarkMark>();
                projectile.transform.position = item.transform.position;
                projectile.rb.useGravity = false;
                projectile.rb.drag = 0.0f;

                item.Despawn();

            });

            

            
        }

    }
}
