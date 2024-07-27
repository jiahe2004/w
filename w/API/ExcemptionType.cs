namespace w.API
{
    //抄 https://github.com/joker-119/CustomRoles 的東西 不知道怎麼改

    using System;

    [Flags]
    public enum ExemptionType
    {
        RoundStart,
        Respawn,
        Revive,
    }
}