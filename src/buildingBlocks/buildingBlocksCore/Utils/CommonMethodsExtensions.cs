using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Linq.Expressions;

namespace buildingBlocksCore.Utils
{
    public static class CommonMethodsExtensions


    {
        public static bool EValidoEnum<T>(this int value) where T : Enum => CommonMethods.EValidoEnum<T>(value);


        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var body = Expression.AndAlso(left.Body, right.Body);
            return Expression.Lambda<Func<T, bool>>(body, left.Parameters);
        }
        #region " String "

        public static string GetQueryString<T>(this T obj, bool usingEncode = false, IEnumerable<string> propsExcluded = null) where T : class => CommonMethods.GetQueryString(obj, usingEncode, propsExcluded);

        public static bool IsGuid(this string numero) => CommonMethods.IsGuid(numero);

        /// <summary>
        /// Formatar uma string CNPJ
        /// </summary>
        /// <param name="CNPJ">string CNPJ sem formatacao</param>
        /// <returns>string CNPJ formatada</returns>
        /// <example>Recebe '99999999999999' Devolve '99.999.999/9999-99'</example>

        public static string FormatCNPJ(this string CNPJ) => CommonMethods.FormatCNPJ(CNPJ);

        /// <summary>
        /// Formatar uma string CPF
        /// </summary>
        /// <param name="CPF">string CPF sem formatacao</param>
        /// <returns>string CPF formatada</returns>
        /// <example>Recebe '99999999999' Devolve '999.999.999-99'</example>

        public static string FormatCPF(this string CPF) => CommonMethods.FormatCPF(CPF);

        public static bool IsValidEmail(this string email) => CommonMethods.IsValidEmail(email);

        public static bool IsCnpj(this string cnpj) => CommonMethods.IsCnpj(cnpj);

        public static bool IsCpf(this string cpf) => CommonMethods.IsCpf(cpf);

        public static string FormatRG(this string texto) => CommonMethods.FormatRG(texto);

        public static string OnlyNumbers(this string numeros) => CommonMethods.OnlyNumbers(numeros);

        #endregion
        public static string SerializeXml<T>(this T ObjectToSerialize) => CommonMethods.SerializeXml(ObjectToSerialize);

        public static T DeserializeXml<T>(this string input) where T : class => CommonMethods.DeserializeXml<T>(input);
        public static string GetQueryString<T>(this T obj, bool encodeValue) => CommonMethods.GetQueryString(obj, encodeValue);
        public static XmlDocument ToXmlDocument(this XDocument xDocument) => CommonMethods.ToXmlDocument(xDocument);
        public static XDocument ToXDocument(this XmlDocument xmlDocument) => CommonMethods.ToXDocument(xmlDocument);
    }
}
