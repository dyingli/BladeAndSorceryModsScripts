using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ThunderRoad;
using UnityEngine;
using TOR;



namespace LightsaberSave
{
    class JSONParser : MonoBehaviour
    {
        
        internal Item item;
        internal ItemData itemData;
        string stringJson;
        private ItemLightsaberSaveData customData;

        internal void CreateItemData() {
            int iterative = 0;
            item.GetComponent<ItemLightsaber>().UpdateCustomData();
            foreach (LightsaberBlade crystal in item.GetComponent<ItemLightsaber>().module.lightsaberBlades) {

                this.item.TryGetCustomData<ItemLightsaberSaveData>(out customData);
                if (customData != null)
                {
                    crystal.kyberCrystal = customData.kyberCrystals[iterative];
                    crystal.bladeLength = customData.bladeLengths[iterative];
                }
                iterative += 1;


                
            
            }
            itemData = item.GetComponent<ItemLightsaber>().module.itemData;

            
            

            


        }

        internal void GenerateJson(int saveSlot)
        {

            string myPath = "C:/Program Files (x86)/Steam/steamapps/common/Blade & Sorcery/BladeAndSorcery_Data/StreamingAssets/Mods/The Outer Rim/Items/Lightsabers/CustomSave/save"+ saveSlot.ToString() + ".json";

            itemData.category = "Custom Sabers";
            itemData.id = "saved" + saveSlot.ToString();
            itemData.displayName = "Saved Saber" + saveSlot.ToString();
            itemData.localizationId = "saved" + saveSlot.ToString();
            stringJson = JsonConvert.SerializeObject(itemData, Catalog.jsonSerializerSettings);

            File.WriteAllText(myPath, stringJson);


        }

    }
}

