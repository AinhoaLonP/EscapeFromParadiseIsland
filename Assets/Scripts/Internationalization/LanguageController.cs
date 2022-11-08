using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Internationalization
{
    public class LanguageController: MonoBehaviour
    {
        public void ChangeToSpanish()
        {
            StaticLanguageController.ChangeToSpanish();
        }

        public void ChangeToEnglish()
        {
            StaticLanguageController.ChangeToEnglish();
        }
    }
}
