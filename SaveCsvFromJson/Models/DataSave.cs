using Microsoft.AspNetCore.Mvc;

namespace SaveCsvFromJson.Models
{
    public class DataSave
    {
        private string JsonContent { get; set; }
        public string Resultado { get; set; }
        public bool JsonValido { get; set; }

        public bool ValidaJson(string jsoncontent)
        {
            //Se há conteúdo
            if (string.IsNullOrWhiteSpace(jsoncontent)) 
                JsonValido = false;

            //Verifica se inicia e termina com o padrão de json...
            //Verificação simples se inicia no padrão do json, pois para fazer uma checagem mais completa,
            //sem usar alguma biblioteca ou pacote teria que pesquisar pois não lembro de cabeça mais...
            if ((jsoncontent.StartsWith("{") && jsoncontent.EndsWith("}")) || 
                (jsoncontent.StartsWith("[") && jsoncontent.EndsWith("]"))) 
            {                
                JsonValido = true;
            }
            else
            {
                JsonValido = false;
            }

            return JsonValido;
        }     

        public string SalvarCSV(string jsoncontent)
        {
            if(ValidaJson(jsoncontent))
            {
                this.JsonContent = jsoncontent;
                string[] valor = JsonContent.Replace("\"[{", "").Replace("}]\"", "").Replace("\"", "").Replace("{", "").Replace("}", "").Trim().Split(',');
                var list = new List<KeyValuePair<string, string>>();

                foreach (var item in valor)
                {
                    //Adiciona a chave e o value no list
                    list.Add(new KeyValuePair<string, string>(item.Split(':')[0], item.Split(':')[1]));
                }

                //Join na string csv populando chave e valor para gerar CSV
                string Csv = String.Join(Environment.NewLine, list.Select(x => $"{x.Key};{x.Value};"));
                var path = @"wwwroot/Downloads/json.csv";

                this.Resultado = Csv;

                //Salvar CSV
                File.WriteAllText(path, Csv);
            }
            
            return Resultado;
        }

    }
}
