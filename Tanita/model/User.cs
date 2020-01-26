using System;

namespace Tanita.model
{
    [Serializable]
    class User
    {
        public  string ID{ get; set; }
        public  string username { get; set; }
        public  string password { get; set; }
        public  string status { get; set; }
    }
}
