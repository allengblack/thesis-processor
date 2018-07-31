using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ThesisProcessor.Models.ThesesViewModels
{
    public class ThesisCreateViewModel
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string References { get; set; }
        public string Author { get; set; }
        public string Supervisor { get; set; }
        public string FileName { get; set; }

        [Display(Name="Date of Submission")]
        public DateTime DateCreated { get; set; }

        public int Day { get; set; }

        [Display(Name = "Date of Submission")]
        public DateTime DateOfThesis { get; set; }
        public IFormFile Thesis { get; set; }
    }
}

//public enum Month
//{
//    JANUARY = 1, FEBRUARY = 2, MARCH = 3,
//    APRIL = 4, MAY = 5, JUNE = 6,
//    JULY = 7, AUGUST = 8, SEPTEMBER = 9,
//    OCTOBER = 10, NOVEMBER = 11, DECEMBER = 12
//}
