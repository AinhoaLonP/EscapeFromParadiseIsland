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
using UnityEngine;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Clase que representa la acción de construir refugio
    /// . Implementa a <see cref="Assets.Scripts.Actions.BaseSimpleAction" />
    /// </summary>
    /// <seealso cref="Assets.Scripts.Actions.BaseSimpleAction" />
    public class BuildRefugeAction : BaseSimpleAction
    {
        /// <summary>
        /// Nombre del objeto al que deben dirigirse para realizar una acción
        /// </summary>
        public override string DestinationName { get => "InConstructionRefuge"; set { } }
        /// <summary>
        /// Nombre de la acción en el Animator
        /// </summary>
        public override string ActionName { get => "build"; set { } }
        /// <summary>
        /// Texto que se muestra cuando una acción sale mal
        /// </summary>
        public override string MessText { get => StaticLanguageController.textMessages.BuildRefugeActionMessMaleText; set { } }
        /// <summary>
        /// Cantidad en la que incrementar las unidades del inventario
        /// </summary>
        public override int IncrementQuantity { get => 20; set { } }
        /// <summary>
        /// Nombre de la acción para mostrar en los mensajes para el usuario
        /// </summary>
        public override string ActionNameForMessages { get => StaticLanguageController.textMessages.ActionNameForMessagesBuildRefuge; set { } }

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
                //Al comenzar la acción, se manda a la posición que va a ocupar el refugio el objeto que representa al refugio sin construir
                if (!Inventory.refugeFinished)
                    charAction.ObjectToGo.transform.position = new Vector3(charAction.ObjectToGo.transform.position.x, 0, charAction.ObjectToGo.transform.position.z);
            }            
        }

        /// <summary>
        /// Incrementa el porcentaje de construcción del refugio en un valor que dependerá de la cantidad de materiales disponibles y de la habilidad del personaje
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
            //Si el refugio ya se ha construído, se reparará
            if (Inventory.refugeFinished)
            {
                consumedResources = Convert.ToInt32(Constants.neededResourcesToRepairRefuge * IncrementQuantity * aptitude / 100);
                Inventory.ConsumeResources(Convert.ToInt32(consumedResources));
                Inventory.Increment(this, Convert.ToInt32(consumedResources * 100 / Constants.neededResourcesToRepairRefuge));
            }
            else
            {
                //La cantidad de materiales consumidos será siempre la misma salvo que quede poco para terminar el refugio (o pocos materiales)
                double calculatedConsumedResources = Constants.neededResourcesToBuildRefuge * IncrementQuantity * aptitude / 100;
                double neededResourcesToFinishRefuge = Constants.neededResourcesToBuildRefuge - ((Constants.neededResourcesToBuildRefuge * Inventory.refugeBuildPercentage) / 100);
                consumedResources = Convert.ToInt32(Math.Min(calculatedConsumedResources, Math.Min(neededResourcesToFinishRefuge, Inventory.resourcesUnits)));
                Inventory.ConsumeResources(consumedResources);
                Inventory.Increment(this, Convert.ToInt32(consumedResources * 100 / Constants.neededResourcesToBuildRefuge));
            }
            if (Inventory.refugeFinished)
            {
                //Cuando se haya finalizado con la construcción, se oculta el refugio sin construir y se muestra el acabado
                GameObject logs = charAction.ObjectToGo;
                logs.transform.position = new Vector3(logs.transform.position.x, -2, logs.transform.position.z);
                GameObject finishedRefuge = GameObject.Find("FinishedRefuge");
                finishedRefuge.transform.position = new Vector3(finishedRefuge.transform.position.x, 0, finishedRefuge.transform.position.z);
            }
        }

        /// <summary>
        /// Derruye parte del refugio
        /// </summary>
        public override void MakeAMess(Assets.Scripts.Game.CharacterController characterMovement)
        {
            string text = MessText;
            if (characterMovement.gender == Utils.Gender.Female)
                text = StaticLanguageController.textMessages.BuildRefugeActionMessFemaleText;
            text = text.Replace("{characterName}", characterMovement.characterName);
            PopupManager.ShowMessage(text);
            Inventory.TearDownRefuge();
        }
    }
}
