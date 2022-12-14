using AgendaContatos.Reports.Interfaces;
using AgendaContatos.Reports.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Reports.Services
{
    /// <summary>
    /// Implementação do relatório de contatos para formato Excel
    /// </summary>
    public class ContatosReportExcel : IContatosReport
    {
        public byte[] Create(ContatosReportModel model)
        {
            //definindo o tipo de uso da biblioteca (free)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //criando um arquivo excel
            using (var excelPackage = new ExcelPackage())
            {
                //criando uma planilha
                var planilha = excelPackage.Workbook.Worksheets.Add("Contatos");

                #region Título da planilha

                planilha.Cells["A1"].Value = "Relatório de Contatos";
                var titulo = planilha.Cells["A1:D1"];
                titulo.Merge = true;
                titulo.Style.Font.Size = 18;
                titulo.Style.Font.Bold = true;

                planilha.Cells["A3"].Value = "Gerado em:";
                planilha.Cells["B3"].Value = model.DataHora.Value.ToString("dd/MM/yyyy HH:mm");

                planilha.Cells["A4"].Value = "Nome do usuário:";
                planilha.Cells["B4"].Value = model.Usuario.Nome;

                planilha.Cells["A5"].Value = "Email do usuário:";
                planilha.Cells["B5"].Value = model.Usuario.Email;

                #endregion

                #region Dados da planilha

                planilha.Cells["A7"].Value = "Nome do Contato";
                planilha.Cells["B7"].Value = "Email";
                planilha.Cells["C7"].Value = "Telefone";
                planilha.Cells["D7"].Value = "Data de Nascimento";

                var cabecalho = planilha.Cells["A7:D7"];
                cabecalho.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#ffffff"));
                cabecalho.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cabecalho.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#000000"));

                var linha = 8;

                foreach (var item in model.Contatos)
                {
                    planilha.Cells[$"A{linha}"].Value = item.Nome;
                    planilha.Cells[$"B{linha}"].Value = item.Email;
                    planilha.Cells[$"C{linha}"].Value = item.Telefone;
                    planilha.Cells[$"D{linha}"].Value = item.DataNascimento.ToString("dd/MM/yyyy");

                    if(linha % 2 == 0)
                    {
                        var conteudo = planilha.Cells[$"A{linha}:D{linha}"];
                        conteudo.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        conteudo.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#eeeeeee"));
                    }

                    linha++;
                }

                #endregion

                #region Finalização

                planilha.Cells["A:D"].AutoFitColumns();

                var borda = planilha.Cells[$"A7:D{linha - 1}"];
                borda.Style.Border.BorderAround(ExcelBorderStyle.Medium);

                //retornando o conteúdo do arquivo..
                return excelPackage.GetAsByteArray();

                #endregion
            }
        }
    }
}
