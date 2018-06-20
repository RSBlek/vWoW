using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Data.Enums;

namespace vWoW.Data
{
    public class RealmBuildInformation
    {
        public ExpansionType ExpansionType { get; private set; }
        public byte MajorVersion { get; private set; }
        public byte MinorVersion { get; private set; }
        public short BuildNumber { get; private set; }

        public RealmBuildInformation(ExpansionType expansionType, byte majorVersion, byte minorVersion, short buildNumber)
        {
            this.ExpansionType = expansionType;
            this.MajorVersion = majorVersion;
            this.MinorVersion = minorVersion;
            this.BuildNumber = buildNumber;
        }
    }
}
