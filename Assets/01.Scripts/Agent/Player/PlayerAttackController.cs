using UnityEngine;
using WeaponManage;

public class PlayerAttackController : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Weapon _currentWeapon;


    private void Start()
    {
        // 무기 회전에 대한 함수를 구독해준다
        _player.PlayerInputCompo.OnMovementEvent += _currentWeapon.HandleRotateWeapon;
    }

    public void Initialize(Player player)
    {
        _player = player;
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        _player.PlayerInputCompo.OnMovementEvent -= _currentWeapon.HandleRotateWeapon;
        _currentWeapon = newWeapon;
        _player.PlayerInputCompo.OnMovementEvent += _currentWeapon.HandleRotateWeapon;
    }

    public void HandleAttack()
    {
        _currentWeapon.Attack();      
    }


}