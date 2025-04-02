using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "UnitAbilitiesConfig", menuName = "ScriptableObjects/Configs/UnitAbilitiesConfig")]
    [Serializable]
    public class UnitAbilitiesConfig : ScriptableObject
    {
        [SerializeField] private List<ActiveAbilityConfig> _activeAbilities;
        public List<IActiveAbility> ActiveAbilities => new(_activeAbilities);

        [SerializeField] private List<PassiveAbilityConfig> _passiveAbilities;
        public List<IPassiveAbility> PassiveAbilities => new(_passiveAbilities);
    }
}
