using System;

namespace ThesisProcessor.Models
{
    public class Thesis
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Abstract { get; set; }
        public string References { get; set; }
        public string Supervisor { get; set; }
        public string FileName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Approved { get; set; }
        public string RejectReason { get; set; }

        public string UploaderId { get; set; }
        public ApplicationUser Uploader { get; set; }
    }
}