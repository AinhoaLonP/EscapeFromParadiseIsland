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
using Assets.Scripts.Game;
using Assets.Scripts.Internationalization;
using Assets.Scripts.UI;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Clase que representa la acción de construir barca
    /// . Implementa a <see cref="Assets.Scripts.Actions.BaseSimpleAction" />
    /// </summary>
    /// <seealso cref="Assets.Scripts.Actions.BaseSimpleAction" />
    public class BuildBoatAction : BaseSimpleAction
    {
        /// <summary>
        /// Nombre del objeto al que deben dirigirse para realizar una acción
        /// </summary>
        public override string DestinationName { get => "InConstructionBoat"; set { } }
        /// <summary>
        /// Nombre de la acción en el Animator
        /// </summary>
        public override string ActionName { get => "build"; set { } }
        /// <summary>
        /// Texto que se muestra cuando una acción sale mal
        /// </summary>
        public override string MessText { get => StaticLanguageController.textMessages.BuildBoatActionMessMaleText; set { } }
        /// <summary>
        /// Cantidad en la que incrementar las unidades del inventario
        /// </summary>
        public override int IncrementQuantity { get => 10; set { } }
        /// <summary>
        /// Nombre de la acción para mostrar en los mensajes para el usuario
        /// </summary>
        public override string ActionNameForMessages { get => StaticLanguageController.textMessages.ActionNameForMessagesBuildBoat; set { } }
        
        /// <summary>Objeto de la barca construida</summary>
        private GameObject finishedBoat;
        /// <summary>Panel que indica que el usuario ha ganado la partida</summary>
        private GameObject winPanel;
        /// <summary>Determina si el usuario ha ganado la partida</summary>
        private bool won = false;

        /// <summary>
        /// Llamado al comienzo de la partida. Oculta el objeto de la barca y el panel de partida ganada
        /// </summary>
        void Start()
        {
            finishedBoat = GameObject.Find("FinishedBoat");
            finishedBoat.SetActive(false);

            winPanel = GameObject.Find("WinPanel");
            winPanel.SetActive(false);

        }

        /// <summary>
        /// El personaje seleccionado realiza la acción durante el tiempo estipulado
        /// </summary>
        /// <param name="characterMovement">Script del personaje seleccionado</param>
        public override void MakeAction(Game.CharacterController characterMovement)
        {
            
            DateTime currentTime = DateTime.Now;
            CharacterAction charAction = CharacterActionsController.GetCharacterActionOfCharacter(characterMovement.transform);
            double elapsedTimeMilliseconds = ((TimeSpan)(currentTime - charAction.StartTime)).TotalMilliseconds;
            characterMovement.actionRemainingTime = (Int32)(ActionDurationMilliseconds - elapsedTimeMilliseconds) / 1000;
            characterMovement.ShowActionRemainingTime();
            if (elapsedTimeMilliseconds >= ActionDurationMilliseconds)
            {
                FinishAction(charAction, characterMovement);
            }
            else
            {
                characterMovement.animator.SetBool(ActionName, true);
                //Al comenzar la acción, se manda a la posición que va a ocupar el bote el objeto que representa al bote sin construir
                if (!Inventory.boatFinished)
                    charAction.ObjectToGo.transform.position = new Vector3(charAction.ObjectToGo.transform.position.x, -0.94f, charAction.ObjectToGo.transform.position.z);
            }            
        }

        /// <summary>
        /// Incrementa el porcentaje de construcción del bote en un valor que dependerá de la cantidad de materiales disponibles y de la habilidad del personaje
        /// </summary>
        /// <param name="charAction">CharacterAction correspondiente al personaje seleccionado y a esta acción</param>
        /// <param name="characterMovement">Script del personaje seleccionado</param>
        public override void FinishAction(CharacterAction charAction, Game.CharacterController characterMovement)
        {
            double aptitude = characterMovement.characterStats.Where(a => a.Action.Name == this.GetType().Name).FirstOrDefault().Aptitude;
            characterMovement.animator.SetBool(ActionName, false);
            charAction.Status = CharacterActionStatus.ReturningFromActionLocation;
            characterMovement.HideActionRemainingTime();
            if (aptitude == 0)
            {
                MakeAMess(characterMovement);
            }
            int consumedResources;
            double calculatedConsumedResources = Constants.neededResourcesToBuildBoat * IncrementQuantity * aptitude / 100;
            double neededResourcesToFinishBoat = Constants.neededResourcesToBuildBoat - ((Constants.neededResourcesToBuildBoat * Inventory.boatBuildPercentage) / 100);
            consumedResources = Convert.ToInt32(Math.Min(calculatedConsumedResources, Math.Min(neededResourcesToFinishBoat, Inventory.resourcesUnits)));
            Inventory.ConsumeResources(consumedResources);
            
            Inventory.Increment(this, Convert.ToInt32(consumedResources * 100 / Constants.neededResourcesToBuildBoat));
            if (Inventory.boatFinished)
            {
                //Cuando se haya finalizado con la construcción, se oculta el bote sin construir y se muestra el acabado
                GameObject logs = charAction.ObjectToGo;
                logs.transform.position = new Vector3(logs.transform.position.x, -5, logs.transform.position.z);
                finishedBoat.SetActive(true);

                if (!won)
                    Win();
            }
        }

        /// <summary>
        /// Rompe el bote
        /// </summary>
        public override void MakeAMess(Assets.Scripts.Game.CharacterController characterMovement)
        {
            string text = MessText;
            if (characterMovement.gender == Utils.Gender.Female)
                text = StaticLanguageController.textMessages.BuildBoatActionMessFemaleText;
            text = text.Replace("{characterName}", characterMovement.characterName);
            PopupManager.ShowMessage(text);
            Inventory.TearDownBoat();
        }

        /// <summary>
        /// Ganar partida
        /// </summary>
        public void Win()
        {
            won = true;

            //Se calcula la puntuación final en base al número de supervivientes y al tiempo transcurrido
            int totalElapsedHours = CourseOfTime.elapsedHours + (CourseOfTime.elapsedDays * 24);
            int deadCharacters = CharacterActionsController.GetNumberOfCharacters() - CharacterActionsController.GetNumberOfActiveCharacters();
            int totalScore = Math.Max(20, Constants.initialPoints - (totalElapsedHours * Constants.pointsDecreasePerHour) - (deadCharacters * Constants.pointsDecreasePerDeadCharacter));
            
            //Se muestra el mensaje de final de partida
            GameObject popupPanel = GameObject.Find("PopupMessage");
            if (popupPanel != null)
                popupPanel.SetActive(false);

            winPanel.SetActive(true);
            Time.timeScale = 0f;
            GameObject score = GameObject.Find("ScoreText");
            TextMeshProUGUI mText = score.transform.GetComponent<TextMeshProUGUI>();
            mText.text = totalScore.ToString();
        }
    }
}
