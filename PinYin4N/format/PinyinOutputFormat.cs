
namespace PinYin4N.format
{

    /**
     * This classes define how the Hanyu Pinyin should be outputted.
     * 
     * <p>
     * The output feature includes:
     * <ul>
     * <li>Output format of character 'ü';
     * <li>Output format of Chinese tones;
     * <li>Cases of letters in outputted string
     * </ul>
     * 
     * <p>
     * Default values of these features are listed below:
     * 
     * <p>
     * HanyuPinyinVCharType := WITH_U_AND_COLON <br>
     * HanyuPinyinCaseType := LOWERCASE <br>
     * HanyuPinyinToneType := WITH_TONE_NUMBER <br>
     * 
     * <p>
     * <b>Some combinations of output format options are meaningless. For example,
     * WITH_TONE_MARK and WITH_U_AND_COLON.</b>
     * 
     * <p>
     * The combination of different output format options are listed below. For
     * example, '吕'
     * 
     * <table border="1">
     * <tr>
     * <th colspan="4"> LOWERCASE </th>
     * </tr>
     * <tr>
     * <th>Combination</th>
     * <th>WITH_U_AND_COLON</th>
     * <th>WITH_V</th>
     * <th>WITH_U_UNICODE</th>
     * </tr>
     * <tr>
     * <th>WITH_TONE_NUMBER</th>
     * <td>lu:3</td>
     * <td>lv3</td>
     * <td>lü3</td>
     * </tr>
     * <tr>
     * <th>WITHOUT_TONE</th>
     * <td>lu:</td>
     * <td>lv</td>
     * <td>lü</td>
     * </tr>
     * <tr>
     * <th>WITH_TONE_MARK</th>
     * <td><font color="red">throw exception</font></td>
     * <td><font color="red">throw exception</font></td>
     * <td>lǚ</td>
     * </tr>
     * </table>
     * 
     * <table border="1">
     * <tr>
     * <th colspan="4"> UPPERCASE </th>
     * </tr>
     * <tr>
     * <th>Combination</th>
     * <th>WITH_U_AND_COLON</th>
     * <th>WITH_V</th>
     * <th>WITH_U_UNICODE</th>
     * </tr>
     * <tr>
     * <th>WITH_TONE_NUMBER</th>
     * <td>LU:3</td>
     * <td>LV3</td>
     * <td>LÜ3</td>
     * </tr>
     * <tr>
     * <th>WITHOUT_TONE</th>
     * <td>LU:</td>
     * <td>LV</td>
     * <td>LÜ</td>
     * </tr>
     * <tr>
     * <th>WITH_TONE_MARK</th>
     * <td><font color="red">throw exception</font></td>
     * <td><font color="red">throw exception</font></td>
     * <td>LǙ</td>
     * </tr>
     * </table>
     * 
     * @see HanyuPinyinVCharType
     * @see HanyuPinyinCaseType
     * @see HanyuPinyinToneType
     * 
     * @author Li Min (xmlerlimin@gmail.com)
     * 
     */
    public sealed class PinyinOutputFormat
    {

        public PinyinOutputFormat()
        {
            RestoreDefault();
        }

        /**
         * Restore default variable values for this class
         * 
         * Default values are listed below:
         * 
         * <p>
         * HanyuPinyinVCharType := WITH_U_AND_COLON <br>
         * HanyuPinyinCaseType := LOWERCASE <br>
         * HanyuPinyinToneType := WITH_TONE_NUMBER <br>
         */
        public void RestoreDefault()
        {
            VCharType = VCharType.WITH_U_AND_COLON;
            CaseType = CaseType.LOWERCASE;
            ToneType = ToneType.WITH_TONE_NUMBER;
        }

        public CaseType CaseType { get; set; }
        public ToneType ToneType { get; set; }
        public VCharType VCharType { get; set; }
    }
}
