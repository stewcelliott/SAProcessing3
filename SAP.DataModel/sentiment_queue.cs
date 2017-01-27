//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAP.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class sentiment_queue
    {
        public sentiment_queue()
        {
            this.sentiment_queue_error = new HashSet<sentiment_queue_error>();
        }
    
        public int id { get; set; }
        public string text_for_analysis { get; set; }
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<int> batch_id { get; set; }
        public bool processed { get; set; }
        public Nullable<System.DateTime> date_processed { get; set; }
        public Nullable<bool> error { get; set; }
    
        public virtual sentiment_batch sentiment_batch { get; set; }
        public virtual ICollection<sentiment_queue_error> sentiment_queue_error { get; set; }
    }
}