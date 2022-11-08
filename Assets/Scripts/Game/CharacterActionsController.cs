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

using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// Clase que maneja el conjunto de los CharacterActions
    /// </summary>
    public static class CharacterActionsController
    {
        /// <summary>
        ///Lista de objetos CharacterAction  
        /// </summary>
        public static List<CharacterAction> charActionPairs = new List<CharacterAction>();

        /// <summary>
        /// Deselecciona el último CharacterAction seleccionado
        /// </summary>
        /// <returns></returns>
        public static bool UnselectLastCharacter()
        {
            CharacterAction pair = GetLastSelectedCharacterAction();
            if (pair == null)
                return false;
            pair.LastSelected = false;
            return true;
        }

        /// <summary>
        /// Obtiene el último CharacterAction seleccionado
        /// </summary>
        /// <returns></returns>
        public static CharacterAction GetLastSelectedCharacterAction()
        {
            return charActionPairs.Where(c => c.LastSelected == true).LastOrDefault();
        }

        /// <summary>
        /// Obtiene el CharacterAction correspondiente al personaje especificado
        /// </summary>
        /// <param name="transform">Transform del personaje</param>
        /// <returns></returns>
        public static CharacterAction GetCharacterActionOfCharacter(Transform transform)
        {
            CharacterAction lastCharacterAction = charActionPairs.FirstOrDefault(c => c.Character.transform == transform);
            return lastCharacterAction;
        }

        /// <summary>
        /// Obtiene el CharacterAction principal del personaje secundario del CharacterAction pasado como parámetro
        /// </summary>
        /// <param name="charAction"></param>
        /// <returns></returns>
        public static CharacterAction GetCharacterActionOfSecondaryCharacter(CharacterAction charAction)
        {
            return charActionPairs.Where(c => c.Character != null && charAction.SecondaryCharacter != null && c.Character.transform == charAction.SecondaryCharacter.transform).LastOrDefault();
        }

        /// <summary>
        /// Obtiene el CharacterAction donde el personaje secundario es el mismo que el personaje principal del CharacterAction pasado como parámetro
        /// </summary>
        /// <param name="charAction"></param>
        /// <returns></returns>
        public static CharacterAction GetPrimaryCharacterActionOfSecondaryCharacterAction(CharacterAction charAction)
        {
            return charActionPairs.Where(c => charAction.Character != null && c.SecondaryCharacter != null && c.SecondaryCharacter.transform == charAction.Character.transform).LastOrDefault();
        }

        /// <summary>
        /// Obtiene el CharacterAction que corresponde a una acción doble y aún no tiene asignado el personaje que va a realizar la acción pasiva
        /// </summary>
        /// <returns></returns>
        public static CharacterAction GetLastWaitingAction()
        {
            return charActionPairs.Where(c => c.Status == CharacterActionStatus.WaitingForSecondaryCharacter).LastOrDefault();
        }

        /// <summary>
        /// Desasigna cualquier acción del personaje pasado como parámetro, y marca el CharacterAction como deseleccionado
        /// </summary>
        /// <param name="transform"></param>
        public static void FinishAction(Transform transform)
        {
            List<CharacterAction> pairs = charActionPairs.Where(p => p.Character.transform == transform).ToList();
            foreach (CharacterAction pair in pairs)
            {
                pair.LastSelected = false;
                pair.Action = null;
            }
        }

        /// <summary>
        /// Devuelve true siempre que quede algún personaje vivo
        /// </summary>
        /// <returns></returns>
        public static bool IsThereAnyActiveCharacter()
        {
            GameObject characters = GameObject.Find("Characters");
            List<Transform> children = new List<Transform>();
            foreach (Transform child in characters.transform)
            {                
                if (child.gameObject.activeSelf && GetCharacterActionOfCharacter(child).Status != CharacterActionStatus.Dead)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Devuelve el número de personajes que quedan vivos
        /// </summary>
        /// <returns></returns>
        public static int GetNumberOfActiveCharacters()
        {
            GameObject characters = GameObject.Find("Characters");
            List<Transform> children = new List<Transform>();
            int count = 0;
            foreach (Transform child in characters.transform)
            {
                if (child.gameObject.activeSelf && GetCharacterActionOfCharacter(child).Status != CharacterActionStatus.Dead)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Devuelve el número total de personajes activos
        /// </summary>
        /// <returns></returns>
        public static int GetNumberOfCharacters()
        {
            GameObject characters = GameObject.Find("Characters");
            List<Transform> children = new List<Transform>();
            int count = 0;
            foreach (Transform child in characters.transform)
            {
                if (child.gameObject.activeSelf)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Devuelve un personaje aleatorio
        /// </summary>
        /// <returns></returns>
        public static CharacterAction SelectRandomCharacterAction()
        {
            try
            {
                GameObject characters = GameObject.Find("Characters");
                List<Transform> children = new List<Transform>();
                foreach (Transform child in characters.transform)
                {
                    if (child.gameObject.activeSelf && GetCharacterActionOfCharacter(child).Status != CharacterActionStatus.Dead)
                        children.Add(child);
                }
                int listSize = children.Count;
                int random = StaticRandom.Instance.Next(listSize);
                if (listSize <= 0)
                {
                    return null;
                }
                Transform selectedChild = children.ElementAt(random);
                CharacterAction selectedCharAction = GetCharacterActionOfCharacter(selectedChild);

                return selectedCharAction;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
