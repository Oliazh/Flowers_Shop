//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Flowers_web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment_Methods
    {
        public int PM_ID { get; set; }
        public int Cust_ID { get; set; }
        public decimal card_number { get; set; }
        public System.DateTime date_from { get; set; }
        public Nullable<decimal> CVV { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
