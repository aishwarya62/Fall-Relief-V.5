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
    
    public partial class tbl_RiskAss_AssQuestion
    {
        public int ID { get; set; }
        public int AssessmentTypeID { get; set; }
        public int QuestionID { get; set; }
        public int QuestionNumber { get; set; }
    
        public virtual tbl_RiskAss_Assessment tbl_RiskAss_Assessment { get; set; }
        public virtual tbl_RiskAss_Questions tbl_RiskAss_Questions { get; set; }
    }
}
