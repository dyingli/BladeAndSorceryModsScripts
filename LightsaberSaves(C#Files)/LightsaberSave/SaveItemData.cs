using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using Newtonsoft.Json;

namespace LightsaberSave
{
    class SaveItemData : ItemData
    {
        [JsonProperty(PropertyName = "$type")]
        public string saveType;



    }
}
