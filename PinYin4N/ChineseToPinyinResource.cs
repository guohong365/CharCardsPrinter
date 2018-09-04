using System.Collections.Generic;
using PinYin4N.Properties;
using System;
namespace PinYin4N
{
    class ChineseToPinyinResource
    {
        public const string LEFT_BRACKET = "(";
        public const string RIGHT_BRACKET = ")";
        public const char COMMA = ',';

        private Dictionary<string, string> PinyinTable { get; set; }

        /**
         * Private constructor as part of the singleton pattern.
         */
        private ChineseToPinyinResource()
        {
            initializeResource();
        }

        /**
         * Initialize a hash-table contains <Unicode, HanyuPinyin> pairs
         */
        private void initializeResource()
        {
            PinyinTable = new Dictionary<string, string>();
            string content = Resources.unicode_to_hanyu_pinyin;
            string[] pairs = content.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < pairs.Length - 2; i += 2)
            {
                PinyinTable.Add(pairs[i], pairs[i + 1]);
            }
        }

        /**
         * Get the unformatted Hanyu Pinyin representations of the given Chinese
         * character in array format.
         * 
         * @param ch
         *            given Chinese character in Unicode
         * @return The Hanyu Pinyin strings of the given Chinese character in array
         *         format; return null if there is no corresponding Pinyin string.
         */
        public string[] GetPinyinStringArray(char ch)
        {
            string pinyinRecord = getPinyinRecordFromChar(ch);

            if (null != pinyinRecord)
            {
                int indexOfLeftBracket = pinyinRecord.IndexOf(LEFT_BRACKET);
                int indexOfRightBracket = pinyinRecord.LastIndexOf(RIGHT_BRACKET);

                string stripedString = pinyinRecord.Substring(indexOfLeftBracket + 1, indexOfRightBracket - indexOfLeftBracket -1);

                return stripedString.Split(COMMA);

            } else
                return null; // no record found or mal-formatted record
        }

        /**
         * @param record
         *            given record string of Hanyu Pinyin
         * @return return true if record is not null and record is not "none0" and
         *         record is not mal-formatted, else return false
         */
        private bool isValidRecord(string record)
        {
            const string noneStr = "(none0)";

            if ((null != record) && !record.Equals(noneStr)
                    && record.StartsWith(LEFT_BRACKET)
                    && record.EndsWith(RIGHT_BRACKET))
            {
                return true;
            } else
                return false;
        }

        /**
         * @param ch
         *            given Chinese character in Unicode
         * @return corresponding Hanyu Pinyin Record in Properties file; null if no
         *         record found
         */
        private string getPinyinRecordFromChar(char ch)
        {
            int codePointOfChar = ch;

            string codepointHexStr = string.Format("{0:X}",codePointOfChar);

            // fetch from hashtable
            string foundRecord = PinyinTable[codepointHexStr];

            return isValidRecord(foundRecord) ? foundRecord : null;
        }

        /**
         * Singleton factory method.
         * 
         * @return the one and only MySingleton.
         */
        internal static ChineseToPinyinResource Instance
        {
            get { return ChineseToPinyinResourceHolder.TheInstance; }
        }

        /**
         * Singleton implementation helper.
         */
        static class ChineseToPinyinResourceHolder
        {
           public static readonly ChineseToPinyinResource TheInstance = new ChineseToPinyinResource();
        }
    }
}