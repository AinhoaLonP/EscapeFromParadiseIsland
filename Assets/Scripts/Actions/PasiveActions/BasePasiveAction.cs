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
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Acciones secundarias de las acciones dobles
    /// </summary>
    public abstract class BasePasiveAction : Action
    {
        /// <summary>
        /// Nombre del objeto al que deben dirigirse para realizar una acción   
        /// </summary>
        public abstract string DestinationName { get; set; }
        /// <summary>
        /// Nombre de la acción en el Animator  
        /// </summary>     
        public abstract string ActionName { get; set; }
        /// <summary>
        /// Duración de la acción 
        /// </summary>       
        public int ActionDurationMilliseconds { get; set; } = 20000;
        /// <summary>
        /// Lugar hacia el que el personaje debe mirar
        /// </summary>     
        public abstract string WhereToLookAt { get; set; }
        /// <summary>
        /// Nombre de la acción para mostrar en los mensajes al usuario
        /// </summary>
        public string ActionNameForMessages { get => ""; set { } }

        /// <summary>
        /// La acción se selecciona automáticamente
        /// </summary>
        public virtual void SelectAction()
        {
            CharacterAction charAction = CharacterActionsController.GetLastSelectedCharacterAction();

            //Comprueba que se ha clicado en algún personaje, y este está esperando por una acción
            if (charAction != null)
            {
                //Comprueba que el personaje no tiene ninguna acción asignada
                if (charAction.Action == null && charAction.Status == CharacterActionStatus.Free)
                {
                    //Se quita la condición de la actitud: las acciones pasivas se hacen siempre
                    Game.CharacterController characterMovement = charAction.CharacterScript;
                    double attitude = characterMovement.characterStats.Where(a => a.Action.Name == this.GetType().Name).FirstOrDefault().Attitude;
                    charAction.ObjectToGo = GameObject.Find(DestinationName);
                    charAction.Action = this;
                    charAction.Status = CharacterActionStatus.MovingToActionLocation;
                }
            }
        }

        /// <summary>
        /// El personaje comienza a realizar la acción
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
        /// Realiza la acción durante el tiempo fijado
        /// </summary>
        /// <param name="characterMovement"></param>
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
            }
            else
            {
                characterMovement.animator.SetBool(ActionName, true);
            }
        }

        /// <summary>
        /// Cuando la acción acaba, el personaje vuelve al centro
        /// </summary>
        /// <param name="charAction"></param>
        /// <param name="characterMovement"></param>
        public virtual void FinishAction(CharacterAction charAction, Game.CharacterController characterMovement)
        {
            characterMovement.animator.SetBool(ActionName, false);
            charAction.Status = CharacterActionStatus.ReturningFromActionLocation;
            characterMovement.HideActionRemainingTime();
        }
        
        /// <summary>
        /// Mira hacia el personaje de la acción primaria correspondiente, o hacia adelante en el caso de estar en la playa
        /// </summary>
        /// <param name="charAction"></param>
        public void LookAtSomething(CharacterAction charAction)
        {
            charAction.Character.transform.LookAt(GameObject.Find(WhereToLookAt).transform);
        }
    }
}
