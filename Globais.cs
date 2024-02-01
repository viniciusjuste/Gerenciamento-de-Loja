using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_de_Loja
{
    internal class Globais
    {
        public static int nivel = 0;
        public static Boolean logado = false;
        public static String caminho = System.Environment.CurrentDirectory;
        public static String nomeBanco = "gerenciamento-loja";
        public static String caminhoBanco = caminho + @"\banco-de-dados\";
        public static String caminhoFoto = caminho + @"\fotos-produtos\";
        public static String caminhoFotoFunc = caminho + @"\fotosfunc\";
    }
}
