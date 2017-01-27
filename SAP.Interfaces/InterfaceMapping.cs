using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ESA.DataModel;
using ESA.Interfaces.Dtos;

namespace ESA.Interfaces
{
    public class InterfaceMapping
    {
        public static void Setup()
        {
            Mapper.CreateMap<sentiment_batch, ISentimentBatchDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(p => p.id))
                .ForMember(dest => dest.BatchLimit, src => src.MapFrom(p => p.batch_limit))
                .ForMember(dest => dest.BatchSize, src => src.MapFrom(p => p.batch_size))
                .ForMember(dest => dest.DateStart, src => src.MapFrom(p => p.date_start))
                .ForMember(dest => dest.DateFinish, src => src.MapFrom(p => p.date_finish));

            Mapper.CreateMap<sentiment, ISentimentDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(p => p.id))
                .ForMember(dest => dest.DischargeId, src => src.MapFrom(p => p.discharge_id))
                .ForMember(dest => dest.DischargeResponseId, src => src.MapFrom(p => p.discharge_response_id))
                .ForMember(dest => dest.DateCreated, src => src.MapFrom(p => p.date_created))
                .ForMember(dest => dest.SentimentSentences, src => src.MapFrom(p => p.sentiment_sentences));

            Mapper.CreateMap<sentiment_sentences, ISentimentSentenceDto>();
        }
    }
}
