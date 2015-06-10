using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP.Pdf
{
    class Pdf
    {
        public async void gerar()
        {
            
            Windows.Storage.StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("a.pdf", Windows.Storage.CreationCollisionOption.ReplaceExisting);
        
            System.IO.Stream stream = await System.IO.WindowsRuntimeStorageExtensions.OpenStreamForWriteAsync(file);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream, System.Text.Encoding.UTF8);
                      
            List<long> xrefs = new List<long>();

            //' PDF-HEADER
            writer.WriteLine("%PDF-1.2");
            
            //' PDF-BODY. Convention is to start with a 4-byte binary comment
            //' so everyone recognizes the pdf as binary. Then the file has
            //' a load of numbered objects, #1..#7 in this case
            writer.Write("%"); writer.Flush();
            stream.Write(new byte[]{199, 236, 143, 162}, 0, 4);
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

            //' #2: page-list - we have only one child page
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            writer.WriteLine("2 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Type /Pages");
            writer.WriteLine("  /Kids [3 0 R 7 0 R]");
            writer.WriteLine("  /Count 2");
            writer.WriteLine(">>");
            writer.WriteLine("endobj");

            //' #3: page - this is our page. We specify size, font resources, and the contents
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            writer.WriteLine("3 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Type /Page");
            writer.WriteLine("  /Parent 2 0 R");
            writer.WriteLine("  /MediaBox [0 0 612 792]");// ' Default userspace units: 72/inch, origin at bottom left
            writer.WriteLine("  /Resources");
            writer.WriteLine("  <<");
            writer.WriteLine("    /ProcSet [/PDF/Text]");// ' This PDF uses only the Text ability
            writer.WriteLine("    /Font");
            writer.WriteLine("    <<");
            writer.WriteLine("      /F0 4 0 R");// ' I will define three fonts, #4, #5 and #6
            writer.WriteLine("    >>");
            writer.WriteLine("  >>");
            writer.WriteLine("  /Contents [5 0 R 6 0 R]");
            writer.WriteLine(">>");
            writer.WriteLine("endobj");

            //' #4, #5, #6: three font resources, all using fonts that are built into all PDF-viewers
            //' We're going to use WinAnsi character encoding, defined below.
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            writer.WriteLine("4 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Type /Font");
            writer.WriteLine("  /Subtype /Type1");
            writer.WriteLine("  /Encoding /WinAnsiEncoding");
            writer.WriteLine("  /BaseFont /Courier");
            writer.WriteLine(">>");

            //' #7: contents of page. This is written in postscript, fully described in
            //' chapter 8 of the PDF 1.2 reference manual.
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            System.Text.StringBuilder sb = new StringBuilder();
            sb.AppendLine("BT");//             ' BT = begin text object, with text-units the same as userspace-units
            sb.AppendLine("/F0 10 Tf");//      ' Tf = start using the named font "F0" with size "40"
            sb.AppendLine("10 TL");//          ' TL = set line height to "40"
            sb.AppendLine("25.0 775.0 Td");// ' Td = position text point at coordinates "230.0", "400.0"
            sb.AppendLine("(Nome) '");//  ' Apostrophe = print the text, and advance to the next line
            sb.AppendLine("/F0 10 Tf");//      '
            sb.AppendLine("10 TL");//          '
            sb.AppendLine("(ol"+enc["é"]+") '");
            sb.AppendLine("ET");//             '
            
            writer.WriteLine("5 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Length " + sb.Length);
            writer.WriteLine(">>");
            writer.WriteLine("stream");
            writer.Write(sb.ToString());
            writer.WriteLine("endstream");
            writer.WriteLine("endobj");


            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            System.Text.StringBuilder ssb = new StringBuilder();
            ssb.AppendLine("BT") ;//            ' BT = begin text object, with text-units the same as userspace-units
            ssb.AppendLine("/F0 10 Tf");//      ' Tf = start using the named font "F0" with size "40"
            ssb.AppendLine("10 TL");//          ' TL = set line height to "40"
            ssb.AppendLine("225.0 775.0 Td");// ' Td = position text point at coordinates "230.0", "400.0"
            ssb.AppendLine("(Quantidade) '");//  ' Apostrophe = print the text, and advance to the next line
            ssb.AppendLine("(ol" + enc["é"] + ") '");
            ssb.AppendLine("ET");//             '
            
            writer.WriteLine("6 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Length " + ssb.Length);
            writer.WriteLine(">>");
            writer.WriteLine("stream");
            writer.Write(ssb.ToString());
            writer.WriteLine("endstream");
            writer.WriteLine("endobj");

            //''''''''''''''''''''''''''''''''''''
            //' #3: page - this is our page. We specify size, font resources, and the contents
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            writer.WriteLine("7 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Type /Page");
            writer.WriteLine("  /Parent 2 0 R");
            writer.WriteLine("  /MediaBox [0 0 612 792]");// ' Default userspace units: 72/inch, origin at bottom left
            writer.WriteLine("  /Resources");
            writer.WriteLine("  <<");
            writer.WriteLine("    /ProcSet [/PDF/Text]");// ' This PDF uses only the Text ability
            writer.WriteLine("    /Font");
            writer.WriteLine("    <<");
            writer.WriteLine("      /F0 4 0 R");// ' I will define three fonts, #4, #5 and #6
            writer.WriteLine("    >>");
            writer.WriteLine("  >>");
            writer.WriteLine("  /Contents [9 0 R 10 0 R]");
            writer.WriteLine(">>");
            writer.WriteLine("endobj");

            //' #4, #5, #6: three font resources, all using fonts that are built into all PDF-viewers
            //' We're going to use WinAnsi character encoding, defined below.
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            writer.WriteLine("8 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Type /Font");
            writer.WriteLine("  /Subtype /Type1");
            writer.WriteLine("  /Encoding /WinAnsiEncoding");
            writer.WriteLine("  /BaseFont /Courier");
            writer.WriteLine(">>");

            //' #7: contents of page. This is written in postscript, fully described in
            //' chapter 8 of the PDF 1.2 reference manual.
            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            writer.WriteLine("9 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Length " + sb.Length);
            writer.WriteLine(">>");
            writer.WriteLine("stream");
            writer.Write(sb.ToString());
            writer.WriteLine("endstream");
            writer.WriteLine("endobj");


            writer.Flush(); stream.Flush(); xrefs.Add(stream.Position);
            writer.WriteLine("10 0 obj");
            writer.WriteLine("<<");
            writer.WriteLine("  /Length " + ssb.Length);
            writer.WriteLine(">>");
            writer.WriteLine("stream");
            writer.Write(ssb.ToString());
            writer.WriteLine("endstream");
            writer.WriteLine("endobj");
            //''''''''''''''''''''''''''''''''''''

            //' PDF-XREFS. This part of the PDF is an index table into every object #1..#7
            //' that we defined.
            writer.Flush(); stream.Flush(); long xref_pos = stream.Position;
            writer.WriteLine("xref");
            writer.WriteLine("1 " + xrefs.Count);
            foreach(long xref in xrefs)
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

            writer.Close();
            stream.Close();

            await Windows.System.Launcher.LaunchFileAsync(file);
        }

        Dictionary<string, string> enc = new Dictionary<string, string>
        {{"A", "\\101"},{"Æ","\\306"},{"Á","\\301"},{"Â","\\302"},{"Ä","\\304"},{"À","\\300"},{"Å","\\305"},{"Ã","\\303"},{"B","\\102"},
        {"C","\\103"},{"Ç","\\307"},{"D","\\104"},{"E","\\105"},{"É","\\311"},{"Ê","\\312"},{"Ë","\\313"},{"È","\\310"},{"Ð","\\320"},
        {"F","\\106"},{"G","\\107"},{"H","\\110"},{"I","\\111"},{"Í","\\315"},{"Î","\\316"},{"Ï","\\317"},{"Ì","\\314"},{"J","\\112"},
        {"K","\\113"},{"L","\\114"},{"M","\\115"},{"N","\\116"},{"Ñ","\\321"},{"O","\\117"},{"Œ","\\214"},{"Ó","\\323"},{"Ô","\\324"},
		{"Ö","\\326"},{"Ò","\\322"},{"Ø","\\330"},{"Õ","\\325"},{"P","\\120"},{"Q","\\121"},{"R","\\122"},{"S","\\123"},{"Š","\\212"},
		{"T","\\124"},{"Þ","\\336"},{"U","\\125"},{"Ú","\\332"},{"Û","\\333"},{"Ü","\\334"},{"Ù","\\331"},{"V","\\126"},{"W","\\127"},
		{"X","\\130"},{"Y","\\131"},{"Ý","\\335"},{"Ÿ","\\237"},{"Z","\\132"},{"a","\\141"},{"á","\\341"},{"â","\\342"},{"´","\\264"},
		{"ä","\\344"},{"æ","\\346"},{"à","\\340"},{"&","\\046"},{"å","\\345"},{"^","\\136"},{"~","\\176"},{"*","\\052"},{"@","\\100"},
		{"ã","\\343"},{"b","\\142"},{"\\","\\134"},{"|","\\174"},{"{","\\173"},{"}","\\175"},{"[","\\133"},{"]","\\135"},{"•","\\225"},
		{"c","\\143"},{"ç","\\347"},{"¸","\\270"},{"¢","\\242"},{"ˆ","\\210"},{":","\\072"},{",","\\054"},{"©","\\251"},{"¤","\\244"},
		{"d","\\144"},{"†","\\206"},{"‡","\\207"},{"°","\\260"},{"¨","\\250"},{"÷","\\367"},{"$","\\044"},{"e","\\145"},{"é","\\351"},
		{"ê","\\352"},{"ë","\\353"},{"è","\\350"},{"8","\\070"},{"…","\\205"},{"—","\\227"},{"–","\\226"},{"=","\\075"},{"ð","\\360"},
		{"!","\\041"},{"¡","\\241"},{"f","\\146"},{"5","\\065"},{"ƒ","\\203"},{"4","\\064"},{"g","\\147"},{"ß","\\337"},{"`","\\140"},
		{">","\\076"},{"«","\\253"},{"»","\\273"},{"‹","\\213"},{"›","\\233"},{"h","\\150"},{"-","\\055"},{"i","\\151"},{"í","\\355"},
		{"î","\\356"},{"ï","\\357"},{"ì","\\354"},{"j","\\152"},{"k","\\153"},{"l","\\154"},{"<","\\074"},{"¬","\\254"},{"m","\\155"},
		{"¯","\\257"},{"µ","\\265"},{"∏","\\327"},{"n","\\156"},{"9","\\071"},{"ñ","\\361"},{"#","\\043"},{"o","\\157"},{"ó","\\363"},
		{"ô","\\364"},{"ö","\\366"},{"œ","\\234"},{"ò","\\362"},{"1","\\061"},{"½","\\275"},{"¼","\\274"},{"¹","\\271"},{"ª","\\252"},
		{"º","\\272"},{"ø","\\370"},{"õ","\\365"},{"p","\\160"},{"¶","\\266"},{"(","\\050"},{")","\\051"},{"%","\\045"},{".","\\056"},
		{"·","\\267"},{"‰","\\211"},{"+","\\053"},{"±","\\261"},{"q","\\161"},{"?","\\077"},{"¿","\\277"},{"\"","\\042"},{"„","\\204"},
		{((char)8220).ToString(),"\\223"},{((char)8221).ToString(),"\\224"},{"‘","\\221"},{"’","\\222"},{"‚","\\202"},{"'","\\047"},
		{"r","\\162"},{"®","\\256"},{"˚","\\260"},{"s","\\163"},{"š","\\232"},{"§","\\247"},{";","\\073"},{"7","\\067"},{"6","\\066"},
		{"/","\\057"},{" ","\\040"},{"£","\\243"},{"t","\\164"},{"þ","\\376"},{"3","\\063"},{"¾","\\276"},{"³","\\263"},{"˜","\\230"},
		{"™","\\231"},{"2","\\062"},{"²","\\262"},{"u","\\165"},{"ú","\\372"},{"û","\\373"},{"ü","\\374"},{"ù","\\371"},{"_","\\137"},
		{"v","\\166"},{"w","\\167"},{"x","\\170"},{"y","\\171"},{"ý","\\375"},{"ÿ","\\377"},{"¥","\\245"},{"z","\\172"},{"0","\\060"}};

    }
}
