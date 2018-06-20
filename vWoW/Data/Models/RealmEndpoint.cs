using System;
using System.Collections.Generic;
using System.Text;

namespace vWoW.Data
{
    public class RealmEndpoint
    {
        public string RealmEndpointInformation { get; private set; }
        public string realmHost { get; private set; }
        public ushort realmPort { get; private set; }

        public RealmEndpoint(string realmendpointinformation)
        {
            this.RealmEndpointInformation = realmendpointinformation;
            string[] splittedRealmEndpointInformation = realmendpointinformation.Split(':');
            this.realmHost = splittedRealmEndpointInformation[0];
            this.realmPort = (ushort)(int.Parse(splittedRealmEndpointInformation[1]));
        }
    }
}
