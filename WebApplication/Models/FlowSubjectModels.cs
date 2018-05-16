using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class FlowSubject
    {
        public string FlowId { get; set; }
        public string TeacherId { get; set; }

        public string Teacher2Id { get; set; }
        public string Teacher3Id { get; set; }
        public string SubjectId { get; set; }
        public string SemesterId { get; set; }
    }
}