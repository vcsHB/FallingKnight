using Agents.Players;
using Managers.Jsonmanager;
using UnityEngine;
namespace Managers
{

    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [Header("Stat Inc Setting")]
        [SerializeField] private float _healthIncValue;
        [SerializeField] private float _damageIncValue;
        [SerializeField] private float _moveSpeedValue;
        [SerializeField] private float _defenseIncValue;
        [SerializeField] private float _attackSpeedIncValue;

        private void Awake()
        {
            JsonManager.instance.Load();

        }

        private void Start()
        {
            Load();
        }

        private void Load()
        {
            Data data = JsonManager.instance.gameData;
            int healthLevel = data.healthLevel;
            int damageLevel = data.attackDamageLevel;
            int moveSpeedLevel = data.moveSpeedLevel;
            int defenseLevel = data.defenseLevel;
            int attackSpeedLevel = data.attackSpeedLevel;


            _player.PlayerStatus.health.AddModifier(healthLevel * _healthIncValue);
            _player.PlayerStatus.health.AddModifier(damageLevel * _damageIncValue);
            _player.PlayerStatus.health.AddModifier(moveSpeedLevel * _moveSpeedValue);
            _player.PlayerStatus.health.AddModifier(defenseLevel * _defenseIncValue);
            _player.PlayerStatus.health.AddModifier(attackSpeedLevel * _attackSpeedIncValue);
        }
    }
}