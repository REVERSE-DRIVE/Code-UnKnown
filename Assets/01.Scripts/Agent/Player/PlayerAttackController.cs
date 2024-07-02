using System;
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

    public void ChangeWeapon(Weapon newWeapon)
    {
        _player.PlayerInputCompo.OnMovementEvent -= _currentWeapon.HandleRotateWeapon;
        _currentWeapon = newWeapon;
        WeaponInit();
        
    }

    private void WeaponInit()
    {
        _currentWeapon.Initialize(_player);
        _player.PlayerInputCompo.OnMovementEvent += _currentWeapon.HandleRotateWeapon;
    }

    public void HandleAttack()
    {
        _currentWeapon.Attack();      
    }


}