using System;
using System.Xml;
using System.Reflection;
namespace PinYin4N {

    /**
     * Contains the resource supporting translations among different Chinese
     * Romanization systems
     * 
     * @author Li Min (xmlerlimin@gmail.com)
     * 
     */
    class PinyinRomanizationResource
    {
        /**
         * A DOM model contains variable pinyin presentations
         */

       public XmlDocument PinyinMappingDoc { get; set; }

        /**
         * Private constructor as part of the singleton pattern.
         */
        private PinyinRomanizationResource()
        {
            initializeResource();
        }

        /**
         * Initialiez a DOM contains variable PinYin representations
         */
        private void initializeResource()
        {
            try
            {
                const string mappingFileName = "pinyin_mapping";

                // Parse file to DOM Document
                PinyinMappingDoc = new XmlDocument();
                PinyinMappingDoc.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream(mappingFileName));

            } catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }

        /**
         * Singleton factory method.
         * 
         * @return the one and only MySingleton.
         */
        internal static PinyinRomanizationResource getInstance()
        {
            return PinyinRomanizationSystemResourceHolder.theInstance;
        }

        /**
         * Singleton implementation helper.
         */
        private static class PinyinRomanizationSystemResourceHolder
        {
            public static readonly PinyinRomanizationResource theInstance = new PinyinRomanizationResource();
        }
    }
}