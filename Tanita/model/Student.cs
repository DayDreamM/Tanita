using System;

namespace Tanita.model
{
    class Student
    {
        public string studentId { get; set; }  //直接用数校ID
        public string studentName { get; set; }
        public int sex { get; set; }
        public string sex_string { get; set; }
        public string birthday { get; set; }
        public string studentEduID { get; set; }
        public string age { get; set; }
        public string height { get; set; }
        public string imagePath { get; set; }
        public string isCheck { get; set; }
    }
}
