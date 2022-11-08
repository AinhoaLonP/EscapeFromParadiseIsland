using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Internationalization
{
    public class SpanishTextMessages : TextMessages
    {
        public override string HealActionMaleMessText { get => "¡Oh no! ¡{paciente} ha muerto debido a la ineptitud de {medico} como médico! Será mejor que no trate a nadie más."; set { } }
        public override string HealActionFemaleMessText { get => "¡Oh no! ¡{paciente} ha muerto debido a la ineptitud de {medico} como médica! Será mejor que no trate a nadie más."; set { } }
        
        public override string PsychologistTreatActionMaleMessText { get => "El intento de {psicologo} por tratar a {paciente} ha sido tan nefasto que éste, sumido en una profunda depresión, ha perdido la motivación por hacer la mayoría de las tareas."; set { } }
        public override string PsychologistTreatActionFemaleMessText { get => "El intento de {psicologo} por tratar a {paciente} ha sido tan nefasto que ésta, sumida en una profunda depresión, ha perdido la motivación por hacer la mayoría de las tareas."; set { } }
        
        public override string PsychologistTreatActionUpgradeMaleText { get => "{paciente} ha mejorado su actitud hacia las siguientes actividades: "; set { } }
        public override string PsychologistTreatActionUpgradeFemaleText { get => "{paciente} ha mejorado su actitud hacia las siguientes actividades: "; set { } }

        public override string PsychologistTreatActionDowngradeText { get => "La actitud de {paciente} hacia las siguientes actividades a empeorado: "; set { } }
               
        public override string TeachActionMaleExpertText { get => "Ahora {alumno} es un experto en esta actividad: {actividad}"; set { } }
        public override string TeachActionFemaleExpertText { get => "Ahora {alumno} es una experta en esta actividad: {actividad}"; set { } }
        
        public override string TeachActionVeryGoodText { get => "Ahora a {alumno} se le da muy bien esta actividad: {actividad}"; set { } }
        
        public override string TeachActionQuiteGoodText { get => "Ahora a {alumno} se le da bastante bien esta actividad: {actividad}"; set { } }
        
        public override string TeachActionALittleBetterText { get => "Ahora a {alumno} se le da un poco mejor esta actividad: {actividad}"; set { } }
        
        public override string TeachActionALittleBetterButBadMaleText { get => "{alumno} ha mejorado un poco en la siguiente actividad, aunque se le sigue dando bastante mal: {actividad}"; set { } }
        public override string TeachActionALittleBetterButBadFemaleText { get => "{alumno} ha mejorado un poco en la siguiente actividad, aunque se le sigue dando bastante mal: {actividad}"; set { } }

        public override string TeachActionBadMaleTeacherText { get => "{maestro} no es un buen profesor, por lo que {alumno} no ha aprendido nada nuevo con él."; set { } }
        public override string TeachActionBadFemaleTeacherText { get => "{maestro} no es una buena profesora, por lo que {alumno} no ha aprendido nada nuevo con ella."; set { } }
        
        public override string TeachActionMaleMessText { get => "{profesor} es tan mal profesor que {alumno} ha aprendido a hacer las cosas de manera incorrecta, por lo que es probable que provoque algún accidente durante sus tareas."; set { } }
        public override string TeachActionFemaleMessText { get => "{profesor} es tan mala profesora que {alumno} ha aprendido a hacer las cosas de manera incorrecta, por lo que es probable que provoque algún accidente durante sus tareas."; set { } }
                
        public override string BuildBoatActionMessMaleText { get => "¡{characterName} es tan torpe que ha tropezado y ha derruído parte de lo que estaba construyendo! Ahora los otros tendrán que trabajar más para arreglar sus destrozos."; set { } }
        public override string BuildBoatActionMessFemaleText { get => "¡{characterName} es tan torpe que ha tropezado y ha derruído parte de lo que estaba construyendo! Ahora los otros tendrán que trabajar más para arreglar sus destrozos."; set { } }

        public override string BuildRefugeActionMessMaleText { get => "¡{characterName} es tan torpe que ha tropezado y ha derruído parte de lo que estaba construyendo! Ahora los otros tendrán que trabajar más para arreglar sus destrozos."; set { } }
        public override string BuildRefugeActionMessFemaleText { get => "¡{characterName} es tan torpe que ha tropezado y ha derruído parte de lo que estaba construyendo! Ahora los otros tendrán que trabajar más para arreglar sus destrozos."; set { } }

        public override string PickFoodActionMessText { get => "¡Oh no! ¡{characterName} ha recogido algunas bayas venenosas y la salud general de los habitantes de la isla ha empeorado!"; set { } }
        
        public override string PickResourcesActionMessMaleText { get => "{characterName} se ha torcido un tobillo mientras recogía materiales. Tendrá que pasar un tiempo en reposo"; set { } }
        public override string PickResourcesActionMessFemaleText { get => "{characterName} se ha torcido un tobillo mientras recogía materiales. Tendrá que pasar un tiempo en reposo"; set { } }

        public override string PickWaterActionMessMaleText { get => "¡Oh no! ¡{characterName} ha derramado el agua recolectada! Será mejor que se dedique a otra cosa que no se le dé tan mal."; set { } }
        public override string PickWaterActionMessFemaleText { get => "¡Oh no! ¡{characterName} ha derramado el agua recolectada! Será mejor que se dedique a otra cosa que no se le dé tan mal."; set { } }
        
        public override string DieText { get => "{characterName} ha muerto debido a su deplorable estado de salud."; set { } }
        
        public override string TooMuchSunText { get => "El sol es demasiado fuerte para estar fuera tantas horas. Más vale que construyan un refugio rápido o podrían sufrir una insolación."; set { } }

        public override string ActionNameForMessagesMedicine { get => "Medicina"; set { } }
        public override string ActionNameForMessagesPsychology { get => "Psicología"; set { } }
        public override string ActionNameForMessagesBeTeacher { get => "Enseñanza"; set { } }
        public override string ActionNameForMessagesBuildBoat { get => "Construir barco"; set { } }
        public override string ActionNameForMessagesBuildRefuge { get => "Construir refugio"; set { } }
        public override string ActionNameForMessagesPickFood { get => "Recolectar comida"; set { } }
        public override string ActionNameForMessagesPickResources { get => "Recolectar materiales"; set { } }
        public override string ActionNameForMessagesPickWater { get => "Recolectar agua"; set { } }

        public override string CanBuildBoatText { get => "Ahora que tus náufragos han cubierto sus necesidades básicas, pueden dedicarse a construir un bote para salir de la isla."; set { } }
    }
}
