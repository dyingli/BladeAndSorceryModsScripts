using UnityEngine;
using ThunderRoad;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Kamui
{
    class ReduceSizeOverTime : MonoBehaviour
    {

        Item item;
        internal bool isSucked;
        private float elapsedTime;
        Vector3 minScale;
        Material kamuiDistortion;
        private float distortionValue;
        Creature creature;
        public void Start() {
            if (this.GetComponent<Item>() != null)
            {
                item = GetComponent<Item>();
            }

            else if (this.GetComponent<Creature>() != null) {

                creature = GetComponent<Creature>();
            
            }

            minScale = new Vector3(0.01f,0.01f,0.01f);


            var op = Addressables.LoadAssetAsync<Material>("apoz123Sharingan.Mat.KamuiDistortion");
            op.Completed += Op_Completed;

            distortionValue = 0f;


        }

        private void Op_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Material> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                // Setting the material
                kamuiDistortion = obj.Result;
            }
        }

        void Update() {

            if (isSucked)
            {
                if (distortionValue < 1f)
                {
                    distortionValue += 0.01f;
                    item.gameObject.GetComponent<Renderer>().sharedMaterial = kamuiDistortion;
                    item.gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("Vector1_07761f96fcf147a7b17d362b38af7e11", distortionValue);
                }

                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / 0.2f;

                if (item != null)
                {
                    item.transform.localScale = Vector3.Lerp(item.transform.localScale, minScale, Mathf.SmoothStep(0, 1, percentageComplete));
                }

                else if (creature != null) {
                
                    creature.transform.localScale = Vector3.Lerp(creature.transform.localScale, minScale, Mathf.SmoothStep(0, 1, percentageComplete));

                }

                



                elapsedTime = 0f;

                if (item != null)
                {
                    if (item.transform.localScale == minScale)
                    {


                        isSucked = false;
                        item.Despawn();

                    }
                }

                else if (creature != null)
                {
                    if (creature.transform.localScale == minScale)
                    {


                        isSucked = false;
                        creature.Despawn();

                    }
                }





            }


        }

    }
}
