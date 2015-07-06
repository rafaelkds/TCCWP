using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP.Pdf
{
    class Pdf
    {
        Windows.Storage.StorageFile file;
        System.IO.Stream stream;
        System.IO.StreamWriter writer;
        List<long> xrefs;
        

        private async void cabecalho()
        {
            writer.WriteLine("%PDF-1.2");
            writer.Write("%"); writer.Flush();
            stream.Write(new byte[] { 199, 236, 143, 162 }, 0, 4);
            stream.Flush();
            writer.WriteLine("");
        
            //' #1: catalog - the overall container of the entire PDF
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            writer.WriteLine("1 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Type /Catalog");
            writer.WriteLine("  /Pages 2 0 R");
            writer.WriteLine(">>");
            writer.WriteLine("endobj");
        }


        private async void listaDePaginas(List<int> paginas)
        {
            //' #2: page-list - we have only one child page
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            writer.WriteLine("2 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Type /Pages");
            string pag = "";
            foreach (int p in paginas)
                pag += p + " 0 R ";
            pag = pag.Substring(0, pag.Length - 1);
            writer.WriteLine("  /Kids [" + pag + "]");
            writer.WriteLine("  /Count " + paginas.Count);
            writer.WriteLine(">>");
            writer.WriteLine("endobj");
        }

        private async void pagina(int objeto, List<string> titulos, List<List<string>> colunas, int pagina, string nomeRelatorio)
        {
            //' #3: page - this is our page. We specify size, font resources, and the contents
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            writer.WriteLine(objeto + " 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Type /Page");
            writer.WriteLine("  /Parent 2 0 R");
            writer.WriteLine("  /MediaBox [0 0 612 792]");// ' Default userspace units: 72/inch, origin at bottom left
            writer.WriteLine("  /Resources");
            writer.WriteLine("  <<");
            writer.WriteLine("    /ProcSet [/PDF/Text]");// ' This PDF uses only the Text ability
            writer.WriteLine("    /Font");
            writer.WriteLine("    <<");
            objeto++;
            writer.WriteLine("      /F0 "+objeto+" 0 R");// ' I will define three fonts, #4, #5 and #6
            writer.WriteLine("    >>");
            writer.WriteLine("  >>");
            string col = "";
            for (int i = 1; i <= colunas.Count+2; i++)
            {
                col += (i + objeto) + " 0 R ";
            }
            col = col.Substring(0, col.Length - 1);
            writer.WriteLine("  /Contents [" + col + "]");
            writer.WriteLine(">>");
            writer.WriteLine("endobj");

            //' #4, #5, #6: three font resources, all using fonts that are built into all PDF-viewers
            //' We're going to use WinAnsi character encoding, defined below.
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            writer.WriteLine(objeto+" 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Type /Font");
            writer.WriteLine("  /Subtype /Type1");
            writer.WriteLine("  /Encoding /WinAnsiEncoding");
            writer.WriteLine("  /BaseFont /Courier");
            writer.WriteLine(">>");

            ///////////////////////////////////////////////////
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            System.Text.StringBuilder sb = new StringBuilder();
            sb.AppendLine("BT");//             ' BT = begin text object, with text-units the same as userspace-units
            sb.AppendLine("/F0 10 Tf");//      ' Tf = start using the named font "F0" with size "40"
            sb.AppendLine("10 TL");//          ' TL = set line height to "40"
            sb.AppendLine("25.0 775.0 Td");// ' Td = position text point at coordinates "230.0", "400.0"
            sb.AppendLine("(" + nomeRelatorio + ") '");//  ' Apostrophe = print the text, and advance to the next line
            objeto++;
            writer.WriteLine(objeto + " 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Length " + sb.Length);
            writer.WriteLine(">>");
            writer.WriteLine("stream");
            writer.Write(sb.ToString());
            writer.WriteLine("endstream");
            writer.WriteLine("endobj");

            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            sb = new StringBuilder();
            sb.AppendLine("BT");//             ' BT = begin text object, with text-units the same as userspace-units
            sb.AppendLine("/F0 10 Tf");//      ' Tf = start using the named font "F0" with size "40"
            sb.AppendLine("10 TL");//          ' TL = set line height to "40"
            sb.AppendLine("550.0 775.0 Td");// ' Td = position text point at coordinates "230.0", "400.0"
            sb.AppendLine("(" + pagina + ") '");//  ' Apostrophe = print the text, and advance to the next line
            objeto++;
            writer.WriteLine(objeto + " 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Length " + sb.Length);
            writer.WriteLine(">>");
            writer.WriteLine("stream");
            writer.Write(sb.ToString());
            writer.WriteLine("endstream");
            writer.WriteLine("endobj");
            ///////////////////////////////////////////////////

            
            for (int i = 0; i < colunas.Count; i++)
            {

                //' #7: contents of page. This is written in postscript, fully described in
                //' chapter 8 of the PDF 1.2 reference manual.
                writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
                sb = new StringBuilder();
                sb.AppendLine("BT");//             ' BT = begin text object, with text-units the same as userspace-units
                sb.AppendLine("/F0 10 Tf");//      ' Tf = start using the named font "F0" with size "40"
                sb.AppendLine("10 TL");//          ' TL = set line height to "40"
                double largura;
                int qtdeCaracteres;// = 92 / colunas.Count - 2;
                if (i == 0)
                {
                    largura = 25;
                    qtdeCaracteres = (int)(92 / colunas.Count * 1.5 - 2);
                }
                else
                {
                    double parte = (double)550 / colunas.Count * 1.5;
                    largura = (double)(550 - parte) / (colunas.Count - 1) * (i - 1) + parte + 25;
                    qtdeCaracteres = (int)((92 - (92 / colunas.Count * 1.5)) / (colunas.Count - 1)) - 2;
                }
                
                sb.AppendLine(largura.ToString("##0.0").Replace(',', '.') + " 750.0 Td");// ' Td = position text point at coordinates "230.0", "400.0"
                //sb.AppendLine(((double)550 / colunas.Count * i + 25).ToString("##0.0").Replace(',', '.') + " 750.0 Td");// ' Td = position text point at coordinates "230.0", "400.0"
                
                sb.AppendLine("("+titulos[i]+") '");//  ' Apostrophe = print the text, and advance to the next line
                sb.Append("(");
                sb.Append('-', qtdeCaracteres);
                sb.AppendLine(") '");//  ' Apostrophe = print the text, and advance to the next line

                foreach (string linha in colunas[i])
                    sb.AppendLine("(" + linha.Substring(0, qtdeCaracteres > linha.Length ? linha.Length : qtdeCaracteres) + ") '");

                sb.AppendLine("ET");//             '
                objeto++;
                writer.WriteLine(objeto + " 0 obj");
                writer.WriteLine("<<");
                writer.WriteLine("  /Length " + sb.Length);
                writer.WriteLine(">>");
                writer.WriteLine("stream");
                writer.Write(sb.ToString());
                writer.WriteLine("endstream");
                writer.WriteLine("endobj");
            }
        }

        private async void finalizar()
        {
            //' PDF-XREFS. This part of the PDF is an index table into every object #1..#7
            //' that we defined.
            writer.Flush(); stream.Flush(); long xref_pos = stream.Position;
            writer.WriteLine("xref");
            writer.WriteLine("1 " + xrefs.Count);
            foreach (long xref in xrefs)
            {
                writer.WriteLine("{0:0000000000} {1:00000} n", xref, 0);
            }

            //' PDF-TRAILER. Every PDF ends with this trailer.
            writer.WriteLine("trailer");
            writer.WriteLine("<<");
            writer.WriteLine("  /Size " + xrefs.Count);
            writer.WriteLine("  /Root 1 0 R");
            writer.WriteLine(">>");
            writer.WriteLine("startxref");
            writer.WriteLine(xref_pos);
            writer.WriteLine("%%EOF");
        }

        
        

        public async void criar(string nomeRelatorio, List<string> titulos, List<List<string>> colunas)
        {
            file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(nomeRelatorio.Replace(" ", "") + ".pdf", Windows.Storage.CreationCollisionOption.ReplaceExisting);

            stream = await System.IO.WindowsRuntimeStorageExtensions.OpenStreamForWriteAsync(file);
            writer = new System.IO.StreamWriter(stream, System.Text.Encoding.UTF8);

            xrefs = new List<long>();


            int linhasPorPagina = 70;
            int totLinhas = 0;
            foreach (List<string> item in colunas)
                if (totLinhas < item.Count) totLinhas = item.Count;

            double qtdePaginas = Math.Ceiling((double)totLinhas / linhasPorPagina);
            
            List<int> paginas = new List<int>();
            int pag = 3;
            int i = 0;
            do
            {
                i++;
                paginas.Add(pag);
                pag += 2 + colunas.Count +2;
            }
            while (i < qtdePaginas);

            cabecalho();

            listaDePaginas(paginas);

            for (int x = 0; x < qtdePaginas; x++)
            {
                List<List<string>> subcolunas = new List<List<string>>();
                foreach(List<string> item in colunas)
                {
                    List<string> subcoluna = 
                        item.GetRange(x * linhasPorPagina,
                        x * linhasPorPagina + linhasPorPagina < item.Count ? linhasPorPagina : item.Count - x * linhasPorPagina);
                    subcolunas.Add(subcoluna);
                }
                pagina(paginas[x], titulos, subcolunas, x+1, nomeRelatorio);
            }

            finalizar();

            writer.Close();
            stream.Close();

            await Windows.System.Launcher.LaunchFileAsync(file);
        }
    }
}
