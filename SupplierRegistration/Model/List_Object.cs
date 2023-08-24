using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupplierRegistration.Model
{
    public class List_Object
    {
        public string App_ID { get; set; }
        public string V_Name { get; set; }
        public string V_PIC { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string EmpID { get; internal set; }
        public string UpdateBy { get; internal set; }
        public string UpdateHidden { get; internal set; }

    }
}