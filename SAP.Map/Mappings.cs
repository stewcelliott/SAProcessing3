using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SAP.DataModel;
using SAP.Interfaces.Dtos;
using AutoMapper.Mappers;

namespace SAP.Map
{
    /// <summary>
    /// automapping for dtos to database objects and vice versa
    /// </summary>
    public static class Mappings
    {
        /// <summary>
        /// setups up map configurations for AutoMapper
        /// </summary>
        public static void Setup()
        {
            var useless = new ListSourceMapper();//call something in AutoMapper.Net4.dll so that it is included in the publish of API 

            #region "data model to dto"
            Mapper.CreateMap<sentiment_batch, ISentimentBatchDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(p => p.id))
                .ForMember(dest => dest.BatchLimit, src => src.MapFrom(p => p.batch_limit))
                .ForMember(dest => dest.BatchSize, src => src.MapFrom(p => p.batch_size))
                .ForMember(dest => dest.DateStart, src => src.MapFrom(p => p.date_start))
                .ForMember(dest => dest.DateFinish, src => src.MapFrom(p => p.date_finish));

            Mapper.CreateMap<sentiment_queue, ISentimentQueueDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(p => p.id))
                .ForMember(dest => dest.TextForAnalysis, src => src.MapFrom(p => p.text_for_analysis))
                .ForMember(dest => dest.DateCreated, src => src.MapFrom(p => p.date_created))
                .ForMember(dest => dest.BatchId, src => src.MapFrom(p => p.batch_id))
                .ForMember(dest => dest.Processed, src => src.MapFrom(p => p.processed))
                .ForMember(dest => dest.DateProcessed, src => src.MapFrom(p => p.date_processed))
                .ForMember(dest => dest.Error, src => src.MapFrom(p => p.error))
                .ForMember(dest => dest.SentimentQueueErrors, src => src.MapFrom(p => p.sentiment_queue_error));

            Mapper.CreateMap<sentiment_queue_error, ISentimentQueueErrorDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(p => p.id))
                .ForMember(dest => dest.SentimentQueueId, src => src.MapFrom(p => p.sentiment_queue_id))
                .ForMember(dest => dest.Message, src => src.MapFrom(p => p.message))
                .ForMember(dest => dest.StackTrace, src => src.MapFrom(p => p.stacktrace))
                .ForMember(dest => dest.DateCreated, src => src.MapFrom(p => p.date_created));

            Mapper.CreateMap<sentiment, ISentimentDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(p => p.id))
                .ForMember(dest => dest.DateCreated, src => src.MapFrom(p => p.date_created))
                .ForMember(dest => dest.AverageScore, src => src.MapFrom(p => p.average_score))
                .ForMember(dest => dest.SentimentSentences, src => src.MapFrom(p => p.sentiment_sentences));

            Mapper.CreateMap<sentiment_sentences, ISentimentSentenceDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(p => p.id))
                .ForMember(dest => dest.SentimentId, src => src.MapFrom(p => p.sentiment_id))
                .ForMember(dest => dest.Text, src => src.MapFrom(p => p.text))
                .ForMember(dest => dest.Score, src => src.MapFrom(p => p.score))
                .ForMember(dest => dest.DateCreated, src => src.MapFrom(p => p.date_created)); 

            #endregion

            #region "dto to datamodel"
            Mapper.CreateMap<ISentimentDto, sentiment>()
                .ForMember(dest => dest.id, src => src.MapFrom(p => p.Id))
                .ForMember(dest => dest.date_created, src => src.MapFrom(p => p.DateCreated))
                .ForMember(dest => dest.average_score, src => src.MapFrom(p => p.AverageScore))
                .ForMember(dest => dest.sentiment_sentences, src => src.MapFrom(p => p.SentimentSentences));

            Mapper.CreateMap<ISentimentSentenceDto, sentiment_sentences>()
                .ForMember(dest => dest.id, src => src.MapFrom(p => p.Id))
                .ForMember(dest => dest.sentiment_id, src => src.MapFrom(p => p.SentimentId))
                .ForMember(dest => dest.text, src => src.MapFrom(p => p.Text))
                .ForMember(dest => dest.score, src => src.MapFrom(p => p.Score))
                .ForMember(dest => dest.date_created, src => src.MapFrom(p => p.DateCreated));

            Mapper.CreateMap<ISentimentQueueErrorDto, sentiment_queue_error>()
                .ForMember(dest => dest.id, src => src.MapFrom(p => p.Id))
                .ForMember(dest => dest.sentiment_queue_id, src => src.MapFrom(p => p.SentimentQueueId))
                .ForMember(dest => dest.message, src => src.MapFrom(p => p.Message))
                .ForMember(dest => dest.stacktrace, src => src.MapFrom(p => p.StackTrace))
                .ForMember(dest => dest.date_created, src => src.MapFrom(p => p.DateCreated));

            #endregion

        }
    }
}
