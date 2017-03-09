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

using System.Text.RegularExpressions;

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
