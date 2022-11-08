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
using System.Reflection;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// Objeto que engloba el controlador de un personaje, una acción y una actitud y aptitud de ese personaje para esa acción
    /// </summary>
    public class CharacterStats
    {
        /// <summary>
        /// Script del personaje
        /// </summary>
        public CharacterController CharacterMovement { get; set; }
        /// <summary>
        /// Acción
        /// </summary>
        public Type Action { get; set; }
        /// <summary>
        /// Actitud del personaje hacia esa acción
        /// </summary>
        public double Attitude { get; set; }
        /// <summary>
        /// Habilidad del personaje en esa acción
        /// </summary>
        public double Aptitude { get; set; }
    }

    /// <summary>
    /// Clase encargada de asignar a cada personaje su puntuación de habilidad y actitud para cada acción
    /// </summary>
    public class CharacterStatsManager
    {
        /// <summary>
        /// Devuelve una puntuación aleatoria entre 0.2 y 1
        /// </summary>
        /// <returns></returns>
        public double GetRandomAttitudeValue()
        {
            return Math.Round(StaticRandom.Instance.NextDouble() * 0.8 + 0.2, 1);
        }

        /// <summary>
        /// Devuelve una puntuación aleatoria entre 0 y 0.7
        /// </summary>
        /// <returns></returns>
        public double GetRandomAptitudeValue()
        {
            return Math.Round(StaticRandom.Instance.NextDouble() * 0.7, 1);
        }

        /// <summary>
        /// Recorre la lista de acciones y a cada una de ellas le asigna un valor de habilidad y uno de actitud
        /// </summary>
        /// <param name="characterMovement">Script del personaje al que corresponderán esas asignaciones</param>
        /// <returns></returns>
        public List<CharacterStats> AssignRandomAttitudeAndAptitude(CharacterController characterMovement)
        {
            List<Type> actions = GetActions();
            List<CharacterStats> characterActionAttitudeAptitudes = new List<CharacterStats>();
            foreach (Type action in actions)
            {
                characterActionAttitudeAptitudes.Add(new CharacterStats
                {
                    CharacterMovement = characterMovement,
                    Action = action,
                    Attitude = GetRandomAttitudeValue(),
                    Aptitude = GetRandomAptitudeValue()
                });
            }
            return characterActionAttitudeAptitudes;
        }

        /// <summary>
        /// Obtiene la lista de acciones del juego
        /// </summary>
        /// <returns></returns>
        private List<Type> GetActions()
        {
            List<Type> types = new List<Type>();
            foreach (Type myType in Assembly.GetExecutingAssembly().GetTypes()
                 .Where(t => t.GetInterfaces().Contains(typeof(Actions.Action)) && !t.GetTypeInfo().IsAbstract))
            {
                types.Add(myType);
            }
            return types;
        }
    }
}
