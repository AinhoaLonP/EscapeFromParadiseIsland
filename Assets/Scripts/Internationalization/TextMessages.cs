using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Internationalization
{
    public abstract class TextMessages
    {
        //HealAction
        /// <summary>
        /// Texto de desastre en acción de curar
        /// </summary>
        public abstract string HealActionMaleMessText { get; set; }
        public abstract string HealActionFemaleMessText { get; set; }

        //Mensajes que mostrar en al terminar la acción según la habilidad del psicólogo

        /// <summary>
        /// Texto de desastre en acción del psicólogo
        /// </summary>
        public abstract string PsychologistTreatActionMaleMessText { get; set; }
        public abstract string PsychologistTreatActionFemaleMessText { get; set; }
        /// <summary>
        /// Texto informativo sobre la mejora de la actitud
        /// </summary>
        public abstract string PsychologistTreatActionUpgradeMaleText { get; set; }
        public abstract string PsychologistTreatActionUpgradeFemaleText { get; set; }
        /// <summary>
        /// Texto informativo sobre la disminución de puntos de actitud
        /// </summary>
        public abstract string PsychologistTreatActionDowngradeText { get; set; }

        //Mensajes que mostrar en al terminar la acción de enseñanza según la habilidad del profesor

        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public abstract string TeachActionMaleExpertText { get; set; }
        public abstract string TeachActionFemaleExpertText { get; set; }

        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public abstract string TeachActionVeryGoodText { get; set; }
        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public abstract string TeachActionQuiteGoodText { get; set; }
        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public abstract string TeachActionALittleBetterText { get; set; }
        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public abstract string TeachActionALittleBetterButBadMaleText { get; set; }
        public abstract string TeachActionALittleBetterButBadFemaleText { get; set; }
        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public abstract string TeachActionBadMaleTeacherText { get; set; }
        public abstract string TeachActionBadFemaleTeacherText { get; set; }
        /// <summary>
        /// Texto de desastre en acción enseñar
        /// </summary>
        public abstract string TeachActionMaleMessText { get; set; }
        public abstract string TeachActionFemaleMessText { get; set; }

        //BuildBoatAction
        /// <summary>
        /// Texto de desastre en acción abstractruir barca
        /// </summary>
        public abstract string BuildBoatActionMessMaleText { get; set; }
        public abstract string BuildBoatActionMessFemaleText { get; set; }

        //BuildRefugeAction
        /// <summary>
        /// Texto de desastre en acción abstractruir refugio
        /// </summary>
        public abstract string BuildRefugeActionMessMaleText { get; set; }
        public abstract string BuildRefugeActionMessFemaleText { get; set; }

        //PickFoodAction
        /// <summary>
        /// Texto de desastre en acción recoger comida
        /// </summary>
        public abstract string PickFoodActionMessText { get; set; }

        //PickResourcesAction
        /// <summary>
        /// Texto de desastre en acción recoger recursos
        /// </summary>
        public abstract string PickResourcesActionMessMaleText { get; set; }
        public abstract string PickResourcesActionMessFemaleText { get; set; }

        public abstract string PickWaterActionMessMaleText { get; set; }
        public abstract string PickWaterActionMessFemaleText { get; set; }

        /// <summary>
        /// Texto a mostrar cuando un personaje muere
        /// </summary>
        public abstract string DieText { get; set; }

        /// <summary>
        /// Texto a mostrar cuando llevan mucho tiempo al sol
        /// </summary>
        public abstract string TooMuchSunText { get; set; }

        //Nombres de las acciones
        public abstract string ActionNameForMessagesMedicine { get; set; }
        public abstract string ActionNameForMessagesPsychology { get; set; }
        public abstract string ActionNameForMessagesBeTeacher { get; set; }
        public abstract string ActionNameForMessagesBuildBoat { get; set; }
        public abstract string ActionNameForMessagesBuildRefuge { get; set; }
        public abstract string ActionNameForMessagesPickFood { get; set; }
        public abstract string ActionNameForMessagesPickResources { get; set; }
        public abstract string ActionNameForMessagesPickWater { get; set; }

        // Mensaje para mostrar al usuario cuando puedan empezar a construir una barca
        public abstract string CanBuildBoatText { get; set; }

    }
}
