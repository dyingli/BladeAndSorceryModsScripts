using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using UnityEngine;
using ThunderRoad;

namespace WandSpellss
{
    class SpeechRecogWands : MonoBehaviour
    {

        GrammarBuilder findServices;
        SpeechRecognitionEngine recognizer;
        public string knownCurrent;

        bool globalWaitFlag;
        public void Start()
        {
            // Create a SpeechRecognitionEngine object for the default recognizer in the en-US locale.
            using (
            recognizer =
              new SpeechRecognitionEngine(
                new System.Globalization.CultureInfo("en-US")))
            {

                // Create a grammar for finding services in different cities.
                Choices spells = new Choices(new string[] { "Avada kedavra", "Stew pify", "Expelliarmus", "PetrificusTotallus" });

                findServices = new GrammarBuilder();
                findServices.Append(spells);

            }


            // Create a Grammar object from the GrammarBuilder and load it to the recognizer.
            Grammar servicesGrammar = new Grammar(findServices);
            recognizer.LoadGrammarAsync(servicesGrammar);


            // Add a handler for the speech recognized event.
            recognizer.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);



            // Configure the input to the speech recognizer.
            recognizer.SetInputToDefaultAudioDevice();

            // Start asynchronous, continuous speech recognition.
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        // Handle the SpeechRecognized event.
        public void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            if (e.Result.Text != null) {
                
                this.knownCurrent = e.Result.Text;
            }


        }

    }
}
