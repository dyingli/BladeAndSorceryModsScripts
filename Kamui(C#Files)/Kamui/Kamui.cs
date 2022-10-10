using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;
using System.Timers;

namespace Kamui
{
    class Kamui : MonoBehaviour
    {
        Item item;
        Collider[] colliderObjects;
        List<Creature> colliderCreature = new List<Creature>();
        List<Item> colliderItems = new List<Item>();
        List<Attractor> attractors = new List<Attractor>();
        Attractor thisAttractor;
        bool attractorOn;
        float attractorToItemDistance;
        float distortionAmount;
        bool stopChecking;
        private float elapsedTime;
        bool isSucked;
        private Timer aTimer;
        bool startDestroy;

        void Start() {

            distortionAmount = 0f;
            stopChecking = false;
            item = GetComponent<Item>();
            thisAttractor = item.gameObject.GetComponent<Attractor>();
            

            
            attractorOn = false;
            startDestroy = false;

            SetTimer();
            
        }

        void Update() {

            if (distortionAmount < 1 && !startDestroy) {

                distortionAmount += 0.01f;
                foreach (Material mat in item.gameObject.GetComponentInChildren<Renderer>().materials) {

                    mat.SetFloat("Vector1_07761f96fcf147a7b17d362b38af7e11",distortionAmount);
                
                
                }
            

            }

            

            float distance = Vector3.Distance(Player.local.creature.transform.position, item.transform.position);
            
            if (distance > 7f && !stopChecking) {

                item.rb.isKinematic = true;
                attractorOn = true;
                thisAttractor.attractorOn = this.attractorOn;
                stopChecking = true;

                colliderObjects = Physics.OverlapSphere(item.transform.position, 4f);
                FindAttractors();
                CreateAttractors();
                thisAttractor.SetFoundAttractor(attractors);

            }

            if (startDestroy) {

                if (distortionAmount > 0.01f) {

                    distortionAmount -= 0.01f;
                    foreach (Material mat in item.gameObject.GetComponentInChildren<Renderer>().materials)
                    {

                        mat.SetFloat("Vector1_07761f96fcf147a7b17d362b38af7e11", distortionAmount);


                    }

                }

                else {

                    item.Despawn();

                
                
                }
            
            }
        
        }


       private void FindAttractors() {


            if (colliderObjects != null)
            {

                foreach (Collider collider in colliderObjects)
                {

                    if (collider.gameObject.GetComponentInParent<Item>() != null)
                    {
                        if (!colliderItems.Contains(collider.gameObject.GetComponentInParent<Item>()))
                        {

                            colliderItems.Add(collider.gameObject.GetComponentInParent<Item>());

                        }

                    }

                   else if (collider.gameObject.GetComponentInParent<Creature>() != null) {


                        if (!colliderCreature.Contains(collider.gameObject.GetComponentInParent<Creature>()))
                        {

                            colliderCreature.Add(collider.gameObject.GetComponentInParent<Creature>());

                        }


                    }
                }


            }


        }


        private void CreateAttractors() {



            if (colliderItems != null)
            {
                Debug.Log("Got past colliderItems null check");
                foreach (Item collideItem in colliderItems)
                {
                    if (collideItem != null)
                    {
                        Debug.Log(collideItem);

                        collideItem.gameObject.AddComponent<Attractor>();
                        Attractor added = collideItem.gameObject.GetComponent<Attractor>();

                        added.rb = collideItem.gameObject.GetComponent<Rigidbody>();
                        Debug.Log(added);
                        if (!attractors.Contains(added))
                        {
                        Debug.Log("Add to list");
                        attractors.Add(added);
                        }

                    }

                }


                foreach (Creature collideCreatures in colliderCreature)
                {
                    if (colliderCreature != null)
                    {

                        collideCreatures.gameObject.AddComponent<Attractor>();
                        Attractor added = collideCreatures.gameObject.GetComponent<Attractor>();

                        added.rb = collideCreatures.gameObject.GetComponent<Rigidbody>();
                        if (!attractors.Contains(added))
                        {
                            Debug.Log("Add to list");
                            attractors.Add(added);
                        }

                    }

                }

            }


        }


        private void SetTimer()
        {

            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(10000);
            // Hook up the Elapsed event for the timer. 

            aTimer.Elapsed += OnTimedEvent;

            aTimer.AutoReset = false;
            aTimer.Enabled = true;

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            startDestroy = true;
        }

    }
}
