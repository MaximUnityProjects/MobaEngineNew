using Assets.Game.Abilitys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Главный компонент, тут хранится вся информация, ссылки на способности, состояния
/// </summary>
public sealed class Unit : MonoBehaviour {


    public List<Ability> abilities;

    public void AddAbility(Ability ability) {
        if (!abilities.Contains(ability)) abilities.Add(ability);
    }

    public void RemoveAbility(Ability ability) {
        if (abilities.Contains(ability)) abilities.Remove(ability);
    }




    public void AddState() {

    }
}
