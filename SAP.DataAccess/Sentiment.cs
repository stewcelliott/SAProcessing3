using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.DataModel;
using System.Linq.Expressions;
using AutoMapper;
using SAP.Dtos;
using SAP.Interfaces;
using SAP.Interfaces.Dtos;
using SAP.Map;

namespace SAP.DataAccess
{
    /// <summary>
    /// data access for sentiments and adjustments
    /// </summary>
    public static class Sentiment
    {        
        /// <summary>
        /// saves a sentiment and its sentences
        /// </summary>
        /// <param name="sentimentDto"></param>
        public static void SaveSentiment(ISentimentDto sentimentDto)
        {
            using (var db = new SentimentEntities())
            {
                var sentiment = Mapper.Map(sentimentDto, new sentiment());
                db.sentiments.Add(sentiment);
                db.SaveChanges();
            }
        }   
    }
}
