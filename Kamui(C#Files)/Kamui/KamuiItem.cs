using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace Kamui
{
    public class KamuiItem : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);

            item.gameObject.AddComponent<Attractor>();
            item.gameObject.GetComponent<Attractor>().mainAttractor = true;
            item.gameObject.GetComponent<Attractor>().rb = item.gameObject.GetComponent<Rigidbody>();
            item.gameObject.AddComponent<Kamui>();
        }

    }
}
