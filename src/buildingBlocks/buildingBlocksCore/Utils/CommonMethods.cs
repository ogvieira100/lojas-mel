using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.Extensions.Logging;
using Serilog;

namespace buildingBlocksCore.Utils
{

    public enum TipoLog
    {
        Informacao = 1,
        Erro = 2,
        Alerta = 3
    }

    public enum EstadoProcesso
    {
        Inicio = 1,
        Processando = 2,
        Finalizando = 3,
        EntradaDados = 4
    }

    public enum Aplicacao
    {
        Customer = 1,
        Invoice = 2,
        Order = 3,
        User = 4,
        Supplier = 5,
        Product = 6
    }

    public enum Processo
    { 
    
        InserirUsuario = 1,
        AtualizarUsuario = 2,
        DeletarUsuario = 3,
        InserirProduto = 4,
        AtualizarProduto = 5,
        InserirFornecedor = 6,
        AtualizarFornecedor = 7
    
    }

    public class LogClass
    {
        public string Msg { get; set; }
        public Guid ProcessoId { get; set; }
        public TipoLog TipoLog { get; set; }
        public Aplicacao Aplicacao { get; set; }
        public EstadoProcesso EstadoProcesso { get; set; }
        public Processo Processo { get; set; }
        public IDictionary<string, string> Chaves { get; set; }

        public bool EObjetoJson { get; set; }

        public LogClass()
        {
            Chaves = new Dictionary<string, string>();
        }
    }

    public class CommonMethods
    {

        #region " Logger "

