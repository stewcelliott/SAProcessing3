using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SAP.Process
{
    /// <summary>
    /// internally accessible helpers for Engine
    /// </summary>
    public static class InternalHelpers
    {
        internal static string FormatSentenceForSplit(this string sentence)
        {
            var formattedSentence = sentence;

            //replace parenthesis
            formattedSentence = Regex.Replace(formattedSentence, @"(?<![:;'])(\()(?![:;'])|(?<![:;'])(\))(?![:;'])", "", RegexOptions.IgnoreCase);

            //space out punctuation omitting apostrophe
            MatchCollection mx = Regex.Matches(formattedSentence, @"[\p{P}-[:;'()]]");
            var shifter = 0;//increment this on loop to adjust index position
            foreach (Match m in mx)
            {
              formattedSentence=  formattedSentence.Insert(m.Index+shifter, " ");
              shifter += 1;
            }

            //remove leading and trailing spaces
            formattedSentence = formattedSentence.Trim();

            //remove excess spacing and new lines/tabs
            formattedSentence = Regex.Replace(formattedSentence, @"\s+", " ", RegexOptions.Multiline);

            return formattedSentence;
        }

       
    }
}
