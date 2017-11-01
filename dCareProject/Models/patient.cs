using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dCareProject.Models {
    public class patient {
        public string section { get; set; }
        public string doctor { get; set; }
        public DateTime date { get; set; }
        public int ID    { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string number { get; set; }
        public DateTime birthday { get; set; }
        public string address { get; set; }
        public int phone { get; set; }
        public int addressID { get; set; }


    }
}