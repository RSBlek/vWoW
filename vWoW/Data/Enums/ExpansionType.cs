using System;
using System.Collections.Generic;
using System.Text;

namespace vWoW.Data.Enums
{
    public enum ExpansionType : byte //uint8 version1;
    {
        /// No Expansion
        None = 0,

        /// 1.x.x
        Vanilla = 1,

        /// 2.x.x
        TheBurningCrusade = 2,

        /// 3.x.x
        WrathOfTheLichKing = 3,

        /// 4.x.x
        Cataclysm = 4,

        /// 5.x.x
        MistsOfPandaria = 5,

        /// 6.x.x
        WarlordsOfDraenor = 6,

        /// 7.x.x
        Legion = 7
    }
}
