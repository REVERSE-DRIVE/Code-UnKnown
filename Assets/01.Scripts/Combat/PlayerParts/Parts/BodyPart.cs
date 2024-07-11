namespace PlayerPartsManage
{
    public class BodyPart : PlayerPart
    {
        public BodyPart(Player owner) : base(owner)
        {
        }

        public override void UseSkill()
        {
            throw new System.NotImplementedException();
        }
    }
}