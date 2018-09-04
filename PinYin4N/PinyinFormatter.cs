using PinYin4N.format;
using PinYin4N.format.exception;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System;

namespace PinYin4N
{

    public class PinyinFormatter
    {
        /**
         * @param pinyinStr
         *            unformatted Hanyu Pinyin string
         * @param outputFormat
         *            given format of Hanyu Pinyin
         * @return formatted Hanyu Pinyin string
         * @throws BadHanyuPinyinOutputFormatCombination
         */
        internal static string FormatPinyin(string pinyinStr, PinyinOutputFormat outputFormat)
        {
            if ((ToneType.WITH_TONE_MARK == outputFormat.ToneType)
                    && ((VCharType.WITH_V == outputFormat.VCharType) ||
                    (VCharType.WITH_U_AND_COLON == outputFormat.VCharType)))
            {
                throw new BadHanyuPinyinOutputFormatCombination("tone marks cannot be added to v or u:");
            }

            if (ToneType.WITHOUT_TONE == outputFormat.ToneType)
            {
                pinyinStr = pinyinStr.Replace("1", "").Replace("2", "").Replace("3", "").Replace("4", "").Replace("5", "");
            }
            else if (ToneType.WITH_TONE_MARK == outputFormat.ToneType)
            {
                pinyinStr = pinyinStr.Replace("u:", "v");
                pinyinStr = convertToneNumber2ToneMark(pinyinStr);
            }

            if (VCharType.WITH_V == outputFormat.VCharType)
            {
                pinyinStr = pinyinStr.Replace("u:", "v");
            }
            else if (VCharType.WITH_U_UNICODE == outputFormat.VCharType)
            {
                pinyinStr = pinyinStr.Replace("u:", "ü");
            }

            if (CaseType.UPPERCASE == outputFormat.CaseType)
            {
                pinyinStr = pinyinStr.ToUpper();
            }
            return pinyinStr;
        }

        /**
         * Convert tone numbers to tone marks using Unicode <br/><br/>
         * 
         * <b>Algorithm for determining location of tone mark</b><br/>
         * 
         * A simple algorithm for determining the vowel on which the tone mark
         * appears is as follows:<br/>
         * 
         * <ol>
         * <li>First, look for an "a" or an "e". If either vowel appears, it takes
         * the tone mark. There are no possible pinyin syllables that contain both
         * an "a" and an "e".
         * 
         * <li>If there is no "a" or "e", look for an "ou". If "ou" appears, then
         * the "o" takes the tone mark.
         * 
         * <li>If none of the above cases hold, then the last vowel in the syllable
         * takes the tone mark.
         * 
         * </ol>
         * 
         * @param pinyinStr
         *            the ascii represention with tone numbers
         * @return the unicode represention with tone marks
         */
        private static string convertToneNumber2ToneMark(string pinyinStr)
        {
            string lowerCasePinyinStr = pinyinStr.ToLower();
            Regex regex1 = new Regex("[a-z]*[1-5]?");
            string regex2 = "^[a-z]*[1-5]$";
            if (regex1.IsMatch(lowerCasePinyinStr))
            {
                char defautlCharValue = '$';
                int defautlIndexValue = -1;

                char unmarkedVowel = defautlCharValue;
                int indexOfUnmarkedVowel = defautlIndexValue;

                char charA = 'a';
                char charE = 'e';
                string ouStr = "ou";
                string allUnmarkedVowelStr = "aeiouv";
                string allMarkedVowelStr = "āáăàaēéĕèeīíĭìiōóŏòoūúŭùuǖǘǚǜü";

                if (Regex.IsMatch(lowerCasePinyinStr, regex2))
                {
                    int tuneNumber = lowerCasePinyinStr[lowerCasePinyinStr.Length - 1] - '0';

                    int indexOfA = lowerCasePinyinStr.IndexOf(charA);
                    int indexOfE = lowerCasePinyinStr.IndexOf(charE);
                    int ouIndex = lowerCasePinyinStr.IndexOf(ouStr);

                    if (-1 != indexOfA)
                    {
                        indexOfUnmarkedVowel = indexOfA;
                        unmarkedVowel = charA;
                    }
                    else if (-1 != indexOfE)
                    {
                        indexOfUnmarkedVowel = indexOfE;
                        unmarkedVowel = charE;
                    }
                    else if (-1 != ouIndex)
                    {
                        indexOfUnmarkedVowel = ouIndex;
                        unmarkedVowel = ouStr[0];
                    }
                    else
                    {
                        for (int i = lowerCasePinyinStr.Length - 1; i >= 0; i--)
                        {
                            if (Regex.IsMatch(new string(lowerCasePinyinStr[i], 1), "[" + allUnmarkedVowelStr + "]"))
                            {
                                indexOfUnmarkedVowel = i;
                                unmarkedVowel = lowerCasePinyinStr[i];
                                break;
                            }
                        }
                    }

                    if ((defautlCharValue != unmarkedVowel)
                            && (defautlIndexValue != indexOfUnmarkedVowel))
                    {
                        int rowIndex = allUnmarkedVowelStr.IndexOf(unmarkedVowel);
                        int columnIndex = tuneNumber - 1;

                        int vowelLocation = rowIndex * 5 + columnIndex;

                        char markedVowel = allMarkedVowelStr[vowelLocation];

                        StringBuilder resultBuffer = new StringBuilder();

                        resultBuffer.Append(lowerCasePinyinStr.Substring(0, indexOfUnmarkedVowel).Replace("v", "ü"));
                        resultBuffer.Append(markedVowel);
                        resultBuffer.Append(lowerCasePinyinStr.Substring(indexOfUnmarkedVowel + 1, lowerCasePinyinStr.Length - indexOfUnmarkedVowel - 2).Replace("v", "ü"));

                        return resultBuffer.ToString();

                    }
                    else  // error happens in the procedure of locating vowel
                    {
                        return lowerCasePinyinStr;
                    }
                }
                else // input string has no any tune number
                {
                    // only replace v with ü (umlat) character
                    return lowerCasePinyinStr.Replace("v", "ü");
                }
            }
            else // bad format
            {
                return lowerCasePinyinStr;
            }
        }
    }
}