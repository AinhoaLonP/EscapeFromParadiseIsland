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
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Acciones dobles, en las que se necesita un personaje principal que realice la acción y uno secundario
    /// </summary>
    public abstract class BaseDoubleAction : BaseAction
    {
        /// <summary>
        /// Acción pasiva complementaria a ésta 
        /// </summary>
        public abstract Action SecondaryAction { get; set; }
        /// <summary>
        /// Lugar hacia el que el personaje debe mirar al realizar la acción 
        /// </summary>   
        public abstract string WhereToLookAt { get; set; } 
        /// <summary>
        /// CharacterAction correspondiente al personaje seleccionado y a esta acción   
        /// </summary>       
        public CharacterAction charAction;
        /// <summary>
        /// Botón con el que seleccionar la acción
        /// </summary>     
        public Button button; 

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
                    if (MakesAction(attitude))
                    {
                        //Si el personaje está por la labor de realizar la actividad, esperará hasta que se seleccione el personaje con el que va a realizar esa actividad
                        charAction.Status = CharacterActionStatus.WaitingForSecondaryCharacter;
                        charAction.ObjectToGo = GameObject.Find(DestinationName);
                        charAction.Action = this;
                        charAction.CharacterScript.individualBarsHUD.enabled = false;
                        AssignSecondaryAction(charAction);
                    }
                    else
                    {
                        Action action = new RestAction();
                        action.SelectAction();
                    }
                }
            }
        }

        /// <summary>
        /// Cambia el estado de la acción secundaria
        /// </summary>
        /// <param name="charAction">CharacterAction correspondiente al personaje seleccionado y a esta acción</param>
        public override void ChangeSecondaryCharacterStatus(CharacterAction charAction)
        {
            CharacterAction actionOfSecondaryCharacter = CharacterActionsController.GetCharacterActionOfSecondaryCharacter(charAction);
            actionOfSecondaryCharacter.Status = CharacterActionStatus.ReturningFromActionLocation;
        }

        /// <summary>
        /// Determina cuál es su acción pasiva complementaria
        /// </summary>
        /// <param name="charAction">CharacterAction correspondiente al personaje seleccionado y a esta acción</param>
        public override void AssignSecondaryAction(CharacterAction charAction)
        {
            //Desactiva el botón de la acción para que solo se pueda realizar una de cada vez
            button.interactable = false;
            this.charAction = charAction;
            Action action = SecondaryAction;
            charAction.SecondaryAction = action;
        }

        /// <summary>
        /// Finaliza la acción actual y activa de nuevo el botón de la acción
        /// </summary>
        /// <param name="charAction"></param>
        /// <param name="characterMovement"></param>
        public override void FinishAction(CharacterAction charAction, Game.CharacterController characterMovement)
        {
            button.interactable = true;
            this.charAction = charAction;
            characterMovement.animator.SetBool(ActionName, false);
            charAction.Status = CharacterActionStatus.ReturningFromActionLocation;
            characterMovement.HideActionRemainingTime();            
            FinishSpecificAction(characterMovement);
        }

        /// <summary>
        /// El personaje realiza la acción mirando hacia el personaje de la acción secundaria
        /// </summary>
        /// <param name="charAction"></param>
        public override void LookAtSomething(CharacterAction charAction)
        {
            charAction.Character.transform.LookAt(GameObject.Find(WhereToLookAt).transform);
        }

        /// <summary>
        /// Finaliza la acción
        /// </summary>
        public abstract void FinishSpecificAction(Game.CharacterController characterMovement);
    }
}
