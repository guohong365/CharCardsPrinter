using System;
using System.Xml;
namespace PinYin4N
{

    /**
     * Contains the logic translating among different Chinese Romanization systems
     * 
     * @author Li Min (xmlerlimin@gmail.com)
     * 
     */
    class PinyinRomanizationTranslator
    {
        /**
         * convert the given unformatted Pinyin string from source Romanization
         * system to target Romanization system
         * 
         * @param sourcePinyinStr
         *            the given unformatted Pinyin string
         * @param sourcePinyinSystem
         *            the Romanization system which is currently used by the given
         *            unformatted Pinyin string
         * @param targetPinyinSystem
         *            the Romanization system that should be converted to
         * @return unformatted Pinyin string in target Romanization system; null if
         *         error happens
         */
        public static string convertRomanizationSystem(string sourcePinyinStr,
                PinyinRomanizationType sourcePinyinSystem,
                PinyinRomanizationType targetPinyinSystem)
        {
            string pinyinString = TextHelper.ExtractPinyinString(sourcePinyinStr);
            string toneNumberStr = TextHelper.ExtractToneNumber(sourcePinyinStr);

            // return value
            string targetPinyinStr = null;
            try
            {
                // find the node of source Pinyin system
                string xpathQuery1 = "//" + sourcePinyinSystem.TagName
                        + "[text()='" + pinyinString + "']";

                XmlDocument pinyinMappingDoc = PinyinRomanizationResource.getInstance().PinyinMappingDoc;

                XmlNode hanyuNode = pinyinMappingDoc.SelectSingleNode(xpathQuery1);

                if (null != hanyuNode)
                {
                    // find the node of target Pinyin system
                    string xpathQuery2 = "../" + targetPinyinSystem.TagName
                            + "/text()";
                    string targetPinyinStrWithoutToneNumber = hanyuNode.SelectSingleNode(xpathQuery2).Value;

                    targetPinyinStr = targetPinyinStrWithoutToneNumber
                            + toneNumberStr;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

            return targetPinyinStr;
        }
    }
}