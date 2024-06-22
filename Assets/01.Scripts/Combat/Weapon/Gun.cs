namespace WeaponManage
{
    public abstract class Gun : Weapon
    {
        public GunsWeaponInfoSO gunInfo { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            gunInfo =  weaponInfo as GunsWeaponInfoSO;
            
        }
    }
}