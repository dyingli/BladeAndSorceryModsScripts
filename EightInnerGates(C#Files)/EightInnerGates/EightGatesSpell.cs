using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace EightInnerGates
{
    class EightGatesSpell : SpellCastProjectile
    {

        VoiceActivatedGates currentGate;
        MultipleRenderers renderers;

        EffectData effectData;
        bool gateOpened;

        public override void Load(SpellCaster spellCaster, Level level)
        {
            base.Load(spellCaster, level);

            spellCaster.gameObject.AddComponent<VoiceActivatedGates>();
            spellCaster.gameObject.AddComponent<MultipleRenderers>();
            renderers = spellCaster.gameObject.GetComponent<MultipleRenderers>();
            currentGate = spellCaster.gameObject.GetComponent<VoiceActivatedGates>();

            
            Debug.Log("Eight gates loaded");
        }

        public override void Unload()
        {
            base.Unload();

            UnityEngine.Object.Destroy(spellCaster.gameObject.GetComponent<VoiceActivatedGates>());
            UnityEngine.Object.Destroy(spellCaster.gameObject.GetComponent<MultipleRenderers>());
        }


        public override void UpdateCaster()
        {
            base.UpdateCaster();


            if (currentGate.hasRecognizedWord == true)
            {

                currentGate.hasRecognizedWord = false;



                if (currentGate.knownCurrent.Contains("Open Eighth Gate of Death"))
                {
                     Debug.Log("Gate of death ACTIVATED!");

                     renderers.gateOpened = gateOpened;
                     renderers.RunRenderer();

                }

                else if(currentGate.knownCurrent.Contains("Close")){

                    renderers.gateOpened = false;


                }

            }

            
            
            
        }


    }
}
