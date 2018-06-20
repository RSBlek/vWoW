using System;
using System.Collections.Generic;
using System.Text;
using vWoW.Data.Enums;

namespace vWoW.Data
{
    public class RealmInfo
    {
        public RealmType RealmType { get; private set; }
        public bool IsLocked { get; private set; }
        public RealmFlags RealmFlags { get; private set; }
        public String RealmName { get; private set; }
        public RealmEndpoint RealmEndpoint { get; private set; }
        public float PopulationLevel { get; private set; }
        public byte CharacterCount { get; private set; }
        public byte RealmTimeZone { get; private set; }
        public byte RealmID { get; private set; }
        public RealmBuildInformation RealmBuildInformation { get; private set; }

        public RealmInfo(RealmType realmType, bool isLocked, RealmFlags realmFlags, String realmName, String realmEndpoint, float populationLevel, byte characterCount, byte realmTimeZone, byte realmID, RealmBuildInformation realmBuildInformation)
        {
            this.RealmType = realmType;
            this.IsLocked = isLocked;
            this.RealmFlags = realmFlags;
            this.RealmName = realmName;
            this.RealmEndpoint = new RealmEndpoint(realmEndpoint);
            this.PopulationLevel = populationLevel;
            this.CharacterCount = characterCount;
            this.RealmTimeZone = realmTimeZone;
            this.RealmID = realmID;
            this.RealmBuildInformation = realmBuildInformation;
        }
    }
}
