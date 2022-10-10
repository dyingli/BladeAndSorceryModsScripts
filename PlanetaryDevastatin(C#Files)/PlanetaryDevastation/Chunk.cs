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
    class Chunk : MonoBehaviour
    {

        Vector3 randomSpawn;
        Quaternion originalRotation;
        internal Vector3 originalItemPos;
        float distance;
        float lerpTime;
        private float elapsedTime;
        internal Item itemIn;
        string phase;
        Dictionary<string, float> phases = new Dictionary<string, float>();

        internal bool canStart;
        bool addOrSubtract;
        internal float randomWait;
        internal bool finished;

        String[] phaseNumber;

        float percentageComplete;
        private float totalElapsedTime;

        internal bool hasBeenAdded;

        float yPos;
        private Timer aTimer;

        internal AudioSource[] audios;
        internal AudioClip clip;

        bool firstSound;
        bool secondSound;

        bool stopCheckFor;
        void Start() {

            firstSound = false;
            secondSound = false;

            this.gameObject.AddComponent<AudioSource>();
            this.gameObject.GetComponent<AudioSource>().spatialBlend = 1f;
            this.gameObject.GetComponent<AudioSource>().volume = 1f;
            this.gameObject.GetComponent<AudioSource>().priority = 80;
            this.gameObject.GetComponent<AudioSource>().spread = 200f;
            this.gameObject.GetComponent<AudioSource>().minDistance = 100f;
            this.gameObject.GetComponent<AudioSource>().maxDistance = 200f;


            originalRotation = this.gameObject.transform.rotation;
            hasBeenAdded = false;
            phaseNumber = new String[] {"1","2","3","4" };

            phase = phaseNumber[(int)UnityEngine.Random.Range(0,3)];
            originalItemPos = this.gameObject.transform.position;

            if (this.gameObject.GetComponent<Rigidbody>() is Rigidbody rigid) {

                rigid.mass = 1000f / (251f / this.gameObject.GetComponent<Renderer>().bounds.size.magnitude);
                rigid.isKinematic = true;
            
            
            }

            if (phase == "1") {


                randomWait = UnityEngine.Random.Range(0f, 12.5f);

            }

            else if (phase == "2") {


                randomWait = UnityEngine.Random.Range(12.5f, 25f);

            }

            else if (phase == "3")
            {


                randomWait = UnityEngine.Random.Range(25f, 37.5f);

            }

            else if (phase == "4")
            {


                randomWait = UnityEngine.Random.Range(37.5f, 50f);

            }

            stopCheckFor = false;

            randomSpawnPoint();
            canStart = false;
        }

        
        
        private void randomSpawnPoint() {

            CastRay();
            if (yPos != null) {
                randomSpawn = new Vector3(Player.local.creature.transform.position.x + UnityEngine.Random.Range(-50f, 50f), yPos, Player.local.creature.transform.position.z + UnityEngine.Random.Range(-50f, 50f));
            }

            this.gameObject.transform.position = randomSpawn;
            this.gameObject.transform.rotation = new Quaternion(0f,0f,0f,0f);
        }


        internal void Update() {

            if (clip != null && !stopCheckFor) {


                this.gameObject.GetComponent<AudioSource>().clip = clip;
                stopCheckFor = true;

            }

            if (canStart) {

                
                distance = Vector3.Distance(itemIn.transform.position, this.gameObject.transform.position);


                totalElapsedTime += Time.deltaTime;
                

                if (totalElapsedTime / randomWait >= 1) {
                    elapsedTime += Time.deltaTime;
                    percentageComplete = elapsedTime / (5f * distance);
                    this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, originalItemPos, Mathf.SmoothStep(0, 1, percentageComplete));
                    this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, originalRotation,Mathf.SmoothStep(0,1,percentageComplete));


                }





                if (this.gameObject.transform.position == originalItemPos)
                {

                    
                    finished = true;
                    canStart = false;
                    elapsedTime = 0f;

                    

                }
            }

        }

        internal void CreateRigidbodies() {

            if (this.gameObject.GetComponent<Rigidbody>() != null) {

                this.gameObject.AddComponent<Rigidbody>();
            

            
            }

        
        
        }

        internal void CastRay()
        {


            RaycastHit hit;
            Transform parent;

            if (Physics.Raycast(this.transform.position, Vector3.down, out hit))
            {

                Debug.Log("Did hit.");
                Debug.Log(hit.collider.gameObject.transform.parent.name);


                yPos = hit.collider.gameObject.transform.parent.position.y - 10f;
                
                
                

               
            }


        }

        public void IgnoreChunks(Transform chunks) {


            
                foreach (Transform ignore in chunks)
                {
                    if (ignore != this)
                    {
                        Physics.IgnoreCollision(this.gameObject.GetComponent<MeshCollider>(), ignore.gameObject.GetComponent<MeshCollider>(), true);
                    }
                }
             

        
        }


        public void StartDespawnTimer() {


            SetTimer();
        
        
        }



        private void SetTimer()
        {

            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(8000);
            // Hook up the Elapsed event for the timer. 

            aTimer.Elapsed += OnTimedEvent;

            aTimer.AutoReset = false;
            aTimer.Enabled = true;

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Destroy(this);
        }



    }
}
