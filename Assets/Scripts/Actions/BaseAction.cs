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
using Assets.Scripts.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Representa las actividades que un personaje puede realizar
    /// </summary>
    public abstract class BaseAction : MonoBehaviour, Action
    {
        public TextMessages textMessages;

        /// <summary>
        /// Nombre del objeto al que deben dirigirse para realizar una acción
        /// </summary>
        public abstract string DestinationName { get; set; }
        /// <summary>
        /// Nombre de la acción en el Animator
        /// </summary>
        public abstract string ActionName { get; set; }
        /// <summary>
        /// Texto que se muestra cuando una acción sale mal
        /// </summary>
        public abstract string MessText { get; set; }
        /// <summary>
        /// Duración de la acción
        /// </summary>
        public int ActionDurationMilliseconds { get; set; } = 20000;
        /// <summary>
        /// Nombre de la acción para mostrar en los mensajes para el usuario
        /// </summary>
        public abstract string ActionNameForMessages { get; set; } 
        
        /// <summary>
        /// Determina si un personaje realiza o no la acción que se le mande, según un resultado aleatorio y su actitud hacia ella
        /// </summary>
        /// <param name="attitude">Actitud del personaje hacia la acción</param>
        /// <returns></returns>
        public virtual bool MakesAction(double attitude)
        {
            double random = Math.Round(StaticRandom.Instance.NextDouble(), 1);
            if (random <= attitude)
                return true;
            return false;
        }

        public IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            Action action = new RestAction();
            action.SelectAction();
        }

        /// <summary>
        /// El personaje seleccionado comienza a realizar la acción
        /// </summary>
        /// <param name="characterMovement">Script del personaje seleccionado</param>
        public virtual void StartAction(Game.CharacterController characterMovement)
        {
            CharacterAction charAction = CharacterActionsController.GetCharacterActionOfCharacter(characterMovement.transform);
            charAction.StartTime = DateTime.Now;
            characterMovement.actionRemainingTime = ActionDurationMilliseconds / 1000;
            characterMovement.ShowActionRemainingTime();
            LookAtSomething(charAction);
        }

        /// <summary>
        /// El personaje seleccionado realiza la acción durante el tiempo estipulado
        /// </summary>
        /// <param name="characterMovement">Script del personaje seleccionado</param>
        public virtual void MakeAction(Game.CharacterController characterMovement)
        {
            DateTime currentTime = DateTime.Now;
            CharacterAction charAction = CharacterActionsController.GetCharacterActionOfCharacter(characterMovement.transform);
            double elapsedTimeMilliseconds = ((TimeSpan)(currentTime - charAction.StartTime)).TotalMilliseconds;
            characterMovement.actionRemainingTime = (Int32)(ActionDurationMilliseconds - elapsedTimeMilliseconds) / 1000;
            characterMovement.ShowActionRemainingTime();
            if (elapsedTimeMilliseconds >= ActionDurationMilliseconds)
            {
                FinishAction(charAction, characterMovement);
                ChangeSecondaryCharacterStatus(charAction);
            }
            else
            {
                characterMovement.animator.SetBool(ActionName, true);
            }            
        }

        /// <summary>
        /// Marca la acción actual como seleccionada
        /// </summary>
        public abstract void SelectAction();

        /// <summary>
        /// Una vez finalizada la acción, en caso de ser doble, cambia el estado de la acción pasiva a "ReturningFromActionLocation"
        /// </summary>
        /// <param name="charAction">CharacterAction correspondiente al personaje seleccionado y a esta acción</param>
        public abstract void ChangeSecondaryCharacterStatus(CharacterAction charAction);

        /// <summary>
        /// La acción finaliza y el personaje vuelve al centro, quedando disponible para una nueva actividad
        /// </summary>
        /// <param name="charAction">CharacterAction correspondiente al personaje seleccionado y a esta acción</param>
        /// <param name="characterMovement">Script del personaje seleccionado</param>
        public abstract void FinishAction(CharacterAction charAction, Game.CharacterController characterMovement);

        /// <summary>
        /// Cuando el personaje tiene habilidad 0 en la acción, hace un destrozo
        /// </summary>
        /// <param name="characterMovement">Controlador del personaje</param>
        public abstract void MakeAMess(Assets.Scripts.Game.CharacterController characterMovement);

        /// <summary>
        /// Para las acciones dobles, determina cuál es su acción pasiva complementaria
        /// </summary>
        /// <param name="charAction">CharacterAction correspondiente al personaje seleccionado y a esta acción</param>
        public abstract void AssignSecondaryAction(CharacterAction charAction);

        /// <summary>
        /// Cuando el personaje llega al lugar donde se realiza la acción, a veces tiene que mirar a un punto predefinido
        /// </summary>
        /// <param name="charAction">CharacterAction correspondiente al personaje seleccionado y a esta acción</param>
        public abstract void LookAtSomething(CharacterAction charAction);
    }
}
