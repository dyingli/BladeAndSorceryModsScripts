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
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace WandSpellss
{ 
    class VoiceWeaponComponent : MonoBehaviour
    {

        float bTimerTime = 1500f;
        List<Material> myMaterials;
        Stack<GameObject> stack = new Stack<GameObject>();
        Item item;
        Item currentDistortion;
        Item current;
        Item previous;
        Item currentShooters;
        Item currentGoldenLight;
        Item currentAvadaLightning;
        Material evanescoDissolve;
        GameObject keyWordRecog;
        ItemData avadaLightning;
        public float spellSpeed;
        public bool magicEffect;
        private float expelliarmusPower;
        int currIndex;
        Dictionary<string, ItemData> spells = new Dictionary<string, ItemData>();
        private KeyWordRecogWand speech;
        bool waitFlag;
        bool determiner;
        internal List<ItemData> spellsList = new List<ItemData>();
        List<string> spellsListWithText = new List<string>();
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
        bool canEngorgio;
        bool canEvanesco;
        float elapsedTime;
        float duration;
        Vector3 startPoint;
        Vector3 endPoint;
        GameObject parentLocal;
        RagdollHand oppositeHand;
        private Timer aTimer;
        ItemData distortionEffect;
        Creature currentHit;
        Vector3 engorgioMaxSize;
        Item dissovledItem;
        List<GameObject> evanescoStore = new List<GameObject>();
        float dissolveVal;
        ItemData textObject;
        private Timer bTimer;
        GameObject currentText;
        private bool objectIsHovering;

        ItemData DarkMark;

        GameObject keyWords;

        internal List<Creature> hitByLevicorpus = new List<Creature>();


        GameObject recognizer;
        string recognizedWord;
        KeyWordRecogWand recogWand;
        private bool usedAscendio;

        List<Horcrux> horcruxes = new List<Horcrux>();

        Soul playerSoul;

        bool canMakeHorcrux;

        void Start() {

            //Horcrux info setup
            playerSoul = Player.local.creature.gameObject.GetComponent<Soul>();
            canMakeHorcrux = false;

            //item initialization
            item = GetComponent<Item>();

            //voice recognizer setup
            item.gameObject.AddComponent<KeyWordRecogWand>();
            recognizer = item.gameObject;
            recogWand = recognizer.GetComponent<KeyWordRecogWand>();

            //Spell data setup
            spellsList.Add(Catalog.GetData<ItemData>("StupefyObject"));
            spellsList.Add(Catalog.GetData<ItemData>("ExpelliarmusObject"));
            spellsList.Add(Catalog.GetData<ItemData>("AvadaKedavraObject"));
            spellsList.Add(Catalog.GetData<ItemData>("PetrificusTotalusObject"));
            spellsList.Add(Catalog.GetData<ItemData>("LevicorpusObject"));
            spellsList.Add(Catalog.GetData<ItemData>("LumosObject"));
            spellsList.Add(Catalog.GetData<ItemData>("ProtegoObject"));
            spellsList.Add(Catalog.GetData<ItemData>("SectumsempraObject"));
            spellsList.Add(Catalog.GetData<ItemData>("MorsmordreObject"));
            DarkMark = Catalog.GetData<ItemData>("TheDarkMark");

            //Evanesco Material Setup
            var op = Addressables.LoadAssetAsync<Material>("apoz123Wand.SpellEffect.Evanesco.Mat");
            op.Completed += Op_Completed1;

            //On land event for ascendio 
            Player.local.locomotion.OnGroundEvent += Locomotion_OnGroundEvent;

        }

        


        private void Op_Completed1(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Material> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                // Setting the material
                evanescoDissolve = obj.Result;
            }
        }
        

        void Update() {

            if (recogWand != null)
            {

                if (recogWand.hasRecognizedWord == true)
                {

                    recogWand.hasRecognizedWord = false;

                    if (recogWand.knownCurrent != null)
                    {

                        if (recogWand.knownCurrent.Contains("Stewpify"))
                        {
                            Debug.Log("Got past comparing words");
                            spellsList[0].SpawnAsync(projectile =>
                            {
                                // Configure projectile.
                                // Set the position and rotation of the projectile (the stupefy) the same as the flyDirRef position and rotation

                                projectile.transform.position = item.flyDirRef.position;
                                projectile.transform.rotation = item.flyDirRef.rotation;
                                // Same as usual
                                projectile.IgnoreRagdollCollision(Player.local.creature.ragdoll);
                                projectile.IgnoreObjectCollision(item);
                                projectile.Throw();


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

                            });


                        }

                        else if (recogWand.knownCurrent.Contains("Expelliarmus"))
                        {
                            Debug.Log("Got past comparing words");
                            spellsList[1].SpawnAsync(projectile =>
                            {
                                projectile.transform.position = item.flyDirRef.position;
                                projectile.transform.rotation = item.flyDirRef.rotation;
                            // Same as usual
                            projectile.IgnoreRagdollCollision(Player.local.creature.ragdoll);
                                projectile.IgnoreObjectCollision(item);
                                projectile.Throw();


                                projectile.gameObject.AddComponent<Expelliarmus>();
                                projectile.gameObject.GetComponent<Expelliarmus>().power = expelliarmusPower;

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


                            });
                        }

                        else if (recogWand.knownCurrent.Contains("Ahvahduhkuhdahvra"))
                        {
                            Debug.Log("Got past comparing words");
                            spellsList[2].SpawnAsync(projectile =>
                            {

                                projectile.transform.position = item.flyDirRef.position;
                                projectile.transform.rotation = item.flyDirRef.rotation;
                            // Same as usual
                            projectile.IgnoreRagdollCollision(Player.local.creature.ragdoll);
                                projectile.IgnoreObjectCollision(item);
                                projectile.Throw();

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


                            });

                            canMakeHorcrux = true;

                            SetTimer();

                        }

                        else if (recogWand.knownCurrent.Contains("PetrificusTotalus"))
                        {
                            Debug.Log("Got past comparing words");
                            spellsList[3].SpawnAsync(projectile =>
                            {
                                projectile.transform.position = item.flyDirRef.position;
                                projectile.transform.rotation = item.flyDirRef.rotation;
                            // Same as usual
                            projectile.IgnoreRagdollCollision(Player.local.creature.ragdoll);
                                projectile.IgnoreObjectCollision(item);
                                projectile.Throw();

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

                            });
                        }

                        else if (recogWand.knownCurrent.Contains("Levicorpus"))
                        {
                            Debug.Log("Got past comparing words");
                            spellsList[4].SpawnAsync(projectile =>
                            {

                                projectile.transform.position = item.flyDirRef.position;
                                projectile.transform.rotation = item.flyDirRef.rotation;
                            // Same as usual
                            projectile.IgnoreRagdollCollision(Player.local.creature.ragdoll);
                                projectile.IgnoreObjectCollision(item);
                                projectile.Throw();

                                projectile.gameObject.AddComponent<Levicorpus>();
                                projectile.gameObject.GetComponent<Levicorpus>().spawnerWeapon = item;
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

                                        case "LevicorpusSound":
                                            sourceCurrent = c;
                                            break;

                                    }

                                }
                                sourceCurrent.Play();


                            });
                        }

                        else if (recogWand.knownCurrent.Contains("Liberacorpus"))
                        {

                            Debug.Log("Got past LiberaCorpus check");
                            if (hitByLevicorpus.Count > 0)
                            {
                                foreach (Creature creature in hitByLevicorpus)
                                {

                                    Debug.Log(creature);
                                    if (creature.footLeft.gameObject.GetComponent<SpringJoint>() != null && creature.footRight.gameObject.GetComponent<SpringJoint>() != null)
                                    {
                                        Debug.Log("Got past double foot check.");
                                        Destroy(creature.footLeft.gameObject.GetComponent<SpringJoint>());

                                        Destroy(creature.footRight.gameObject.GetComponent<SpringJoint>());


                                    }


                                }

                                hitByLevicorpus.Clear();
                                hitByLevicorpus.TrimExcess();

                            }


                        }

                        else if (recogWand.knownCurrent.Contains("Lumos"))
                        {

                            if (current != null)
                            {

                                if (current.name.Contains("Lumos") == false)
                                {
                                    spellsList[5].SpawnAsync(projectile =>
                                    {

                                        current = projectile;
                                        sourceCurrent = projectile.gameObject.GetComponent<AudioSource>();
                                        sourceCurrent.Play();
                                    });
                                }

                            }

                            else
                            {

                                spellsList[5].SpawnAsync(projectile =>
                                {

                                    current = projectile;
                                    sourceCurrent = projectile.gameObject.GetComponent<AudioSource>();
                                    sourceCurrent.Play();
                                });

                            }


                        }

                        else if (recogWand.knownCurrent.Contains("Nox"))
                        {

                            if (current != null)
                            {

                                if (current.name.Contains("Lumos"))
                                {
                                    foreach (AudioSource c in item.GetComponentsInChildren<AudioSource>())
                                    {

                                        switch (c.name)
                                        {

                                            case "NoxSound":
                                                sourceCurrent = c;
                                                break;

                                        }

                                    }
                                    sourceCurrent.Play();

                                    current.Despawn();
                                    current = null;

                                }

                            }


                        }


                        else if (recogWand.knownCurrent.Contains("Protego"))
                        {

                            spellsList[6].SpawnAsync(projectile =>
                            {


                                projectile.rb.useGravity = false;
                                projectile.rb.drag = 0.0f;
                                current = projectile;


                                foreach (AudioSource c in item.GetComponentsInChildren<AudioSource>())
                                {

                                    switch (c.name)
                                    {

                                        case "ProtegoSound":
                                            sourceCurrent = c;
                                            break;

                                    }

                                }

                                sourceCurrent.Play();
                            });

                        }

                        else if (recogWand.knownCurrent.Contains("Evanesco"))
                        {

                            item.gameObject.AddComponent<Evanesco>();

                            item.gameObject.GetComponent<Evanesco>().evanescoDissolve = evanescoDissolve;
                            item.gameObject.GetComponent<Evanesco>().CastRay();
                            //parentLocal = item.gameObject.GetComponent<Evanesco>().parentLocal;

                            //Debug.Log(item.gameObject.GetComponent<Evanesco>().evanescoDissolve);

                        }

                        else if (recogWand.knownCurrent.Contains("Ascendio"))
                        {

                            item.gameObject.AddComponent<Ascendio>();
                            item.gameObject.GetComponent<Ascendio>().Ascend();

                            usedAscendio = true;



                        }

                        else if (recogWand.knownCurrent.Contains("Sectumsempra"))
                        {

                            spellsList[7].SpawnAsync(projectile =>
                            {

                                projectile.IgnoreRagdollCollision(Player.local.creature.ragdoll);
                                projectile.IgnoreObjectCollision(item);
                                projectile.Throw();

                                projectile.rb.useGravity = false;

                                Debug.Log(projectile);
                                projectile.rb.drag = 0.0f;
                                currentShooters = projectile;
                            //Add the force in the direction of the flyDirRef (the blue axis in unity)
                                projectile.rb.AddForce(item.flyDirRef.transform.forward * 100f, ForceMode.Impulse);
                                projectile.gameObject.AddComponent<sempraDespawn>();



                                foreach (AudioSource c in item.GetComponentsInChildren<AudioSource>())
                                {

                                    switch (c.name)
                                    {

                                        case "SectumsempraSound":
                                            sourceCurrent = c;
                                            break;

                                    }

                                }

                                sourceCurrent.Play();
                            });

                        }

                        else if (recogWand.knownCurrent.Contains("Vincere mortem"))
                        {

                            if (item.mainHandler.otherHand.grabbedHandle.item.gameObject.GetComponent<Horcrux>() == null && canMakeHorcrux == true)
                            {
                                if (item.mainHandler.otherHand != null)
                                {

                                    item.mainHandler.otherHand.grabbedHandle.item.gameObject.AddComponent<Horcrux>();
                                    horcruxes.Add(item.mainHandler.otherHand.grabbedHandle.item.gameObject.GetComponent<Horcrux>());
                                    canMakeHorcrux = false;


                                }
                            }

                            

                        }
                        else if (recogWand.knownCurrent.Contains("Engorgio"))
                        {
                            item.gameObject.AddComponent<Engorgio>();
                            item.gameObject.GetComponent<Engorgio>().CastRay();


                        }

                        else if (recogWand.knownCurrent.Contains("Morsmordre"))
                        {


                            spellsList[8].SpawnAsync(projectile =>
                            {

                                projectile.transform.position = item.flyDirRef.position;
                                projectile.transform.rotation = item.flyDirRef.rotation;
                            // Same as usual
                            projectile.IgnoreRagdollCollision(Player.local.creature.ragdoll);
                                projectile.IgnoreObjectCollision(item);
                                projectile.Throw();

                                projectile.gameObject.AddComponent<Morsmordre>();
                                projectile.gameObject.GetComponent<Morsmordre>().darkMark = DarkMark;
                                projectile.rb.useGravity = false;
                                projectile.rb.drag = 0.0f;

                                currentShooters = projectile;

                            //Add the force in the direction of the flyDirRef (the blue axis in unity)
                            projectile.rb.AddForce(item.flyDirRef.transform.forward * spellSpeed, ForceMode.Impulse);

                                projectile.gameObject.AddComponent<SpellDespawn>();

                            });
                        }
                    }



                }
                if (current != null)
                {

                    if (current.name.Contains("Lumos"))
                    {

                        current.transform.position = item.flyDirRef.transform.position;
                        current.transform.rotation = item.flyDirRef.transform.rotation;

                    }

                    else if (current.name.Contains("Quad"))
                    {

                        current.transform.position = item.flyDirRef.transform.position;
                        current.transform.rotation = item.flyDirRef.transform.rotation;


                        if (sourceCurrent != null)
                        {
                            if (!sourceCurrent.isPlaying)
                            {
                                current.Despawn();
                                current = null;
                            }
                        }


                    }

                }

            }
        
        
        }

        private void Locomotion_OnGroundEvent(Vector3 groundPoint, Vector3 velocity, Collider groundCollider)
        {
            if (usedAscendio == true)
            {
                usedAscendio = false;
                Player.fallDamage = true;
            }
        }

        public void Setup(float importSpeed, bool importMagicEffect, float importExpelliarmusPower)
        {
            spellSpeed = importSpeed;
            magicEffect = importMagicEffect;
            expelliarmusPower = importExpelliarmusPower;
        }

        private void SetTimer()
        {

            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(30000);
            // Hook up the Elapsed event for the timer. 

            aTimer.Elapsed += OnTimedEvent;

            aTimer.AutoReset = false;
            aTimer.Enabled = true;

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            canMakeHorcrux = false;
        }
    }
}
