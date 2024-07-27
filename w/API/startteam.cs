

namespace w.API
{
    //抄 https://github.com/joker-119/CustomRoles 的東西 不知道怎麼改

    using System;

    [Flags]
    public enum StartTeam
    {
        ClassD = 1,
        Scientist = 2,
        Guard = 4,
        Ntf = 8,
        Chaos = 16,
        Scp = 32,
        Revived = 64,
        Escape = 128,
        Other = 256,
    }
}