        public static void Logar(LogClass logClass, Microsoft.Extensions.Logging.ILogger logger)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<object> pars = new List<object>();
                ObterInfoLog(logClass, sb, pars);
                switch (logClass.TipoLog)
                {
                    case TipoLog.Informacao:
                        Log.Information(sb.ToString(), pars.ToArray());
                        break;
                    case TipoLog.Erro:
                        Log.Error(sb.ToString(), pars.ToArray());
                        break;
                    case TipoLog.Alerta:
                        Log.Warning(sb.ToString(), pars.ToArray());
                        break;
                    default:
                        Log.Information(sb.ToString(), pars.ToArray());
                        break;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static void Logar(LogClass logClass)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<object> pars = new List<object>();
                ObterInfoLog(logClass, sb, pars);
                switch (logClass.TipoLog)
                {
                    case TipoLog.Informacao:
                        Log.Information(sb.ToString(), pars.ToArray());
                        break;
                    case TipoLog.Erro:
                        Log.Error(sb.ToString(), pars.ToArray());
                        break;
                    case TipoLog.Alerta:
                        Log.Warning(sb.ToString(), pars.ToArray());
                        break;
                    default:
                        Log.Information(sb.ToString(), pars.ToArray());
                        break;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        static void ObterInfoLog(LogClass logClass, StringBuilder sb, List<object> pars)
        {
            try
            {
                string msgError = string.Empty;
                if (!Enum.IsDefined(logClass.TipoLog))
                    msgError = "Atenção TipoLog inválido";
                if (!Enum.IsDefined(logClass.Processo))
                    msgError = "Atenção Processo inválido";
                if (!Enum.IsDefined(logClass.Aplicacao))
                    msgError = "Atenção Aplicação inválida";
                if (!Enum.IsDefined(logClass.EstadoProcesso))
                    msgError = "Atenção EstadoProcesso inválido";
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);


                if (logClass.EObjetoJson)
                {
                    sb.Append("Objeto JSON logado: {@JsonObj}");
                }
                else 
                 sb.Append(logClass.Msg+" ");

                 sb.Append(" ProcessoId:{ProcessoId} ");
                 sb.Append(" Processo:{Processo} ");
                 sb.Append(" TipoLog:{TipoLog} ");
                 sb.Append(" Aplicacao:{Aplicacao} ");
                 sb.Append(" EstadoProcesso:{EstadoProcesso} ");
                foreach (var key in logClass.Chaves.Keys)
                    sb.Append("{" + key + "}");

                

                if (logClass.EObjetoJson)
                    pars.Add(logClass.Msg);
                 
                    pars.Add(logClass.ProcessoId);
                    pars.Add(logClass.Processo);
                    pars.Add(logClass.TipoLog);
                    pars.Add(logClass.Aplicacao);
                    pars.Add(logClass.EstadoProcesso);
                    foreach (var value in logClass.Chaves.Values)
                        pars.Add(value);
                
            }
            catch (Exception ex)
            {

                throw;
            }

           
        }

        #endregion

        public static bool EValidoEnum<T>(int value) where T : Enum => Enum.IsDefined(typeof(T), value);

        #region " Strings "



        public static string GetQueryString<T>(T obj, bool usingEncode = false,
                                              IEnumerable<string> propsExcluded = null) where T : class
        {

            var properties = obj.GetType().GetProperties();
            Type[] typesWriteQuery = { typeof(string),
                                       typeof(String),
                                       typeof(int),
                                       typeof(Int64),
                                       typeof(Int32),
                                       typeof(decimal),
                                       typeof(Decimal), typeof(bool), typeof(Boolean) };
            var propWriteQuery = new List<string>();
            foreach (var prop in properties)
            {
                if ((propsExcluded ?? new List<string>()).Contains(prop.Name)
                    || prop.GetValue(obj, null) == null
                    || prop.PropertyType == typeof(IList)
                    || !typesWriteQuery.Contains(prop.PropertyType)
                    )
                    continue;
                propWriteQuery.Add(prop.Name + "=" + (usingEncode == true ? HttpUtility.UrlEncode(prop.GetValue(obj, null).ToString()) : prop.GetValue(obj, null).ToString()));

            }
            return String.Join("&", propWriteQuery.ToArray());
        }

        public static string OnlyNumbers(string numbers) => String.Join("", System.Text.RegularExpressions.Regex.Split(string.IsNullOrEmpty(numbers) ? "" : numbers, @"[^\d]"));

        public static bool IsGuid(string numero)
        {

            var guidResult = Guid.Empty;
            return Guid.TryParse(numero, out guidResult);

        }

        /// <summary>
        /// Formatar uma string CNPJ
        /// </summary>
        /// <param name="CNPJ">string CNPJ sem formatacao</param>
        /// <returns>string CNPJ formatada</returns>
        /// <example>Recebe '99999999999999' Devolve '99.999.999/9999-99'</example>

        public static string FormatCNPJ(string CNPJ)
        {
            if (string.IsNullOrEmpty(CNPJ))
                return CNPJ;
            return Convert.ToUInt64(OnlyNumbers(CNPJ)).ToString(@"00\.000\.000\/0000\-00");
        }

        /// <summary>
        /// Formatar uma string CPF
        /// </summary>
        /// <param name="CPF">string CPF sem formatacao</param>
        /// <returns>string CPF formatada</returns>
        /// <example>Recebe '99999999999' Devolve '999.999.999-99'</example>

        public static string FormatCPF(string CPF)
        {
            if (string.IsNullOrEmpty(CPF))
                return CPF;
            return Convert.ToUInt64(OnlyNumbers(CPF)).ToString(@"000\.000\.000\-00");
        }

        public static string FormatRG(string texto) => texto.Substring(0, 2) + "." + texto.Substring(2, 3) + "." + texto.Substring(5, 3) + "-" + texto.Substring(8, 1).ToUpper();

        public static bool IsCpf(string cpf)
        {

            cpf = OnlyNumbers(cpf);

            if (string.IsNullOrEmpty(cpf))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool IsValidEmail(string email)
        {

            if (string.IsNullOrEmpty(email))
                return false;

            bool emailValido = false;
            //Expressão regular retirada de
            //https://msdn.microsoft.com/pt-br/library/01escwtf(v=vs.110).aspx
            string emailRegex = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            try
            {
                emailValido = Regex.IsMatch(
                    email,
                    emailRegex);
            }
            catch (RegexMatchTimeoutException)
            {
                emailValido = false;
            }

            return emailValido;

        }
        public static bool IsCnpj(string cnpj)
        {
            cnpj = OnlyNumbers(cnpj);

            if (string.IsNullOrEmpty(cnpj))
                return false;


            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);

        }

        #endregion

        public static string GetQueryString<T>(T obj, bool encodeValue)
        {

            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + (encodeValue ? HttpUtility.UrlEncode(p.GetValue(obj, null).ToString()) : p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());

        }



        public static XmlDocument ToXmlDocument(XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public static XDocument ToXDocument(XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
        public static string SerializeXml<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }

        public static T DeserializeXml<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }


    }
}
