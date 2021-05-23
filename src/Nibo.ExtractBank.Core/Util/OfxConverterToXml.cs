using Nibo.ExtractBank.Domain.Dto;
using Nibo.ExtractBank.Domain.Entitie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nibo.ExtractBank.Core.Util
{
    public static class OfxConverterToXml
    {
        public static TransactionBankDto Parser(StreamReader ofxSr)
        {
            var xml = ParseToXml(ofxSr);

            OFX ofxSerialized;
            var serializer = new XmlSerializer(typeof(OFX));
            using (var textReader = new StringReader(xml))
            {
                ofxSerialized = (OFX)serializer.Deserialize(textReader);
            }

            return OfxSerializedToDto(ofxSerialized);
        }

        private static TransactionBankDto OfxSerializedToDto(OFX ofxSerialized)
        {
            var BANKACCTFROM = ofxSerialized?.BANKMSGSRSV1?.STMTTRNRS?.STMTRS?.BANKACCTFROM;
            var BANKTRANLIST = ofxSerialized?.BANKMSGSRSV1?.STMTTRNRS?.STMTRS?.BANKTRANLIST;

            if (BANKACCTFROM == null || BANKTRANLIST == null)
                return null;

            var transaction = new TransactionBankDto
            {
                CheckTransaction = BANKACCTFROM.ACCTID,
                IdBank = BANKACCTFROM.BANKID,
                Type = BANKACCTFROM.ACCTTYPE,
                InitDateTransaction = BANKTRANLIST.DTSTART,
                EndtDateTransaction = BANKTRANLIST.DTEND
            };

            if (BANKTRANLIST.STMTTRN != null)
            {
                transaction.Transactions = BANKTRANLIST.STMTTRN.Select(x => new TransactionBankDto
                {
                    Date = ParseDate(x.DTPOSTED),
                    Description = x.MEMO,
                    Amount = x.TRNAMT,
                    Type = x.TRNTYPE
                }).ToList();
            }

            return transaction;
        }

        public static string ParseToXml(StreamReader ofxSr)
        {
            var resultXml = new StringBuilder();
            int level = 0;
            string line;

            while ((line = ofxSr.ReadLine()) != null)
            {
                line = line.Trim();

                if (line.StartsWith("</") && line.EndsWith(">"))
                {
                    resultXml = AddTabs(resultXml, level, true);
                    level--;
                    resultXml.Append(line);
                }
                else if (line.StartsWith("<") && line.EndsWith(">"))
                {
                    level++;
                    resultXml = AddTabs(resultXml, level, true);
                    resultXml.Append(line);
                }
                else if (line.StartsWith("<") && !line.EndsWith(">"))
                {
                    resultXml = AddTabs(resultXml, level + 1, true);
                    resultXml.Append(line);
                    resultXml.Append(ReturnFinalTag(line));
                }
            }
            ofxSr.Close();

            return resultXml.ToString();
        }

        private static StringBuilder AddTabs(StringBuilder sb, int lengthTabs, bool newLine)
        {
            if (newLine)
            {
                sb.AppendLine();
            }
            for (int j = 1; j < lengthTabs; j++)
            {
                sb.Append("\t");
            }

            return sb;
        }

        private static string ReturnFinalTag(string content)
        {
            string returnFinal = "";

            if ((content.IndexOf("<") != -1) && (content.IndexOf(">") != -1))
            {
                int position1 = content.IndexOf("<");
                int position2 = content.IndexOf(">");
                if ((position2 - position1) > 2)
                {
                    returnFinal = content.Substring(position1, (position2 - position1) + 1);
                    returnFinal = returnFinal.Replace("<", "</");
                }
            }

            return returnFinal;
        }

        private static DateTime ParseDate(string dateOfx)
        {            
            var year = int.Parse(dateOfx.Substring(0, 4));
            var month = int.Parse(dateOfx.Substring(4, 2));
            var day = int.Parse(dateOfx.Substring(6, 2));
            var hour = int.Parse(dateOfx.Substring(8, 2));
            var minute = int.Parse(dateOfx.Substring(10, 2));
            var second = int.Parse(dateOfx.Substring(12, 2));

            return new DateTime(year, month, day, hour, minute, second);
        }
    }
}
