namespace IsaacClone
{
    public enum ShootingChaserAttackDependencyType : byte
    {
        ranged = 0,
        timed = 1
    }

    public enum TearSourceType : byte
    {
        player = 0,
        enemy = 1
    }

    public enum PickUpType : byte
    {
        coin = 0,
        key = 1,
        bomb = 2,
        heart = 3
    }
}