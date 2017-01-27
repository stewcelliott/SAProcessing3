using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java.util;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.neural.rnn;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.sentiment;
using edu.stanford.nlp.util;
using edu.stanford.nlp.trees;
using SAP.Dtos;
using SAP.Interfaces;
using SAP.Interfaces.Dtos;
using System.Configuration;
using System.Diagnostics;


namespace SAP.Process
{
    /// <summary>
    /// sentiment analyser 
    /// </summary>
    public class Analyser : IAnalyser
    {
        private StanfordCoreNLP Pipeline { get; set; }
        private Properties Properties { get; set; }

        /// <summary>
        /// init stanford CoreNLP properties
        /// </summary>
        /// <param name="sentimentModelPath"></param>// Path to the folder with models extracted from `stanford-corenlp-3.5.1-models.jar`
        public void Init(string sentimentModelPath)
        {
            try
            {
                Trace.WriteLine("Initialising analyser...");

                Properties = new Properties();
                Properties.setProperty("annotators",
                    "tokenize, ssplit, pos, lemma, ner, parse, dcoref, sentiment, parse");
                Properties.setProperty("sutime.binders", "0");
                
                // We should change current directory, so StanfordCoreNLP could find all the model files automatically 
                Trace.WriteLine("Setting properties for Analyser...");
                Directory.SetCurrentDirectory(sentimentModelPath);
                Pipeline = new StanfordCoreNLP(Properties);
                Trace.WriteLine("Initialising analyser complete.");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Error initialising analyser: {0}",ex.Message));
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
                throw;
            }
        }

        /// <summary>
        /// returns an overall sentiment score with the component sentences and individual scores
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public ISentimentDto GetSentiment(ISentimentQueueDto sentimentQueueItem)
        {
            try
            {
                ISentimentDto sentiment = new SentimentDto {  SentimentSentences = new List<ISentimentSentenceDto>(), DateCreated = DateTime.Now };
                decimal sentenceSum = 0;
                decimal scoreSum = 0;

                if (!string.IsNullOrEmpty(sentimentQueueItem.TextForAnalysis))
                {
                    Annotation annotation = Pipeline.process(sentimentQueueItem.TextForAnalysis.ToLower());//convert to lower

                    var sentences = annotation.get(typeof(CoreAnnotations.SentencesAnnotation));

                    var arrayList = sentences as ArrayList;
                    if (arrayList != null)
                        foreach (Annotation sentence in arrayList)
                        {
                            sentenceSum += 1;

                            CoreMap coreMapping = sentence;

                            var tree = (Tree)coreMapping.get(typeof(SentimentCoreAnnotations.AnnotatedTree));


                            var score = RNNCoreAnnotations.getPredictedClass(tree);
                            scoreSum += score;

                            sentiment.SentimentSentences.Add(new SentimentSentenceDto { Text = sentence.ToString(), Score = score, DateCreated = DateTime.Now });
                        }
                    sentiment.AverageScore = scoreSum / sentenceSum; //set average
                }

                return sentiment;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Error analysing sentiment for sentimentId: {0} - error: {1}", sentimentQueueItem.Id, ex.Message));
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
                throw ex;
            }
        }
    }
}
