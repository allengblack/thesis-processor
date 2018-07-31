namespace ThesisProcessor.Models.ThesesViewModels
{
    public class ThesisRejectViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string RejectReason { get; set; }

        public Thesis ToModel()
        {
            return new Thesis
            {
                RejectReason = this.RejectReason
            };
        }
    }
}
