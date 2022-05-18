using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using System.Timers;
using System.Collections;
using System.IO;

namespace WandSpellss
{

    public class MyWeaponComponent : MonoBehaviour
    {
        Item item;
        Item currentDistortion;
        Item current;
        Item previous;
        Item currentShooters;
        Item currentGoldenLight;
        Item currentAvadaLightning;
        ItemData avadaLightning;
        public float spellSpeed;
        public bool magicEffect;
        int currIndex;
        Dictionary<string, ItemData> spells = new Dictionary<string, ItemData>();
        private KeyWordRecogWand speech;
        bool waitFlag;
        bool determiner;
        List<ItemData> spellsList = new List<ItemData>();
        Accio rayAccio;
        int pressedIndex;
        AudioSource sourceCurrent;
        AudioSource touch;
        GameObject sound;
        int firstTouch;
        internal bool canSpeak;
        bool canFire;
        bool ignoreTouch;
        bool playSound;
        GameObject sourceObj;
        bool canRayCast;
        bool canAccio;
        float elapsedTime;
        float duration;
        Vector3 startPoint;
        Vector3 endPoint;
        GameObject parentLocal;
        RagdollHand oppositeHand;
        private Timer aTimer;
        ItemData distortionEffect;
        Creature currentHit;

        public void Start()
        {
            canAccio = false;
            playSound = false;
            ignoreTouch = false;
            canFire = false;
            firstTouch = 0;
            touch = GetComponent<AudioSource>();
            pressedIndex = 0;
            waitFlag = false;
            avadaLightning = Catalog.GetData<ItemData>("AvadaKedavraLightning");
            distortionEffect = Catalog.GetData<ItemData>("Distortion");
            item = GetComponent<Item>();
            spells.Add("Expelliarmus", Catalog.GetData<ItemData>("ExpelliarmusObject"));
            spellsList.Add(Catalog.GetData<ItemData>("StupefyObject"));
            spellsList.Add(Catalog.GetData<ItemData>("ExpelliarmusObject"));
            spellsList.Add(Catalog.GetData<ItemData>("AvadaKedavraObject"));
            spellsList.Add(Catalog.GetData<ItemData>("PetrificusTotalusObject"));
            spellsList.Add(Catalog.GetData<ItemData>("ProtegoObject"));
            spellsList.Add(Catalog.GetData<ItemData>("LumosObject"));
            spellsList.Add(Catalog.GetData<ItemData>("LumosObject"));

            currIndex = 0;
            item.OnHeldActionEvent += Item_OnHeldActionEvent;
            item.OnGrabEvent += Item_OnGrabEvent;
            //speech = new KeyWordRecogWand();
            
            //speech.Start();


            

            //StartCoroutine(Speak());

        }



        
        private void Item_OnGrabEvent(Handle arg1, RagdollHand arg2)
        {

                if (firstTouch == 0 && magicEffect == true)
                {
                    Vector3 transformLocal = new Vector3(Player.local.head.transform.position.x, Player.local.head.transform.position.y, Player.local.head.transform.position.z);
                    Quaternion tranformRotLocal = new Quaternion(Player.local.head.transform.rotation.x, Player.local.head.transform.rotation.y, Player.local.head.transform.rotation.z, Player.local.head.transform.rotation.w);
                    Catalog.GetData<ItemData>("GoldenLight").SpawnAsync(spawned =>
                    {

                        Debug.Log(spawned);
                        spawned.transform.position = new Vector3(Player.local.head.transform.position.x, Player.local.head.transform.position.y + 2.5f, Player.local.head.transform.position.z);
                        spawned.transform.rotation = Player.local.head.transform.rotation;
                        spawned.rb.useGravity = false;
                        spawned.rb.drag = 0.0f;

                        currentGoldenLight = spawned;

                    });

                    touch.Play();

                firstTouch++;
            }

            

        }
        
