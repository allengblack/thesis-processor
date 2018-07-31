using AutoMapper;
using ThesisProcessor.Models.ThesesViewModels;

namespace ThesisProcessor.Models
{
    public class ThesisProcessorProfile : Profile
    {
        public ThesisProcessorProfile()
        {
            CreateMap<ThesisCreateViewModel, Thesis>();

            CreateMap<ThesisSaveViewModel, Thesis>()
                .ForMember(t => t.DateCreated, opt => opt.Ignore())
                .ForMember(t => t.Uploader, opt => opt.Ignore());
        }
    }
}
