namespace KWKY.Common
{
    /// <summary>
    /// 系统常量
    /// </summary>
    public class ConstData
    {
        #region  系统变量

        public const string Invariant = "System.Data.SqlClient"; // for Microsoft SQL Server
        public const string MysqlInvariant = "MySql.Data.MySqlClient"; // for mysql
        public const string ClusterId = "bjkwjq-kwky-net";
        public const string ServiceId = "Orleans-kwky-net";
        public const string ClusterIdDev = "bjkwjq-kwky-net-dev";
        public const string ServiceIdDev = "Orleans-kwky-net-dev";
        public const string AdoNetGrainStorageName = "OrleansMySqlStorage";

        /// <summary>
        /// 跨域配置名
        /// </summary>
        public const string CorsPolicyName = "CorsPolicyName";

        /// <summary>
        /// 认证字段名称
        /// </summary>
        public const string JwtTokenName = "token";

        /// <summary>
        /// token Claim中加入的存放Id 的字段名
        /// </summary>
        public const string ClaimTypes_UserId = "UserId";


        #endregion


        #region Format

        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// yyyy-MM-dd:HH:mm
        /// </summary>
        public const string DateTimeFormatNoSecond = "yyyy-MM-dd HH:mm";

        /// <summary>
        /// yyyy-MM-dd:HH:mm:ss
        /// </summary>
        public const string DateTimeFormatWithSecond = "yyyy-MM-dd HH:mm:ss";

        #endregion


        #region RegularExpresss  正则表达式

        public const string MobileRegex = @"^[1]+[3,9]+\d{9}"; //精准版 @"^(13[0-9]|14[579]|15[0-3,5-9]|16[6]|17[0135678]|18[0-9]|19[89])\d{8}$")
        public const string ChineseIDRegex = @"(^\d{18}$)|(^\d{17}(\d|X|x)$)|(^\d{15}$)";
        #endregion


        #region Char    

        /// <summary>
        /// _
        /// </summary>
        public const char UnderLine = '_';

        /// <summary>
        /// ^
        /// </summary>
        public const char Caret = '^';

        /// <summary>
        /// &
        /// </summary>
        public const char And = '&';

        /// <summary>
        /// |
        /// </summary>
        public const char Or = '|';

        /// <summary>
        /// :
        /// </summary>
        public const char Colon = ':';
        /// <summary>
        /// ' '
        /// </summary>
        public const char EmptyChar = ' ';
        /// <summary>
        /// ','
        /// </summary>
        public const char CommaChar = ',';
        #endregion

    }
}
