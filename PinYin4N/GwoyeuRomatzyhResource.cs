


using System.Xml;
using System.IO;
using System.Reflection;
using System;
namespace PinYin4N {

    /**
     * A class contains resource that translates from Hanyu Pinyin to Gwoyeu
     * Romatzyh
     * 
     * @author Li Min (xmlerlimin@gmail.com)
     * 
     */
    class GwoyeuRomatzyhResource
    {
        /**
         * A DOM model contains Hanyu Pinyin to Gwoyeu Romatzyh mapping
         */
        public XmlDocument PinyinToGwoyeuMappingDoc { get; set; }

        /**
         * Private constructor as part of the singleton pattern.
         */
        private GwoyeuRomatzyhResource()
        {
            initializeResource();
        }

        /**
         * Initialiez a DOM contains Hanyu Pinyin to Gwoyeu mapping
         */
        private void initializeResource()
        {
            try
            {
                const string mappingFileName = "pinyin_gwoyeu_mapping";

                // Parse file to DOM Document
                PinyinToGwoyeuMappingDoc = new XmlDocument();
                PinyinToGwoyeuMappingDoc.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream(mappingFileName));

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
        internal static GwoyeuRomatzyhResource GetInstance()
        {
            return GwoyeuRomatzyhSystemResourceHolder.theInstance;
        }

        /**
         * Singleton implementation helper.
         */
        private static class GwoyeuRomatzyhSystemResourceHolder
        {
            internal static readonly GwoyeuRomatzyhResource theInstance = new GwoyeuRomatzyhResource();
        }
    }
}