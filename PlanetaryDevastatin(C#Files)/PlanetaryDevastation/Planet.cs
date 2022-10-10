using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;
using System.Timers;

namespace PlanetaryDevastation
{
    public class Planet : MonoBehaviour
    {
        Transform meshTransform;
        GameObject mesh;
        Item item;
        bool canPut;
        internal int numberFinished = 0;
        bool started;
        bool destroyed;
        List<AudioSource> audios;
        private Timer aTimer;

        public void Start() {


            
            item = GetComponent<Item>();


            meshTransform = item.GetCustomReference("PlanetaryDevastationReference");
            mesh = meshTransform.gameObject;


            foreach (Transform chunk in mesh.transform) {

                chunk.gameObject.AddComponent<Chunk>();
                chunk.gameObject.AddComponent<Rigidbody>();
                chunk.gameObject.GetComponent<Chunk>().clip = item.GetComponentsInChildren<AudioSource>()[UnityEngine.Random.Range(0, item.GetComponentsInChildren<AudioSource>().Length - 2)].clip;
            }


            item.GetComponentsInChildren<AudioSource>()[2].playOnAwake = false;

            canPut = true;

            started = false;


            destroyed = false;
            
            
        }

        public void OnCollisionEnter(Collision c) {

            if (!destroyed)
            {
                Debug.Log("CollisionOccured");
                List<Rigidbody> rigids = new List<Rigidbody>();


                Collider[] colliders = Physics.OverlapSphere(item.transform.position, 50f);

                foreach (Collider collider in colliders)
                {

                    if (collider.GetComponentInParent<Rigidbody>() is Rigidbody body)
                    {

                        if (!Player.local.GetComponentsInChildren<Rigidbody>().Contains(body))
                        {
                            if (!rigids.Contains(body))
                            {

                                rigids.Add(body);


                            }
                        }

                    }


                }

                foreach (Transform chunk in mesh.transform)
                {
                    rigids.Add(chunk.gameObject.GetComponent<Rigidbody>());

                }

                foreach (Rigidbody rigid in rigids)
                {

                    Vector3 forceDirection = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(0f, 50f), UnityEngine.Random.Range(-50f, 50f));
                    rigid.isKinematic = false;
                    if (rigid.gameObject.GetComponent<Chunk>() != null)
                    {
                        rigid.AddForce(forceDirection * (0.75f * rigid.mass), ForceMode.Impulse);

                    }

                    else {

                        rigid.AddForce(forceDirection *(4f * rigid.mass), ForceMode.Impulse);

                    }


                }

                //item.transform.DetachChildren();
                //item.Despawn();

                destroyed = true;

                //item.GetComponentInChildren<EffectParticle>().Play();
                
            }



            


        }
        
        void Update() {

            if (numberFinished >= mesh.transform.childCount && canPut)
            {

                canPut = false;
                item.rb.useGravity = true;
                
            }

            if (canPut)
            {
                
                PutTogether();
                started = true;
            }

            

        }



        private void PutTogether() {

            foreach (Transform chunk in mesh.transform) {

                if (chunk.gameObject.GetComponent<Chunk>() != null) {

                    if (!started)
                    {
                        chunk.gameObject.GetComponent<Chunk>().itemIn = item;
                        chunk.gameObject.GetComponent<Chunk>().canStart = true;
                    }
                    else
                    {
                        if (chunk.gameObject.GetComponent<Chunk>().finished == true && !chunk.gameObject.GetComponent<Chunk>().hasBeenAdded)
                        {
                            chunk.gameObject.GetComponent<Chunk>().hasBeenAdded = true;
                            numberFinished += 1;
                        }
                        
                    }



                }
            
            }
        
        
        }


        private void SetTimer()
        {

            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(35000);
            // Hook up the Elapsed event for the timer. 

            aTimer.Elapsed += OnTimedEvent;

            aTimer.AutoReset = false;
            aTimer.Enabled = true;

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            item.GetComponentsInChildren<AudioSource>()[2].Stop();
        }

    }
}
