using System;
using ItemManage;
using UnityEngine;
using WeaponManage;

public class PlayerAttackController : MonoBehaviour
{
    private Player _player;
    private Transform _weaponHandleTrm;
    [SerializeField] private WeaponInfoSO _currentWeaponInfo;
    [SerializeField] private Weapon _currentWeapon;


    private void Awake()
    {
        _weaponHandleTrm = transform.Find("WeaponHandle");
        
        if (_currentWeapon == null)
        {
            _currentWeapon = Instantiate(_currentWeaponInfo.WeaponPrefab, _weaponHandleTrm);
        }
        
        
    }
    

    private void Start()
    {
        // 무기 회전에 대한 함수를 구독해준다
        WeaponInit();
    }

    public void Initialize(Player player)
    {
        _player = player;
    }

    public void ChangeWeapon(WeaponInfoSO newWeaponSO)
    {
        _player.PlayerInputCompo.OnMovementEvent -= _currentWeapon.HandleRotateWeapon;
        WeaponInfoSO prevWeapon = _currentWeaponInfo;
        _currentWeaponInfo = newWeaponSO;
        Destroy(_currentWeapon.gameObject);
        _currentWeapon = Instantiate(_currentWeaponInfo.WeaponPrefab, _weaponHandleTrm);
        WeaponInit();
        ItemDropManager.Instance.DropItem(ItemType.Weapon, prevWeapon.id, transform.position);

        
    }

    private void WeaponInit()
    {
        _currentWeapon.Initialize(_player);
        _player.PlayerInputCompo.OnMovementEvent += _currentWeapon.HandleRotateWeapon;
        _player.PlayerInputCompo.controlButtons.OnAttackEvent += HandleAttack;
    }

    public void HandleAttack()
    {
        if (_currentWeapon == null)
        {
            Debug.LogWarning("Weapon Is NULL");
            return;
        }
        _currentWeapon.Attack();      
    }


}