/*
    Copyright 2016 Healthcare Communications UK Ltd
 
    This file is part of HCSentimentAnalysisProcessor.

    HCSentimentAnalysisProcessor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    HCSentimentAnalysisProcessor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with HCSentimentAnalysisProcessor.  If not, see <http://www.gnu.org/licenses/>.

 */

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SAP.DataModel;
using SAP.Dtos;
using SAP.Interfaces.Dtos;

namespace SAP.DataAccess
{
    /// <summary>
    /// data access for queue processing
    /// </summary>
    public static class Queue
    {

        //TODO: refactor with unit of work so context is shared between data access classes
        //private static SentimentEntities _db = new SentimentEntities();
        //for now just wrap with using statments to ensure data is not stale
        //i.e. using (var db = new SentimentEntities()) 
        
        /// <summary>
        /// creates a new sentiment batch and returns it
        /// </summary>
        /// <param name="batchLimit"></param>
        /// <param name="batchSize"></param>
        /// <param name="dateStart"></param>
        /// <returns></returns>
        public static ISentimentBatchDto StartBatch(int batchLimit, int batchSize, DateTime dateStart)//TODO: refactor params to Interface
        {
            using (var db = new SentimentEntities())
            {
                var newBatch = new sentiment_batch
                {
                    batch_limit = batchLimit,
                    batch_size = batchSize,
                    date_start = dateStart
                };
                db.sentiment_batch.Add(newBatch);
                db.SaveChanges();
                ISentimentBatchDto dto = new SentimentBatchDto();
                return Mapper.Map(newBatch, dto);
            }
        }

        /// <summary>
        /// updates the finish date of the batch passed in 
        /// </summary>
        /// <param name="sentimentBatch"></param>
        public static void FinishBatch(ISentimentBatchDto sentimentBatch)
        {
            using (var db = new SentimentEntities())
            {
                db.sentiment_batch.Find(sentimentBatch.Id).date_finish = sentimentBatch.DateFinish;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// returns a list of sentiment queue items not yet processed at batch size specified
        /// </summary>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public static List<ISentimentQueueDto> GetSentimentQueueForProcessing(int batchSize, bool retryFailed =false)
        {
            using (var db = new SentimentEntities())
            {
                var queue = new List<sentiment_queue>();

                queue = retryFailed ? db.sentiment_queue.OrderBy(x => x.id).Where(x => x.processed == false || x.error == true).Take(batchSize).ToList() : db.sentiment_queue.OrderBy(x => x.id).Where(x => x.processed == false).Take(batchSize).ToList();
                var dto = new List<ISentimentQueueDto>();
                return Mapper.Map(queue, dto);
            }
        }

        /// <summary>
        /// saves the processing outcome of a sentiment queue item
        /// </summary>
        /// <param name="sentimentQueue"></param>
        public static void SaveSentimentQueueProcessingOutcome(ISentimentQueueDto sentimentQueue)
        {
            using (var db = new SentimentEntities())
            {
                var sentimentQueueToUpdate = db.sentiment_queue.Find(sentimentQueue.Id);
                sentimentQueueToUpdate.batch_id = sentimentQueue.BatchId;
                sentimentQueueToUpdate.processed = sentimentQueue.Processed;
                sentimentQueueToUpdate.date_processed = sentimentQueue.DateProcessed;
                sentimentQueueToUpdate.error = sentimentQueue.Error;
                sentimentQueueToUpdate.sentiment_queue_error = Mapper.Map(sentimentQueue.SentimentQueueErrors,
                    new List<sentiment_queue_error>());

                db.SaveChanges();
            }
        }       
    }
}
