using UnityEngine;

namespace Assets.Game.Abilitys {
    /// <summary>
    /// Способность, может прикреплятся к обьекту как компонент
    /// </summary>
    abstract public class Ability : MonoBehaviour {

        [HideInInspector] public AbilityTarget abilityTarget = AbilityTarget.Self;
        [HideInInspector] public AbilityMode mode = AbilityMode.Passive;
        [HideInInspector] public AbilityNumber number = AbilityNumber.Hidden;
        public delegate void MethodContainer();
        private Unit unit;

        /// <summary>
        /// Использовать для подписки на срабатывание способности. Вызывать только в методе Cast.
        /// </summary>
        //public event MethodContainer OnCast;

        /// <summary>
        /// Выполнение способности
        /// </summary>
        public abstract void Cast();


        /// <summary>
        /// Вызывать в OnEnable
        /// </summary>
        protected void Enable() {
            unit = GetComponent<Unit>();
            unit.AddAbility(this);
        }

        /// <summary>
        /// Вызывать в OnDisable
        /// </summary>
        protected void Disable() {
            unit.RemoveAbility(this);
        }

    }
    public enum AbilityNumber { Hidden, First, Second, Third, Fourth, Fifth }
    public enum AbilityMode { Active, Passive }
    public enum AbilityTarget { Self, Ground, EnemyHero, AllyHero, EnemyBuilding, AllyBuilding, EnemyOrdinary, AllyOrdinary, ForestOrdinary }
}
