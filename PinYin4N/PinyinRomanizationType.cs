/**
 * This file is part of pinyin4j (http://sourceforge.net/projects/pinyin4j/) 
 * and distributed under GNU GENERAL PUBLIC LICENSE (GPL).
 * 
 * pinyin4j is free software; you can redistribute it and/or modify 
 * it under the terms of the GNU General Public License as published by 
 * the Free Software Foundation; either version 2 of the License, or 
 * (at your option) any later version. 
 * 
 * pinyin4j is distributed in the hope that it will be useful, 
 * but WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
 * GNU General Public License for more details. 
 * 
 * You should have received a copy of the GNU General Public License 
 * along with pinyin4j.
 */

/**
 * 
 */
namespace PinYin4N {

    /**
     * The class describes variable Chinese Pinyin Romanization System
     * 
     * @author Li Min (xmlerlimin@gmail.com)
     * 
     */
    public class PinyinRomanizationType
    {
        /**
         * Hanyu Pinyin system
         */
        public static readonly PinyinRomanizationType HANYU_PINYIN = new PinyinRomanizationType("Hanyu");

        /**
         * Wade-Giles Pinyin system
         */
        public static readonly PinyinRomanizationType WADEGILES_PINYIN = new PinyinRomanizationType("Wade");

        /**
         * Mandarin Phonetic Symbols 2 (MPS2) Pinyin system
         */
        public static readonly PinyinRomanizationType MPS2_PINYIN = new PinyinRomanizationType("MPSII");

        /**
         * Yale Pinyin system
         */
        public static readonly PinyinRomanizationType YALE_PINYIN = new PinyinRomanizationType("Yale");

        /**
         * Tongyong Pinyin system
         */
        public static readonly PinyinRomanizationType TONGYONG_PINYIN = new PinyinRomanizationType("Tongyong");

        /**
         * Gwoyeu Romatzyh system
         */
        public static readonly PinyinRomanizationType GWOYEU_ROMATZYH = new PinyinRomanizationType("Gwoyeu");

        /**
         * Constructor
         */
        protected PinyinRomanizationType(string tagName)
        {
            TagName=tagName;
        }

        public string TagName { get; set; }
    }
}