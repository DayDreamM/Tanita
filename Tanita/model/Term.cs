using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanita.model
{
    class Term
    {
        public string termId { get; set; }
        public string termName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string isCurTerm { get; set; }
    }
}
