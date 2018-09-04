
namespace PinYin4N
{
    class TextHelper
    {

        /**
         * @param pinyinWithToneNumber
         * @return Hanyu Pinyin string without tone number
         */
        internal static string ExtractToneNumber(string hanyuPinyinWithToneNumber)
        {
            return hanyuPinyinWithToneNumber.Substring(hanyuPinyinWithToneNumber.Length - 1);
        }

        /**
         * @param PinyinWithToneNumber
         * @return only tone number
         */
        internal static string ExtractPinyinString(string hanyuPinyinWithToneNumber)
        {
            return hanyuPinyinWithToneNumber.Substring(0, hanyuPinyinWithToneNumber.Length - 1);
        }

    }

}
