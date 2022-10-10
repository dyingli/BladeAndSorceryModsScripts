using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;
using System.Collections;
using System.Timers;

namespace MassForcePush
{
    public class MassForcePush : SpellCastCharge
    {

        Collider[] colliderObjects;
        Collider[] collider;
        float randomOffSet;
        bool canLevitate;
        private float elapsedTime;
        bool gestureWorked;
        float distance = 8f;
        List<Collider> tempList = new List<Collider>();
        Item[] tempCollider;
        Vector3 direction;
        List<Item> colliderItems = new List<Item>();
        bool canPushOneHanded;
        List<Item> noDupeColliderItems = new List<Item>();
        CoroutineManager manager = new CoroutineManager();
        int currRandom;
        Vector3 directionUsed;
        RaycastHit hit;

        private Timer bTimer;

        public override void Fire(bool active)
        {
            base.Fire(active);


            if (!active)
            {

                return;
            }
            canPushOneHanded = true;
            colliderObjects = Physics.OverlapSphere(Player.local.transform.position, 5f);


            if (colliderObjects != null)
            {
                
                foreach (Collider c in colliderObjects)
                {
                    
                    if (c.gameObject.GetComponentInParent<Item>() != null)
                    {


                        
                        c.attachedRigidbody.useGravity = false;
                        c.attachedRigidbody.drag = 0.0f;
                        c.attachedRigidbody.mass = 1.0f;
                        c.gameObject.GetComponentInParent<Item>().IgnoreRagdollCollision(Player.local.creature.ragdoll);

                        randomOffSet = (float)UnityEngine.Random.Range(0.6f, 2.6f);
                        float randomAngle = (float)UnityEngine.Random.Range(-360,360);
                        if (c.gameObject.GetComponent<RandomValue>() == null)
                        {

                            c.gameObject.AddComponent<RandomValue>().SetFloat(randomOffSet);
                            c.gameObject.AddComponent<RandomValue>().SetFloatAngle(randomAngle);
                            c.gameObject.GetComponent<RandomValue>().SetStartPoint(c.gameObject.GetComponentInParent<Item>().transform.position);
                        }

                        else if (c.gameObject.GetComponent<RandomValue>() != null)
                        {

                            c.gameObject.GetComponent<RandomValue>().SetFloat(randomOffSet);
                            c.gameObject.AddComponent<RandomValue>().SetFloatAngle(randomAngle);
                            c.gameObject.GetComponent<RandomValue>().SetStartPoint(c.gameObject.GetComponentInParent<Item>().transform.position);
                        }
                        tempList.Add(c);
                        canLevitate = true;
                    }

                }

                foreach (Collider collider in tempList) {

                    if (!noDupeColliderItems.Contains(collider.GetComponentInParent<Item>())){
                        noDupeColliderItems.Add(collider.GetComponentInParent<Item>());
                    }
                }
                tempList.Clear();
                
            }
        }

