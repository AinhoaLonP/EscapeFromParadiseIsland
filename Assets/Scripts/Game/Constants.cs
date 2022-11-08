// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Ainhoa Longo Pérez
// Last Modified On : 06-19-2020
// ***********************************************************************
// <copyright file="BaseDoubleAction.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Assets.Scripts.Game
{
    /// <summary>
    /// Clase que engloba todas las constantes del juego
    /// </summary>
    public static class Constants
    {

        #region Mensajes acciones

        /*
        //HealAction
        /// <summary>
        /// Texto de desastre en acción de curar
        /// </summary>
        public const string healActionMaleMessText = "¡Oh no! ¡{paciente} ha muerto debido a la ineptitud de {medico} como médico! Será mejor que no trate a nadie más.";
        public const string healActionFemaleMessText = "¡Oh no! ¡{paciente} ha muerto debido a la ineptitud de {medico} como médica! Será mejor que no trate a nadie más.";

        //Mensajes que mostrar en al terminar la acción según la habilidad del psicólogo

        /// <summary>
        /// Texto de desastre en acción del psicólogo
        /// </summary>
        public const string psychologistTreatActionMaleMessText = "El intento de {psicologo} por tratar a {paciente} ha sido tan nefasto que éste, sumido en una profunda depresión, ha perdido la motivación por hacer la mayoría de las tareas.";
        public const string psychologistTreatActionFemaleMessText = "El intento de {psicologo} por tratar a {paciente} ha sido tan nefasto que ésta, sumida en una profunda depresión, ha perdido la motivación por hacer la mayoría de las tareas.";
        /// <summary>
        /// Texto informativo sobre la mejora de la actitud
        /// </summary>
        public const string psychologistTreatActionUpgradeText = "{paciente} ha mejorado su actitud hacia las siguientes actividades: ";
        /// <summary>
        /// Texto informativo sobre la disminución de puntos de actitud
        /// </summary>
        public const string psychologistTreatActionDowngradeText = "La actitud de {paciente} hacia las siguientes actividades a empeorado: ";

        //Mensajes que mostrar en al terminar la acción de enseñanza según la habilidad del profesor

        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public const string teachActionMaleExpertText = "Ahora {alumno} es un experto en esta actividad: {actividad}";
        public const string teachActionFemaleExpertText = "Ahora {alumno} es una experta en esta actividad: {actividad}";

        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public const string teachActionVeryGoodText = "Ahora a {alumno} se le da muy bien esta actividad: {actividad}";
        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public const string teachActionQuiteGoodText = "Ahora a {alumno} se le da bastante bien esta actividad: {actividad}";
        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public const string teachActionALittleBetterText = "Ahora a {alumno} se le da un poco mejor esta actividad: {actividad}";
        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public const string teachActionALittleBetterButBadText = "{alumno} ha mejorado un poco en la siguiente actividad, aunque se le sigue dando bastante mal: {actividad}";
        /// <summary>
        /// Texto informativo sobre la mejora de las habilidades de un personaje
        /// </summary>
        public const string teachActionBadMaleTeacherText = "{maestro} no es un buen profesor, por lo que {alumno} no ha aprendido nada nuevo con él.";
        public const string teachActionBadFemaleTeacherText = "{maestro} no es una buena profesora, por lo que {alumno} no ha aprendido nada nuevo con ella.";
        /// <summary>
        /// Texto de desastre en acción enseñar
        /// </summary>
        public const string teachActionMaleMessText = "{profesor} es tan mal profesor que {alumno} ha aprendido a hacer las cosas de manera incorrecta, por lo que es probable que provoque algún accidente durante sus tareas.";
        public const string teachActionFemaleMessText = "{profesor} es tan mala profesora que {alumno} ha aprendido a hacer las cosas de manera incorrecta, por lo que es probable que provoque algún accidente durante sus tareas.";

        //BuildBoatAction
        /// <summary>
        /// Texto de desastre en acción construir barca
        /// </summary>
        public const string buildBoatActionMessText = "¡{characterName} es tan torpe que ha tropezado y ha derruído parte de lo que estaba construyendo! Ahora los otros tendrán que trabajar más para arreglar sus destrozos.";

        //BuildRefugeAction
        /// <summary>
        /// Texto de desastre en acción construir refugio
        /// </summary>
        public const string buildRefugeActionMessText = "¡{characterName} es tan torpe que ha tropezado y ha derruído parte de lo que estaba construyendo! Ahora los otros tendrán que trabajar más para arreglar sus destrozos.";

        //PickFoodAction
        /// <summary>
        /// Texto de desastre en acción recoger comida
        /// </summary>
        public const string pickFoodActionMessText = "¡Oh no! ¡{characterName} ha recogido algunas bayas venenosas y la salud general de los habitantes de la isla ha empeorado!";

        //PickResourcesAction
        /// <summary>
        /// Texto de desastre en acción recoger recursos
        /// </summary>
        public const string pickResourcesActionMessText = "{characterName} se ha torcido un tobillo mientras recogía materiales. Tendrá que pasar un tiempo en reposo";
        
            
        //PickWaterAction        
        /// <summary>
        /// Texto de desastre en acción recoger agua
        /// </summary>
        public const string pickWaterActionMessText = "¡Oh no! ¡{characterName} ha derramado el agua recolectada! Será mejor que se dedique a otra cosa que no se le dé tan mal.";

             
        */

        #endregion

        /// <summary>
        /// Multiplicador del tiempo que un personaje pasa en la playa cuando se tuerce un tobillo comparado con el tiempo normal de descanso
        /// </summary>
        public const int pickResourcesActionTimeMultiplierInRest = 4;

        #region Camera handler
        //CameraHandler

        /// <summary>
        /// Incremento en el zoom
        /// </summary>
        public const float zoomChangeAmount = 80f;
        /// <summary>
        /// Zoom máximo
        /// </summary>
        public const float maxZoom = 78f;
        /// <summary>
        /// Zoom mínimo
        /// </summary>
        public const float minZoom = 30f;
        /// <summary>
        /// Tamaño en pixels del borde sobre el que colocar el cursor para que la cámara se mueva
        /// </summary>
        public const int mDelta = 10;
        /// <summary>
        /// Velocidad de movimiento de la cámara
        /// </summary>
        public const float mSpeed = 20.0f;

        #endregion

        #region Character
        //Character

        /// <summary>
        /// Velocidad de movimiento del personaje
        /// </summary>
        public const float characterSpeed = 3;
        /// <summary>
        /// Velocidad de rotación del personaje
        /// </summary>
        public const float characterRotationSpeed = 4;
        /// <summary>
        /// Tiempo que el personaje sigue apareciendo después de morir
        /// </summary>
        public const int millisecondsCharacterRemainsDead = 3000;
        /// <summary>
        /// Valor a partir del cuál, en un resultado aleatorio, un personaje moriría si se diesen las condiciones para ello
        /// </summary>
        public const double possibleDeathLimit = 0.7;
        /// <summary>
        /// Porcentaje de daño del refugio cada vez que se daña
        /// </summary>
        public const int buildingDamageEachTime = 1;
        /// <summary>
        /// Horas que deben pasar antes de que los personajes corran peligro de insolación
        /// </summary>
        public const int hoursElapsedForSunstrokeDanger = 12;
        /// <summary>
        /// Mínima puntuación a la que pueden bajar con el paso del tiempo en la isla
        /// </summary>
        public const double lowAttitudeLimitAsTimePassing = 0.3;
        /// <summary>
        /// Duración real de una hora en el juego
        /// </summary>
        public const int millisecondsInAnHour = 12000;

        #endregion

        /*
        /// <summary>
        /// Texto a mostrar cuando un personaje muere
        /// </summary>
        public const string dieText = "{characterName} ha muerto debido a su deplorable estado de salud.";
        /// <summary>
        /// Texto a mostrar cuando llevan mucho tiempo al sol
        /// </summary>
        public const string tooMuchSunText = "El sol es demasiado fuerte para estar fuera tantas horas. Más vale que construyan un refugio rápido o podrían sufrir una insolación.";
        */

        #region Points

        //Puntos que obtendrá el usuario en caso de ganar el juego

        /// <summary>
        /// Puntos iniciales del usuario
        /// </summary>
        public const int initialPoints = 3000;
        /// <summary>
        /// Pérdida de puntos del usuario por hora
        /// </summary>
        public const int pointsDecreasePerHour = 10;
        /// <summary>
        /// Pérdida de puntos del usuario cada vez que muere un personaje
        /// </summary>
        public const int pointsDecreasePerDeadCharacter = 50;

        #endregion

        #region Leaderboard
        //Acceso a la tabla de mejores puntuaciones   

        /// <summary>
        /// Código privado de acceso al ranking
        /// </summary>
        public const string privateCode = "6zSi79oaK0i4Ql05wAMDQQV2fphuS5SkeHc72QAl2s_g";
        /// <summary>
        /// Código público de acceso al ranking
        /// </summary>
        public const string publicCode = "5ecd2f90377dce0a143e2fec";
        /// <summary>
        /// Web de acceso al ranking
        /// </summary>
        public const string webURL = "http://dreamlo.com/lb/";

        #endregion

        #region Inventory
        //Inventario       

        /// <summary>
        /// Unidades de agua que cada personaje consume  
        /// </summary>
        public const int waterConsumptionPerCharacter = 1;
        /// <summary>
        /// Frecuencia en horas con la que se consume el agua 
        /// </summary>
        public const int waterConsumptionFrequency = 3;
        /// <summary>
        /// Unidades de comida que cada personaje consume 
        /// </summary>
        public const int foodConsumptionPerCharacter = 1;
        /// <summary>
        /// Frecuencia en horas con la que se consume la comida 
        /// </summary>
        public const int foodConsumptionFrequency = 4;
        /// <summary>
        /// Frecuencia en horas con la que se daña el refugio
        /// </summary>
        public const int buildingDamageFrequency = 3;
        /// <summary>
        /// Cantidad de agua que se derramará cuando ocurre un desastre, por cada personaje activo  
        /// </summary>
        public const int pourWaterQuantity = 5;
        /// <summary>
        /// Porcentaje de salud que bajará cuando ocurre un desastre al recoger comida 
        /// </summary>
        public const int healthDecreaseInFoodMesh = 15;
        /// <summary>
        /// Daño que se le hace al refugio cuando ocurre un desastre durante la construcción  
        /// </summary>
        public const int buildingDamageInMesh = 30;
        /// <summary>
        /// Daño que se le hace al bote cuando ocurre un desastre durante la construcción 
        /// </summary>
        public const int boatDamageInMesh = 10;
        /// <summary>
        /// Salud máxima 
        /// </summary>
        public const int maxHealth = 100;
        public const int maxSocial = 100;
        public const int maxMood = 100;
        /// <summary>
        /// Daño a la salud por hora por el sol 
        /// </summary>
        public const int sunHealthDamagePerHour = 2;
        /// <summary>
        /// Cantidad de materiales necesarios para construir el refugio  
        /// </summary>
        public const int neededResourcesToBuildRefuge = 100;
        /// <summary>
        /// Cantidad de materiales necesarios para reparar el refugio  
        /// </summary>
        public const int neededResourcesToRepairRefuge = 20;
        /// <summary>
        /// Cantidad de materiales necesarios para construir el bote
        /// </summary>
        public const int neededResourcesToBuildBoat = 500;

        #endregion
    }
}
