using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace RoadStatus
{
    [DataContract(Name = "Road")]
    public class RoadCorridor
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "statusSeverity")]
        public string StatusSeverity { get; set; }

        [DataMember(Name = "statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }

        //[
        //    {
        //        "$type": "Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities",
        //        "id": "a2",
        //        "displayName": "A2",
        //        "statusSeverity": "Good",
        //        "statusSeverityDescription": "No Exceptional Delays",
        //        "bounds": "[[-0.0857,51.44091],[0.17118,51.49438]]",
        //        "envelope": "[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]",
        //        "url": "/Road/a2"
        //    }
        //]
    }
}