        public override void UpdateCaster()
        {

            if (Physics.Raycast(Player.local.head.transform.position, Player.local.head.transform.forward, out hit))
            {

                if (hit.collider.transform.parent.GetComponent<Creature>() != null)
                {

                    direction = hit.collider.transform.parent.GetComponent<Creature>().transform.position - Player.local.head.transform.position;

                }

            }

            else
            {
                direction = Player.local.head.transform.forward;
            }
            foreach (Item item in noDupeColliderItems) {

                Quaternion randQuaternion = Quaternion.Euler(0, item.gameObject.GetComponentInChildren<RandomValue>().randomAngle / 2, item.gameObject.GetComponentInChildren<RandomValue>().randomAngle);
                //item.transform.rotation = Quaternion.Slerp(item.transform.rotation,randQuaternion, Time.deltaTime);
                item.gameObject.transform.Rotate(new Vector3(item.gameObject.GetComponentInChildren<RandomValue>().randomAngle, item.gameObject.GetComponentInChildren<RandomValue>().randomAngle / 2, item.gameObject.GetComponentInChildren<RandomValue>().randomAngle)  * Time.deltaTime);
                
            }
            if (canLevitate == true)
            {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / 2f;

                foreach (Collider c in colliderObjects)
                {
                    if (c.gameObject.GetComponentInParent<Item>() != null)
                    {
                        c.gameObject.GetComponentInParent<Item>().transform.position = Vector3.Lerp(c.GetComponent<RandomValue>().startpoint, new Vector3(c.GetComponent<RandomValue>().startpoint.x,
                                                                                                                                c.GetComponent<RandomValue>().startpoint.y + c.GetComponent<RandomValue>().randomFloat,
                                                                                                                                    c.GetComponent<RandomValue>().startpoint.z), Mathf.SmoothStep(0, 1, percentageComplete));
                    }

                }

                if (percentageComplete >= 1.8f)
                {


                    canLevitate = false;
                    elapsedTime = 0f;

                }

            }
            else if (Player.local.footLeft.ragdollFoot.rb.velocity.sqrMagnitude <= 1f) {
                if ((Vector3.Dot(Player.local.handRight.transform.forward.normalized, direction.normalized) > 0.75f && Player.local.handRight.ragdollHand.rb.velocity.sqrMagnitude > 1f) &&
                    (Vector3.Dot(Player.local.handLeft.transform.forward.normalized, direction.normalized) > 0.75f && Player.local.handLeft.ragdollHand.rb.velocity.sqrMagnitude > 1f))
                {

                    if (colliderObjects != null)
                    {


                        foreach (Collider c in colliderObjects)
                        {


                            if (c.gameObject.GetComponentInParent<Item>() != null)
                            {
                                c.gameObject.GetComponent<RandomValue>().transformPos = c.gameObject.GetComponentInParent<Item>().transform.position;
                                c.gameObject.GetComponent<RandomValue>().distance = distance;

                                c.gameObject.GetComponent<RandomValue>().CalculateDirection(direction);
                                c.attachedRigidbody.useGravity = true;
                                c.attachedRigidbody.AddForce(c.gameObject.GetComponent<RandomValue>().direction * 3f, ForceMode.Impulse);

                            }


                        }

                        colliderObjects = null;
                        noDupeColliderItems.Clear();
                    }



                }

                else if ((((Vector3.Dot(Player.local.handRight.transform.forward.normalized, direction.normalized) > 0.75f && Player.local.handRight.ragdollHand.rb.velocity.sqrMagnitude > 2f)) ||
                    (Vector3.Dot(Player.local.handLeft.transform.forward.normalized, direction.normalized) > 0.75f && Player.local.handLeft.ragdollHand.rb.velocity.sqrMagnitude > 2f)) && (noDupeColliderItems != null) && canPushOneHanded == true) {

                    int randomNumber = UnityEngine.Random.Range(0, noDupeColliderItems.Count);
                    Item c = noDupeColliderItems[randomNumber];


                    Debug.Log("Number of Items: " + noDupeColliderItems.Count);
                    Debug.Log("Random Item: " + noDupeColliderItems[randomNumber]);

                    collider = c.gameObject.GetComponentsInChildren<Collider>();
                    
                    foreach (Collider colliders in collider) {

                        if (colliders.gameObject.GetComponent<RandomValue>() != null) {
                            colliders.gameObject.GetComponent<RandomValue>().transformPos = colliders.gameObject.GetComponentInParent<Item>().transform.position;
                            colliders.gameObject.GetComponent<RandomValue>().distance = distance;

                            colliders.gameObject.GetComponent<RandomValue>().CalculateDirection(direction);

                            directionUsed = colliders.gameObject.GetComponent<RandomValue>().direction;
                            if (directionUsed != null)
                            {
                              break;
                            }
                        }

                    }





                        c.GetComponent<Rigidbody>().useGravity = true;
                        c.GetComponent<Rigidbody>().AddForce(directionUsed * 20f, ForceMode.Impulse);
                        noDupeColliderItems.RemoveAt(randomNumber);

                        foreach(Collider colliderRemove in collider) {
                            
                            
                            if (colliderObjects.Contains(colliderRemove)) {



                                colliderObjects.Contains(colliderRemove);
                                List<Collider> tempList = colliderObjects.ToList();
                                tempList.Remove(colliderRemove);
                                colliderObjects = tempList.ToArray();
                            }
                    
                        }
                        canPushOneHanded = false;
                        SetTimer();



                }
                
            }
            
        }

        


        public void RemoveAt<T>(ref T[] arr, int index)
        {
            for (int a = index; a < arr.Length - 1; a++)
            {

                // moving elements downwards, to fill the gap at [index]
                arr[a] = arr[a + 1];

            }
            // finally, let's decrement Array's size by one
            Array.Resize(ref arr, arr.Length - 1);
        }



        private void SetTimer()
        {
            
            // Create a timer with a two second interval.
            bTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 

            bTimer.Elapsed += OnTimedEvent;

            bTimer.AutoReset = false;
            bTimer.Enabled = true;

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            
            canPushOneHanded = true;

        }

    }
}
