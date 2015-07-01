using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP.Util
{
    static class Texto
    {
        public static string removerAcentuacao(string texto)
        {
            texto = texto.Replace('â', 'a');
            texto = texto.Replace('ã', 'a');
            texto = texto.Replace('á', 'a');
            texto = texto.Replace('à', 'a');
            texto = texto.Replace('ä', 'a');

            texto = texto.Replace('é', 'e');
            texto = texto.Replace('è', 'e');
            texto = texto.Replace('ê', 'e');
            texto = texto.Replace('ë', 'e');

            texto = texto.Replace('í', 'i');
            texto = texto.Replace('ì', 'i');
            texto = texto.Replace('î', 'i');
            texto = texto.Replace('ï', 'i');

            texto = texto.Replace('ó', 'o');
            texto = texto.Replace('ò', 'o');
            texto = texto.Replace('ô', 'o');
            texto = texto.Replace('õ', 'o');
            texto = texto.Replace('ö', 'o');

            texto = texto.Replace('ú', 'u');
            texto = texto.Replace('ù', 'u');
            texto = texto.Replace('û', 'u');
            texto = texto.Replace('ü', 'u');

            texto = texto.Replace('ç', 'c');

            texto = texto.Replace('Â', 'A');
            texto = texto.Replace('Ã', 'A');
            texto = texto.Replace('Á', 'A');
            texto = texto.Replace('À', 'A');
            texto = texto.Replace('Ä', 'A');

            texto = texto.Replace('É', 'E');
            texto = texto.Replace('È', 'E');
            texto = texto.Replace('Ê', 'E');
            texto = texto.Replace('Ë', 'E');

            texto = texto.Replace('Í', 'I');
            texto = texto.Replace('Ì', 'I');
            texto = texto.Replace('Î', 'I');
            texto = texto.Replace('Ï', 'I');

            texto = texto.Replace('Ó', 'O');
            texto = texto.Replace('Ò', 'O');
            texto = texto.Replace('Ô', 'O');
            texto = texto.Replace('Õ', 'O');
            texto = texto.Replace('Ö', 'O');

            texto = texto.Replace('Ú', 'U');
            texto = texto.Replace('Ù', 'U');
            texto = texto.Replace('Û', 'U');
            texto = texto.Replace('Ü', 'U');

            texto = texto.Replace('Ç', 'C');

            return texto;
        }
    }
}
