using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace WandSpellss
{
    class KeyWordRecogWand
    {

        private string[] m_keywords = new string[]{"Yes"};

        private KeywordRecognizer m_recognizer;

        internal string knownCurrent;
        private bool isListening;
        private Coroutine attentionSpan;

        public void Start() {
            
            m_recognizer = new KeywordRecognizer(m_keywords);
            m_recognizer.OnPhraseRecognized += M_recognizer_OnPhraseRecognized;
            m_recognizer.Start();
        
        }

        private void M_recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
            builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
            builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
            Debug.Log(builder.ToString());
            knownCurrent = builder.ToString();
        }
    }
}
