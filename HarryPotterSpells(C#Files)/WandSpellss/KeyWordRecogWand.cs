using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;
using UnityEngine.Windows.Speech;

namespace WandSpellss
{
    class KeyWordRecogWand : MonoBehaviour

    {
        internal bool hasRecognizedWord;
        private string[] m_keywords = new string[]{"Stewpify","Expelliarmus","Ahvahduhkuhdahvra","PetrificusTotalus","Levicorpus", "Liberacorpus","Protego","Lumos","Assio","Engorgio","Evanesco","Geminio","Sectumsempra","Nox","Ascendio", "Vincere mortem", "Morsmordre" };

        private KeywordRecognizer m_recognizer;

        internal string knownCurrent;
        private bool isListening;
        private Coroutine attentionSpan;

        public void Start() {

            hasRecognizedWord = false;
            m_recognizer = new KeywordRecognizer(m_keywords);
            m_recognizer.OnPhraseRecognized += M_recognizer_OnPhraseRecognized;
            m_recognizer.Start();
        
        }

        private void M_recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            StringBuilder builder = new StringBuilder();
            hasRecognizedWord = true;
            builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
            builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
            builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
            knownCurrent = builder.ToString();
        }
    }
}
