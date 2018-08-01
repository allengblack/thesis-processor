using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ThesisProcessor.Models.ThesesViewModels
{
    public class ThesisSaveViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string References { get; set; }
        public string Author { get; set; }
        public string Supervisor { get; set; }
        public string FileName { get; set; }
        public bool Approved { get; set; }

        [Display(Name = "Reason for Rejecting")]
        public string RejectReason { get; set; }
    }
}
