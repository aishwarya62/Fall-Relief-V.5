//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RiskAssessment
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_RiskAss_Session
    {
        public System.Guid sessionID { get; set; }
        public System.DateTime timeStamp { get; set; }
        public int AssessmentTypeID { get; set; }
        public string sessionToken { get; set; }
        public int AssessmentNo { get; set; }
    
        public virtual tbl_RiskAss_Assessment tbl_RiskAss_Assessment { get; set; }
    }
}