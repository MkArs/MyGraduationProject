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

    //public enum CoinType : byte
    //{
    //    penny = 0,
    //    nickel = 1,
    //    dime = 2,
    //    lucky = 3
    //}

    public enum TearEffectType : byte 
    { 
        none = 0,
        poison = 1,
        fire = 2,
        explosionOnContact = 3,
        beam = 4,
        homing = 5,
        spectral = 6,
        explosionOnTime = 7,
        acid = 8,
        fear = 9,
        charm = 10,
        booger = 11
    }

    public enum PlayerEffectType : byte
    {
        none = 0,
        flight = 1,
        vampirism = 2,
        stompy = 3,
        invincibilityOverTime = 4,
        invincibilityOnHit = 5,
        charmOnHit = 6,
        midasTouch = 7,
        dmgUpOnHit = 8
    }

    public enum RoomType : byte
    {
        usual = 0,
        treasure = 1,
        shop = 2,
        boss = 3,
        starting = 4
    }

}