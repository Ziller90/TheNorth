namespace RPGCharacterAnims.Actions
{
    public class AttackContext
    {
        public string type;
        public int side;
        public int number;

        public AttackContext(string type, int side, int number = -1)
        {
            this.type = type;
            this.side = side;
            this.number = number;
        }

        public AttackContext(string type, string side, int number = -1)
        {
            this.type = type;
            this.number = number;
            switch (side.ToLower()) {
                case "none":
                    this.side = (int)AttackSide.None;
                    break;
                case "left":
                    this.side = (int)AttackSide.Left;
                    break;
                case "right":
                    this.side = (int)AttackSide.Right;
                    break;
                case "dual":
                    this.side = (int)AttackSide.Dual;
                    break;
            }
        }
    }

    public class Attack : BaseActionHandler<AttackContext>
    {
        public override bool CanStartAction(RPGCharacterController controller)
        {
            return !controller.isRelaxed && !active && !controller.isCasting && controller.canAction;
        }

        public override bool CanEndAction(RPGCharacterController controller)
        {
            return active;
        }

        protected override void _StartAction(RPGCharacterController controller, AttackContext context)
        {
            int attackSide = 0;
            int attackNumber = context.number;
            int weaponNumber = controller.rightWeapon;
            float duration = 0f;

            if (context.side == (int)AttackSide.Right && AnimationData.Is2HandedWeapon(weaponNumber)) { context.side = (int)AttackSide.None; }

            switch (context.side) {
                case (int)AttackSide.None:
                    attackSide = 0;
                    weaponNumber = controller.rightWeapon;
                    break;
                case (int)AttackSide.Left:
                    attackSide = 1;
                    weaponNumber = controller.leftWeapon;
                    break;
                case (int)AttackSide.Right:
                    attackSide = 2;
                    weaponNumber = controller.rightWeapon;
                    break;
                case (int)AttackSide.Dual:
                    attackSide = 3;
                    weaponNumber = controller.rightWeapon;
                    break;
            }

            if (attackNumber == -1) {
                switch (context.type) {
                    case "Attack":
                        attackNumber = AnimationData.RandomAttackNumber(attackSide, weaponNumber);
                        break;
                    case "Kick":
                        attackNumber = AnimationData.RandomKickNumber(attackSide);
                        break;
                    case "Special":
                        attackNumber = 1;
                        break;
                }
            }

            duration = AnimationData.AttackDuration(attackSide, weaponNumber, attackNumber);

            if (!controller.maintainingGround) {
                controller.AirAttack();
                EndAction(controller);
            }
			else if (controller.isMoving) {
                controller.RunningAttack(
                    attackSide,
                    controller.hasLeftWeapon,
                    controller.hasRightWeapon,
                    controller.hasDualWeapons,
                    controller.hasTwoHandedWeapon
                );
                EndAction(controller);
            }
			else if (context.type == "Kick") {
                controller.AttackKick(attackNumber);
                EndAction(controller);
            }
			else if (context.type == "Attack") {
                controller.Attack(
                    attackNumber,
                    attackSide,
                    controller.leftWeapon,
                    controller.rightWeapon,
                    duration
                );
                EndAction(controller);
            }
			else if (context.type == "Special") {
                controller.isSpecial = true;
                controller.StartSpecial(attackNumber);
            }
        }

        protected override void _EndAction(RPGCharacterController controller)
        {
            if (controller.isSpecial) {
                controller.isSpecial = false;
                controller.EndSpecial();
            }
        }
    }
}