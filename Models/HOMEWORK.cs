//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FallowUP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOMEWORK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOMEWORK()
        {
            this.STUDENTHOMEWORK = new HashSet<STUDENTHOMEWORK>();
        }
    
        public int HOMEWORKID { get; set; }
        public int CLASSID { get; set; }
        public int TEACHERID { get; set; }
        public string HOMEWORKNAME { get; set; }
        public Nullable<System.DateTime> STARTDATE { get; set; }
        public Nullable<System.DateTime> FINISHDATE { get; set; }
        public bool ACTIVE { get; set; }
        public string TEACHERNAME { get; set; }
        public string TEACHERSURNAME { get; set; }
        public string CLASSNAME { get; set; }
        public string HOMEWORK1 { get; set; }
    
        public virtual TEACHERCLASS TEACHERCLASS { get; set; }
        public virtual USERS USERS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENTHOMEWORK> STUDENTHOMEWORK { get; set; }
    }
}