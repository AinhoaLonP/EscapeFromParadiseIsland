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

using System.Collections.Generic;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Contiene un listado de acciones en las que el personaje puede mejorar o empeorar su habilidad o motivación
    /// </summary>
    public static class ActionsManager
    {
        /// <summary>
        ///Listado de acciones en las que un personaje puede aumentar o disminuir su habilidad o motivación 
        /// </summary>
        public static List<Action> actions = new List<Action>
        {
            new HealAction(),
            new PsychologistTreatAction(),
            new TeachAction(),
            new BuildRefugeAction(),
            new BuildBoatAction(),
            new PickFoodAction(),
            new PickResourcesAction(),
            new PickWaterAction()
        };
        /// <summary>
        ///Listado de acciones en las que un personaje puede aumentar o disminuir su habilidad o motivación 
        /// </summary>
        public static List<Action> Actions { get => actions; set { } }
    }
}
