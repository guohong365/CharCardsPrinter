using System;

namespace PinYin4N.format.exception
{

    /**
     * An exception class indicates the wrong combination of pinyin output formats
     * 
     * @author Li Min (xmlerlimin@gmail.com)
     * 
     */
    public class BadHanyuPinyinOutputFormatCombination : Exception
    {
        public BadHanyuPinyinOutputFormatCombination(String message)
                : base(message)
        {
        }

    }
}