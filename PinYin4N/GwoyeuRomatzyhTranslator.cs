using System;
using System.Xml;
namespace PinYin4N {


    /**
     * A class contains logic that translates from Hanyu Pinyin to Gwoyeu Romatzyh
     * 
     * @author Li Min (xmlerlimin@gmail.com)
     * 
     */
    class GwoyeuRomatzyhTranslator
    {
        /**
         * @param hanyuPinyinStr
         *            Given unformatted Hanyu Pinyin with tone number
         * @return Corresponding Gwoyeu Romatzyh; null if no mapping is found.
         */
        public static string convertHanyuPinyinToGwoyeuRomatzyh(string hanyuPinyinStr)
        {
            string pinyinString = TextHelper.ExtractPinyinString(hanyuPinyinStr);
            string toneNumberStr = TextHelper.ExtractToneNumber(hanyuPinyinStr);

            // return value
            string gwoyeuStr = null;
            try
            {
                // find the node of source Pinyin system
                string xpathQuery1 = "//"
                        + PinyinRomanizationType.HANYU_PINYIN.TagName
                        + "[text()='" + pinyinString + "']";

                XmlDocument pinyinToGwoyeuMappingDoc = GwoyeuRomatzyhResource.GetInstance().PinyinToGwoyeuMappingDoc;

                XmlNode hanyuNode = pinyinToGwoyeuMappingDoc.SelectSingleNode(xpathQuery1);

                if (null != hanyuNode)
                {
                    // find the node of target Pinyin system
                    string xpathQuery2 = "../"
                            + PinyinRomanizationType.GWOYEU_ROMATZYH.TagName
                            + tones[int.Parse(toneNumberStr) - 1]
                            + "/text()";
                    string targetPinyinStrWithoutToneNumber = hanyuNode.SelectSingleNode(xpathQuery2).Value;

                    gwoyeuStr = targetPinyinStrWithoutToneNumber;
                }
            } catch (Exception e)
            {
                throw e;
            }

            return gwoyeuStr;
        }

        /**
         * The postfixs to distinguish different tone of Gwoyeu Romatzyh
         * 
         * <i>Should be removed if new xPath parser supporting tag name with number.</i>
         */
        static private string[] tones = new string[] { "_I", "_II", "_III", "_IV", "_V" };
    }
}