        public void Update()
        {

            /*
            if (speech.knownCurrent != null)
            {
                
                Catalog.GetData<ItemData>("ExpelliarmusObject").SpawnAsync(projectile =>
                {
                    projectile.gameObject.AddComponent<Expelliarmus>();

                    projectile.rb.useGravity = false;
                    projectile.rb.drag = 0.0f;
                //Add the force in the direction of the flyDirRef (the blue axis in unity)
                    projectile.rb.AddForce(item.flyDirRef.transform.forward * spellSpeed, ForceMode.Impulse);
                    projectile.gameObject.AddComponent<SpellDespawn>();

                    sourceCurrent = projectile.gameObject.GetComponent<Expelliarmus>().source.DeepCopyByExpressionTree();

                    sourceCurrent.Play();
                    
                });
                speech.knownCurrent = null;
            }

            */
            oppositeHand = item.mainHandler.otherHand;
            endPoint = oppositeHand.transform.position;
            if (currentDistortion != null)
            {
                currentDistortion.transform.position = item.flyDirRef.position;
                currentDistortion.transform.rotation = item.flyDirRef.rotation;
            }
               

            if (currentGoldenLight != null)
            {
                currentGoldenLight.transform.position = new Vector3(Player.local.head.transform.position.x, Player.local.head.transform.position.y + 2.5f, Player.local.head.transform.position.z);
                currentGoldenLight.transform.rotation = Player.local.head.transform.rotation;
            }


            if (currIndex == 4)
            {

                current.transform.position = item.flyDirRef.transform.position;
                current.transform.rotation = item.flyDirRef.transform.rotation;
            }
            else if (currIndex == 5)
            {
                current.transform.position = item.flyDirRef.position;
                current.transform.rotation = item.flyDirRef.transform.rotation;
            }

            if (canAccio == true)
            {

                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / duration;

                float distanceSqr = (endPoint - parentLocal.transform.position).sqrMagnitude;
                parentLocal.transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.SmoothStep(0, 1, percentageComplete));

                if (distanceSqr <= 0.03f)
                {

                    canAccio = false;

                    elapsedTime = 0f;
                    if (oppositeHand == Player.local.handLeft.ragdollHand)
                    {
                        Player.currentCreature.GetHand(Side.Left).Grab(parentLocal.GetComponent<Item>().mainHandleRight);
                    }
                    else
                    {

                        Player.currentCreature.GetHand(Side.Right).Grab(parentLocal.GetComponent<Item>().mainHandleRight);

                    }

                }
            }


            if (touch.isPlaying == false)
            {
                canFire = true;
                ignoreTouch = true;
                if (currentGoldenLight != null)
                {
                    currentGoldenLight.Despawn();
                }

            }

            /*
            if (item.isGripped)
            {
                StartCoroutine(Speak());
            }
            */









        }

        private bool determinerSetTrue()
        {

            return determiner;
        }

        IEnumerator Speak()
        {

            while (true)
            {


                yield return new WaitForSeconds(2);


             }


            
        }

        





        //working spells with List
        
