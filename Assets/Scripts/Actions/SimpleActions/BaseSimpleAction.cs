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
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Son las acciones simples, que sólo necesitan a un personaje para hacerlas
    /// </summary>
    public abstract class BaseSimpleAction : BaseAction
    {
        /// <summary>
        /// Cantidad en la que incrementar las unidades del inventario
        /// </summary>
        public abstract int IncrementQuantity { get; set; }

        bool AnimatorIsPlaying(CharacterAction charAction)
        {
            return charAction.Character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length >
                   charAction.Character.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        /// <summary>
        /// Una vez escogido el personaje, al clicar en la acción ésta se selecciona
        /// </summary>
        public override void SelectAction()
        {
            CharacterAction charAction = CharacterActionsController.GetLastSelectedCharacterAction();

            //Comprueba que se ha clicado en algún personaje, y este está esperando por una acción
            if (charAction != null)
            {
                //Comprueba que el personaje no tiene ninguna acción asignada
                if (charAction.Action == null && charAction.Status == CharacterActionStatus.Free)
                {
                    Game.CharacterController characterMovement = charAction.CharacterScript;
                    double attitude = characterMovement.characterStats.Where(a => a.Action.Name == this.GetType().Name).FirstOrDefault().Attitude;
                    //Según el resultado de MakesAction(attitude), se decide si el personaje va a obedecer al usuario. Si es así, realizará la acción. Si no, se irá a la playa.
                    if (MakesAction(attitude))
                    {
                        charAction.ObjectToGo = GameObject.Find(DestinationName);
                        charAction.Action = this;
                        charAction.Status = CharacterActionStatus.MovingToActionLocation;
                        charAction.CharacterScript.individualBarsHUD.enabled = false;
                    }
                    else
                    {
                        //StartCoroutine(ExecuteAfterTime(3.4f));
                        Action action = new RestAction();
                        action.SelectAction();
                    }
                }
            }
        }

        /// <summary>
        /// Terminar acción e incrementar en el inventario el número de unidades del recurso (o el porcentaje) definido en "IncrementQuantity"
        /// </summary>
        /// <param name="charAction">CharacterAction correspondiente al personaje seleccionado y a esta acción</param>
        /// <param name="characterMovement">Script del personaje seleccionado</param>
        public override void FinishAction(CharacterAction charAction, Game.CharacterController characterMovement)
        {
            double aptitude = characterMovement.characterStats.Where(a => a.Action.Name == this.GetType().Name).FirstOrDefault().Aptitude;
            characterMovement.animator.SetBool(ActionName, false);
            charAction.Status = CharacterActionStatus.ReturningFromActionLocation;
            Inventory.Increment(this, Convert.ToInt32(IncrementQuantity * aptitude));
            characterMovement.HideActionRemainingTime();
            if (aptitude == 0)
            {
                MakeAMess(characterMovement);
            }
        }

        /// <summary>En caso de las acciones simples, no se hace nada</summary>
        public override void ChangeSecondaryCharacterStatus(CharacterAction charAction) { }

        /// <summary>En caso de las acciones simples, no se hace nada</summary>
        public override void AssignSecondaryAction(CharacterAction charAction) { }

        /// <summary>En caso de las acciones simples, no se hace nada</summary>
        public override void LookAtSomething(CharacterAction charAction) { }
    }
}
