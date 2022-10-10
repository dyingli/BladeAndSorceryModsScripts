using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Rendering;

using UnityEngine.VFX;

namespace EightInnerGates
{
    class MultipleRenderers : MonoBehaviour

    {

        List<SkinnedMeshRenderer> playerMeshRenderer = new List<SkinnedMeshRenderer>();
        Renderer pRenderer;
        VisualEffect VFXGraph;

        int renderNumber;

        bool graphLoaded;
        bool setupDone;

        internal bool gateOpened;

        void Start() {

            graphLoaded = false;
            gateOpened = false;
            renderNumber = 0;
            Debug.Log("Hit multi renderer start method");

            foreach (Creature.RendererData renders in Player.local.creature.renderers) {

                if (renders.meshPart != null)
                {

                    playerMeshRenderer.Add(renders.meshPart.GetComponentInChildren<SkinnedMeshRenderer>());
                }


            }

            setupDone = false;
            

            Debug.Log("Got past foreach in start");



            var op = Addressables.LoadAssetAsync<GameObject>("apoz123EightGates.EigthGate.VFX");
            op.Completed += Op_Completed1;

            
        }

        void Update() {

            if (graphLoaded && !setupDone)
            {

                
                foreach (SkinnedMeshRenderer renderer in playerMeshRenderer)
                {

                    VisualEffect effectIn = VFXGraph.DeepCopyByExpressionTree();

                    GameObject objectToSpawn = new GameObject();
                    objectToSpawn.AddComponent<VisualEffect>();
                    objectToSpawn.GetComponent<VisualEffect>().visualEffectAsset = effectIn.visualEffectAsset;
                    Instantiate(objectToSpawn);
                    

                    renderer.gameObject.AddComponent<SkinnedMeshToMesh>().Setup(renderer, 0.02f, objectToSpawn.GetComponent<VisualEffect>());

                }

                setupDone = true;

            }




        }
        public void RunRenderer() {

            Debug.Log("Running renderers");



                int count = 0;
            if (graphLoaded)
            {
                if (playerMeshRenderer != null)
                {


                    foreach (SkinnedMeshRenderer renderer in playerMeshRenderer)
                    {
                        Debug.Log("Looping over playerMeshRenderer");

                        if (renderer.gameObject.GetComponent<SkinnedMeshToMesh>() is SkinnedMeshToMesh smtm)
                        {
                            smtm.VFXGraph.Play();
                            smtm.StartUpdate();

                        }


                     }




                }



                else
                {

                    Debug.Log("No skinned mesh renderers available.");


                }

            }

                



        }

        private void Op_Completed1(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                // Setting the visual effect
                Debug.Log("Got visual effect graph");
                VFXGraph = obj.Result.GetComponent<VisualEffect>();

                graphLoaded = true;
            }

            else {

                Debug.Log("Did not retrieve visual effect graph");
            
            }
        }

        
        
    }
}