        private void Item_OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {


            
            if (action == Interactable.Action.UseStart) {
                
                    if (currIndex == spellsList.Count - 1)
                    {
                        
                        currIndex = 0;
                        pressedIndex = 0;
                        
                        if (previous != null)
                        {
                            previous.Despawn();
                        }

                    }

                    else
                    {
                        
                        currIndex = currIndex + 1;
                        pressedIndex = 0;
                        
                        if (previous != null)
                        {
                            previous.Despawn();
                        }
                    
                    }
                
            }
            
            
            if (action == Interactable.Action.AlternateUseStart && canFire == true)
            {
                //item.mainHandler.grabbedHandle.HapticRumble(item.mainHandler.playerHand);
                if (currIndex != 4 && currIndex != 5 && currIndex != 6) {
                    SetTimer();
                    distortionEffect.SpawnAsync(distortion => {
                        currentDistortion = distortion;
                    });
                }
                if (currIndex != 6)
                {
                    
                    spellsList[currIndex].SpawnAsync(projectile =>
                        {
                        // Configure projectile.
                        // Set the position and rotation of the projectile (the stupefy) the same as the flyDirRef position and rotation

                            projectile.transform.position = item.flyDirRef.position;
                            projectile.transform.rotation = item.flyDirRef.rotation;
                        // Same as usual
                            projectile.IgnoreRagdollCollision(Player.local.creature.ragdoll);
                            projectile.IgnoreObjectCollision(item);
                            projectile.Throw();


                            if (currIndex == 0)
                            {
                                projectile.gameObject.AddComponent<Stupefy>();

                                projectile.rb.useGravity = false;
                                projectile.rb.drag = 0.0f;

                                currentShooters = projectile;

                                //Add the force in the direction of the flyDirRef (the blue axis in unity)
                                projectile.rb.AddForce(item.flyDirRef.transform.forward * spellSpeed, ForceMode.Impulse);
                                projectile.gameObject.AddComponent<SpellDespawn>();

                                foreach (AudioSource c in item.GetComponentsInChildren<AudioSource>())
                                {

                                    switch (c.name)
                                    {

                                        case "StupefySound":
                                            sourceCurrent = c;
                                            break;

                                    }

                                }

                                sourceCurrent.Play();
                                playSound = true;
                            }

                            else if (currIndex == 1)
                            {
                                projectile.gameObject.AddComponent<Expelliarmus>();

                                projectile.rb.useGravity = false;
                                projectile.rb.drag = 0.0f;

                                currentShooters = projectile;

                                //Add the force in the direction of the flyDirRef (the blue axis in unity)
                                projectile.rb.AddForce(item.flyDirRef.transform.forward * spellSpeed, ForceMode.Impulse);

                                projectile.gameObject.AddComponent<SpellDespawn>();
                                foreach (AudioSource c in item.GetComponentsInChildren<AudioSource>())
                                {

                                    switch (c.name)
                                    {

                                        case "ExpelliarmusSound":
                                            sourceCurrent = c;
                                            break;

                                    }

                                }
                                sourceCurrent.Play();

                            }
                            else if (currIndex == 2)
                            {


                                projectile.gameObject.AddComponent<AvadaKedavra>();
                                projectile.rb.useGravity = false;
                                projectile.rb.drag = 0.0f;
                                currentShooters = projectile;
                                //Add the force in the direction of the flyDirRef (the blue axis in unity)
                                projectile.rb.AddForce(item.flyDirRef.transform.forward * spellSpeed, ForceMode.Impulse);
                                projectile.gameObject.AddComponent<SpellDespawn>();
                                foreach (AudioSource c in item.GetComponentsInChildren<AudioSource>())
                                {

                                    switch (c.name)
                                    {

                                        case "AvadaSound":
                                            sourceCurrent = c;
                                            break;

                                    }

                                }
                                sourceCurrent.Play();
                            }
                            else if (currIndex == 3)
                            {
                                projectile.gameObject.AddComponent<PetrificusTotalus>();

                                projectile.rb.useGravity = false;
                                projectile.rb.drag = 0.0f;
                                currentShooters = projectile;
                                //Add the force in the direction of the flyDirRef (the blue axis in unity)
                                projectile.rb.AddForce(item.flyDirRef.transform.forward * spellSpeed, ForceMode.Impulse);
                                projectile.gameObject.AddComponent<SpellDespawn>();
                                foreach (AudioSource c in item.GetComponentsInChildren<AudioSource>())
                                {

                                    switch (c.name)
                                    {

                                        case "PetrificusTotallusSound":
                                            sourceCurrent = c;
                                            break;

                                    }

                                }
                                sourceCurrent.Play();
                            }
                            /*
                            else if (currIndex == 4)
                            {

                                    pressedIndex = pressedIndex + 1;
                                    projectile.gameObject.AddComponent<Aguamenti>();

                                    projectile.rb.useGravity = false;
                                    projectile.rb.drag = 0.0f;



                                    current = projectile;
                                    if (pressedIndex == 2)
                                    {
                                        previous.Despawn();
                                        pressedIndex = 0;
                                        previous = current;
                                    }
                                    else
                                    {
                                        if (previous != null)
                                        {
                                            previous.Despawn();
                                            previous = null;
                                        }
                                        previous = current;
                                    }
                                Debug.Log(current);
                                Debug.Log(previous);
                            }       
                            */

                            else if (currIndex == 4)
                            {
                                pressedIndex = pressedIndex + 1;
                                projectile.gameObject.AddComponent<Protego>();

                                projectile.rb.useGravity = false;
                                projectile.rb.drag = 0.0f;
                                current = projectile;

                                if (pressedIndex == 2)
                                {
                                    previous.Despawn();
                                    previous = null;
                                    pressedIndex = 0;
                                    previous = current;
                                }
                                else
                                {
                                    if (previous != null)
                                    {
                                        previous.Despawn();
                                        previous = null;
                                    }
                                    previous = current;
                                }
                                sourceCurrent = projectile.gameObject.GetComponent<Protego>().source;
                                sourceCurrent.Play();

                            }

                            else if (currIndex == 5)
                            {
                                pressedIndex = pressedIndex + 1;
                                projectile.rb.useGravity = false;
                                projectile.rb.drag = 0.0f;

                                current = projectile;
                                if (pressedIndex == 2)
                                {

                                    previous.Despawn();
                                    previous = null;
                                    pressedIndex = 0;
                                    previous = current;
                                }
                                else
                                {
                                    if (previous != null)
                                    {
                                        previous.Despawn();
                                        previous = null;
                                    }
                                    previous = current;
                                }
                                sourceCurrent = projectile.gameObject.GetComponent<AudioSource>();
                                sourceCurrent.Play();

                            }

                        });


                    
                }

                if (currIndex == 6)
                {

                    item.gameObject.AddComponent<Accio>();
                    item.gameObject.GetComponent<Accio>().CastRay();
                    
                    parentLocal = item.gameObject.GetComponent<Accio>().parentLocal;

                    startPoint = item.gameObject.GetComponent<Accio>().startPoint;
                    endPoint = item.gameObject.GetComponent<Accio>().endPoint;
                    duration = 0.6f;
                    if (item.gameObject.GetComponent<Accio>().cantAccio == true)
                    {
                        canAccio = false;
                    }
                    else {
                        if (oppositeHand.playerHand.ragdollHand.isGrabbed)
                        {
                            canAccio = false;
                        }
                        else {

                            canAccio = true;
                        
                        }
                    }
                    

                }




            }
        
            

        }

        private void SetTimer()
        {

            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(200);
            // Hook up the Elapsed event for the timer. 

            aTimer.Elapsed += OnTimedEvent;

            aTimer.AutoReset = false;
            aTimer.Enabled = true;

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (currentDistortion != null) {
                currentDistortion.Despawn();
            }


        }



        public void Setup(float importSpeed, bool importMagicEffect)
        {
            spellSpeed = importSpeed;
            magicEffect = importMagicEffect;
        }


    }
}
