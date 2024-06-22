namespace WeaponManage
{
    public abstract class Sword : Weapon
    {
        public SwordsWeaponInfoSO swordInfo { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            swordInfo =  weaponInfo as SwordsWeaponInfoSO;
            
        }
    }
}